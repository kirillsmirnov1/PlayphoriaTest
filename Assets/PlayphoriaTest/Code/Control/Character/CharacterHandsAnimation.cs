using UnityEngine;

namespace PlayphoriaTest.Control.Character
{
    [RequireComponent(typeof(Animator))]
    public class CharacterHandsAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float animationChangeSpeed = 1f;
        
        private static readonly int Hands = Animator.StringToHash("hands");
        private int _obstacleCollisions;
        private float _handsValLerped;

        private void OnValidate()
        {
            animator ??= GetComponent<Animator>();
        }

        private void Awake() 
            => CharacterCollisionDetector.OnObstacleCollision += IterateObstacleCollisions;

        private void OnDestroy() 
            => CharacterCollisionDetector.OnObstacleCollision -= IterateObstacleCollisions;

        private void Update()
        {
            HandleDebugHandsInput();
            SetHandsAnimation();
        }

        private void HandleDebugHandsInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IterateObstacleCollisions(1);
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                IterateObstacleCollisions(-1);
            }
        }

        private void IterateObstacleCollisions(int val)
        {
            _obstacleCollisions += val;
        }

        private void SetHandsAnimation()
        {
            var showHands = _obstacleCollisions > 0;
            var handsVal = showHands ? 1f : 0f;
            _handsValLerped = Mathf.Lerp(_handsValLerped, handsVal, Time.deltaTime * animationChangeSpeed);
            animator.SetFloat(Hands, _handsValLerped);
            animator.SetLayerWeight(1, _handsValLerped);
        }
    }
}