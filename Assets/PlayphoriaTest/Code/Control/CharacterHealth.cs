using UnityEngine;

namespace PlayphoriaTest.Control
{
    public class CharacterHealth : MonoBehaviour
    {
        public float Health { get; private set; } = 10;
        
        [SerializeField] private float bulletDamage = 1;

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