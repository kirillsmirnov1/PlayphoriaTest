using UnityEngine;

namespace PlayphoriaTest.Control
{
    [RequireComponent(typeof(CharacterHandsAnimation))]
    [RequireComponent(typeof(Collider))]
    public class CharacterCollisionDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask obstacleMask;
        [SerializeField] private CharacterHandsAnimation characterHandsAnimation;

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
        }

        private void OnCollisionExit(Collision other)
        {
            if (ObstacleCollision(other))
            {
                characterHandsAnimation.IterateObstacleCollisions(-1);
            }
        }

        private bool ObstacleCollision(Collision other) 
            => MaskContainsLayer(obstacleMask, other.gameObject.layer);

        private static bool MaskContainsLayer(LayerMask mask, int layer) 
            => mask == (mask | (1 << layer));
    }
}
