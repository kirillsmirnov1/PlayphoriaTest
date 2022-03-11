using PlayphoriaTest.Model;
using UnityEngine;

namespace PlayphoriaTest.Control.Shooting
{
    public class Bullet : MonoBehaviour, IDamageSource
    {
        [SerializeField] private float timeTillRepool = 3f;
        [SerializeField] private float speed = 50f;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float damage = 1;
        
        private BulletPool _pool;
        private float _repoolTime;

        public float Damage => damage;

        private void OnValidate()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable() 
            => _repoolTime = Time.time + timeTillRepool;

        public void Init(BulletPool turret) 
            => _pool = turret;

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
            => _pool.Repool(this);
    }
}
