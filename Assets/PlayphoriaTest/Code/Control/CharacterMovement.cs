using Joysticks;
using UnityEngine;
using UnityUtils.Extensions;

namespace PlayphoriaTest.Control
{
    [RequireComponent(typeof(Animator))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float speedMod = 1f;
        
        [Header("Components")]
        [SerializeField] private Joystick joystick;
        [SerializeField] private Animator animator;

        private static readonly int Hands = Animator.StringToHash("hands");
        private static readonly int Speed = Animator.StringToHash("speed");

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
            HandleJoystickInput();
            HandleDebugHandsInput();
        }

        private void HandleJoystickInput()
        {
            var input = joystick.Direction;
            var inputMagnitude = input.magnitude;
            
            // Set animation speed
            animator.SetFloat(Speed, inputMagnitude);
            // Rotate
            transform.rotation = Quaternion.Euler(0, 180-input.ToAngleInDegrees(), 0);
            // Move
            transform.position += transform.forward * (inputMagnitude * speedMod * Time.deltaTime);
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
