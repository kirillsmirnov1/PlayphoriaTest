using UnityEngine;

namespace PlayphoriaTest.Control
{
    public class FollowObject : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 gap = new Vector3(0, 10, 0);
        [SerializeField] private float speed = 1f;
        
        private void OnValidate()
        {
            if (!Application.isPlaying)
            {
                transform.position = target.position + gap;
            }
        }

        private void FixedUpdate() 
            => LerpToTarget();

        private void LerpToTarget()
        {
            transform.position = Vector3.Lerp(transform.position, target.position + gap, speed * Time.fixedDeltaTime);
        }
    }
}
