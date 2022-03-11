using System;
using UnityEngine;

namespace PlayphoriaTest.Control.Character
{
    public class CharacterHealth : MonoBehaviour
    {
        public static event Action OnDeath;
        
        public float Health { get; private set; }
        
        [SerializeField] public float baseHealth = 10;

        private bool _dead;

        private void Awake()
        {
            Health = baseHealth;
            CharacterCollisionDetector.OnDamageCollision += OnDamage;
        }

        private void OnDestroy()
        {
            CharacterCollisionDetector.OnDamageCollision -= OnDamage;
        }

        private void OnDamage(float damageValue) 
            => ChangeHealth(-damageValue);

        private void ChangeHealth(float val)
        {
            Health = Mathf.Clamp(Health + val, 0, float.PositiveInfinity);
            if (Health <= 0 && !_dead)
            {
                _dead = true;
                OnDeath?.Invoke();
            }
        }
    }
}