using UnityEngine;

namespace PlayphoriaTest.Control
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float timeTillRepool = 3f;
        [SerializeField] private float speed = 50f;
        [SerializeField] private Rigidbody rb;
        
        private Turret _turret;
        private float _repoolTime;

        private void OnValidate()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _repoolTime = Time.time + timeTillRepool;
            transform.parent = null;
        }

        public void Init(Turret turret)
        {
            _turret = turret;
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + transform.forward * (speed * Time.fixedDeltaTime));
        }

        private void Update()
        {
            CheckRepool();
        }

        private void CheckRepool()
        {
            if (Time.time > _repoolTime)
            {
                Repool();
            }
        }

        private void OnCollisionEnter(Collision other) 
            => Repool();

        private void Repool() 
            => _turret.Repool(this);
    }
}
