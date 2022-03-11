using System;
using PlayphoriaTest.Model;
using UnityEngine;

namespace PlayphoriaTest.Control.Character
{
    [RequireComponent(typeof(Collider))]
    public class CharacterCollisionDetector : MonoBehaviour
    {
        public static event Action<float> OnDamageCollision;
        public static event Action<int> OnObstacleCollision; 

        [SerializeField] private LayerMask obstacleMask;
        [SerializeField] private LayerMask bulletMask;

        public Vector3 LastBulletHitDirection { get; private set; }

        private void OnCollisionEnter(Collision other)
        {
            if (ObstacleCollision(other))
            {
                OnObstacleCollision?.Invoke(1);
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
                OnObstacleCollision?.Invoke(-1);
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
