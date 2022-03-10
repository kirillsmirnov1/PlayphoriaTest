using UnityEngine;

namespace PlayphoriaTest.Control.Character
{
    [RequireComponent(typeof(Animator))]
    public class CharacterHandsAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private static readonly int Hands = Animator.StringToHash("hands");
        private int _obstacleCollisions;
        
        private void OnValidate()
        {
            animator ??= GetComponent<Animator>();
        }
        
        private void Awake()
        {
            SetHandsAnimation();
        }

        private void Update()
        {
            HandleDebugHandsInput();
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

        public void IterateObstacleCollisions(int val)
        {
            _obstacleCollisions += val;
            SetHandsAnimation();
        }

        private void SetHandsAnimation()
        {
            var showHands = _obstacleCollisions > 0;
            var handsVal = showHands ? 1f : 0f;
            animator.SetFloat(Hands, handsVal);
            animator.SetLayerWeight(1, handsVal);
        }
    }
}