using UnityEngine;

namespace PlayphoriaTest.Control.Character
{
    [RequireComponent(typeof(CharacterDeath))]
    public class CharacterHealth : MonoBehaviour
    {
        public float Health { get; private set; }
        
        [SerializeField] private CharacterDeath characterDeath;
        [SerializeField] public float baseHealth = 10;
        [SerializeField] private float bulletDamage = 1;

        private bool _dead;
        
        private void OnValidate()
        {
            characterDeath ??= GetComponent<CharacterDeath>();
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
            if (Health <= 0 && !_dead)
            {
                _dead = true;
                characterDeath.Handle();
            }
        }
    }
}