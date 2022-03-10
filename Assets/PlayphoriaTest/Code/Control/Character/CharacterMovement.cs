using Joysticks;
using UnityEngine;
using UnityUtils.Extensions;

namespace PlayphoriaTest.Control.Character
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
        
        private static readonly int Speed = Animator.StringToHash("speed");
        
        private Vector2 _input;
        private float _inputMagnitude;

        private void OnValidate()
        {
            animator ??= GetComponent<Animator>();
            rb ??= GetComponent<Rigidbody>();
        }

        private void Update()
        {
            GetInput();
            SetAnimatorSpeed();
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
    }
}
