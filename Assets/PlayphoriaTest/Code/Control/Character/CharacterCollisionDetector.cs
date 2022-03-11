using System;
using PlayphoriaTest.Model;
using UnityEngine;

namespace PlayphoriaTest.Control.Character
{
    [RequireComponent(typeof(CharacterHandsAnimation))]
    [RequireComponent(typeof(Collider))]
    public class CharacterCollisionDetector : MonoBehaviour
    {
        public static event Action<float> OnDamageCollision; 

        [SerializeField] private LayerMask obstacleMask;
        [SerializeField] private LayerMask bulletMask;
        [SerializeField] private CharacterHandsAnimation characterHandsAnimation;

        public Vector3 LastBulletHitDirection { get; private set; }

        private void OnValidate()
        {
            characterHandsAnimation ??= GetComponent<CharacterHandsAnimation>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (ObstacleCollision(other))
            {
                characterHandsAnimation.IterateObstacleCollisions(1);
            }
            else if(BulletCollision(other))
            {
                LastBulletHitDirection = (other.contacts[0].point - transform.position).normalized;
                var damage = other.gameObject.GetComponent<IDamageSource>().Damage;
                OnDamageCollision?.Invoke(damage);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (ObstacleCollision(other))
            {
                characterHandsAnimation.IterateObstacleCollisions(-1);
            }
        }

        private bool BulletCollision(Collision other) 
            => MaskContainsLayer(bulletMask, other.gameObject.layer);

        private bool ObstacleCollision(Collision other) 
            => MaskContainsLayer(obstacleMask, other.gameObject.layer);

        private static bool MaskContainsLayer(LayerMask mask, int layer) 
            => mask == (mask | (1 << layer));
    }
}
