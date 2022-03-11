using System.Collections.Generic;
using UnityEngine;

namespace PlayphoriaTest.Control.Shooting
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private int initialBulletCount = 50;

        private static BulletPool _instance;
        
        private Queue<Bullet> _bulletPool;
        
        private void Awake()
        {
            _instance = this;
            InitBulletPool();
        }

        private void InitBulletPool()
        {
            _bulletPool = new Queue<Bullet>();
            SpawnBullets(initialBulletCount);
        }
        
        private void SpawnBullets(int count)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnBullet();
            }
        }
        
        private void SpawnBullet()
        {
            var bullet = Instantiate(bulletPrefab, transform)
                .GetComponent<Bullet>();
            bullet.gameObject.SetActive(false);
            bullet.Init(this);
            _bulletPool.Enqueue(bullet);
        }

        public void Repool(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            _bulletPool.Enqueue(bullet);
        }

        public static Bullet GetBullet() 
            => _instance.GetBulletImpl();

        private Bullet GetBulletImpl()
        {
            if(_bulletPool.Count < 1) SpawnBullet();
            return _bulletPool.Dequeue();
        }
    }
}
