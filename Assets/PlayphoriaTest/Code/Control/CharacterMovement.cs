using Joysticks;
using UnityEngine;
using UnityUtils.Extensions;

namespace PlayphoriaTest.Control
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float speedMod = 1f;
        
        [Header("Components")]
        [SerializeField] private Joystick joystick;
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody rb;

        private static readonly int Hands = Animator.StringToHash("hands");
        private static readonly int Speed = Animator.StringToHash("speed");

        private bool _showHands;
        private Vector2 _input;
        private float _inputMagnitude;

        private void OnValidate()
        {
            animator ??= GetComponent<Animator>();
            rb ??= GetComponent<Rigidbody>();
        }

        private void Awake()
        {
            SetHandsAnimation();
        }

        private void Update()
        {
            GetInput();
            SetAnimatorSpeed();
            HandleDebugHandsInput();
        }

        private void FixedUpdate()
        {
            MoveCharacter();
        }

        private void GetInput()
        {
            _input = joystick.Direction;
            _inputMagnitude = _input.magnitude;
        }

        private void SetAnimatorSpeed()
        {
            animator.SetFloat(Speed, _inputMagnitude);
        }

        private void MoveCharacter()
        {
            rb.MoveRotation(Quaternion.Euler(0, 180-_input.ToAngleInDegrees(), 0));
            rb.MovePosition(rb.position + transform.forward * (_inputMagnitude * speedMod * Time.fixedDeltaTime));
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
