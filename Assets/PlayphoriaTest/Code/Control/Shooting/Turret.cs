using System.Collections.Generic;
using UnityEngine;

namespace PlayphoriaTest.Control.Shooting
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private int initialBulletCount = 50;
        [SerializeField] private float rotationSpeed = 1f;
        [SerializeField] private float shootDelay = .3f;
        [SerializeField] private Transform[] anchors;
        
        private Queue<Bullet> _bulletPool;
        private float _timeToShoot;

        private void Awake()
        {
            InitBulletPool();
        }

        private void InitBulletPool()
        {
            _bulletPool = new Queue<Bullet>();
            SpawnBullets(initialBulletCount);
        }

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
            if(_bulletPool.Count < 2) SpawnBullets(2);
            var turretRotation = transform.rotation;
            var rotations = new[] { turretRotation, turretRotation * Quaternion.Euler(0, 180, 0)};
            for (int i = 0; i < 2; i++)
            {
                var bullet = _bulletPool.Dequeue();
                bullet.transform.rotation = rotations[i];
                bullet.transform.position = anchors[i].position;
                bullet.gameObject.SetActive(true);
            }
        }

        private void SpawnBullets(int count)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnBullet();
            }
        }

        private void Rotate()
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

        private void SpawnBullet()
        {
            var bullet = Instantiate(bulletPrefab, transform).GetComponent<Bullet>();
            bullet.gameObject.SetActive(false);
            bullet.Init(this);
            _bulletPool.Enqueue(bullet);
        }

        public void Repool(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            bullet.transform.parent = transform;
            _bulletPool.Enqueue(bullet);
        }
    }
}
