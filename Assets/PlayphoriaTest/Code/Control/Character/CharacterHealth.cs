using System;
using UnityEngine;

namespace PlayphoriaTest.Control.Character
{
    public class CharacterHealth : MonoBehaviour
    {
        public float Health { get; private set; } 

        [SerializeField] public float baseHealth = 10;
        [SerializeField] private float bulletDamage = 1;

        private void Awake()
        {
            Health = baseHealth;
        }

        public void OnBulletHit()
        {
            ChangeHealth(-bulletDamage);
        }

        private void ChangeHealth(float val)
        {
            Health = Mathf.Clamp(Health + val, 0, float.PositiveInfinity);
            if (Health <= 0)
            {
                Debug.Log("Player is dead"); // TODO ragdoll
            }
        }
    }
}