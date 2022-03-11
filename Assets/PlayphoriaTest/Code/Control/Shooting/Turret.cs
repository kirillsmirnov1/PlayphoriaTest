using UnityEngine;

namespace PlayphoriaTest.Control.Shooting
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 1f;
        [SerializeField] private float shootDelay = .3f;
        [SerializeField] private Transform[] anchors;
        
        private float _timeToShoot;

        private void Update()
        {
            Rotate();
            DelayShooting();
        }

        private void DelayShooting()
        {
            if (Time.time > _timeToShoot)
            {
                _timeToShoot = Time.time + shootDelay;
                Shoot();
            }
        }

        private void Shoot()
        {
            var turretRotation = transform.rotation;
            var rotations = new[] { turretRotation, turretRotation * Quaternion.Euler(0, 180, 0)};
            for (int i = 0; i < 2; i++)
            {
                var bullet = BulletPool.GetBullet();
                bullet.transform.SetPositionAndRotation(anchors[i].position, rotations[i]);
                bullet.gameObject.SetActive(true);
            }
        }

        private void Rotate()
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }
}
