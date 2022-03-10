using System;
using UnityEngine;

namespace PlayphoriaTest.Control.Character
{
    [RequireComponent(typeof(RagdollHelper))]
    public class CharacterHealth : MonoBehaviour
    {
        public float Health { get; private set; }
        
        [SerializeField] private RagdollHelper ragdollHelper;
        [SerializeField] public float baseHealth = 10;
        [SerializeField] private float bulletDamage = 1;

        private void OnValidate()
        {
            ragdollHelper ??= GetComponent<RagdollHelper>();
        }

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
                OnDeath();
            }
        }

        private void OnDeath()
        {
            ragdollHelper.ragdolled = true;
        }
    }
}