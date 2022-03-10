using UnityEngine;

namespace PlayphoriaTest.Control.Character
{
    [RequireComponent(typeof(CharacterHealth))]
    [RequireComponent(typeof(CharacterHandsAnimation))]
    [RequireComponent(typeof(Collider))]
    public class CharacterCollisionDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask obstacleMask;
        [SerializeField] private LayerMask bulletMask;
        [SerializeField] private CharacterHandsAnimation characterHandsAnimation;
        [SerializeField] private CharacterHealth characterHealth;
        
        private void OnValidate()
        {
            characterHandsAnimation ??= GetComponent<CharacterHandsAnimation>();
            characterHealth ??= GetComponent<CharacterHealth>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (ObstacleCollision(other))
            {
                characterHandsAnimation.IterateObstacleCollisions(1);
            }
            else if(BulletCollision(other))
            {
                characterHealth.OnBulletHit();
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
