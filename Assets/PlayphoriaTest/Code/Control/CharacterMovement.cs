using System;
using UnityEngine;

namespace PlayphoriaTest.Control
{
    [RequireComponent(typeof(Animator))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private static readonly int Hands = Animator.StringToHash("hands");
        
        private bool _showHands;
        
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
                _showHands = true;
                SetHandsAnimation();
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                _showHands = false;
                SetHandsAnimation();
            }
        }

        private void SetHandsAnimation()
        {
            var handsVal = _showHands ? 1f : 0f;
            animator.SetFloat(Hands, handsVal);
            animator.SetLayerWeight(1, handsVal);
        }
    }
}
