using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace OZITK
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float damage = 1;
        [SerializeField] private float speed = 1;
        [SerializeField] private float destroyTime = 3;

        public float DamageMulti = 1;
        public float Damage => damage;

        private ObjectPool<Projectile> _projectilePool;
        private Rigidbody _rigidbody;
        private bool released = false;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _rigidbody.linearVelocity = transform.forward * speed;
            released = false;

            StartCoroutine(DeactivateAfterTime());
        }

        private void OnCollisionEnter(Collision other)
        {
            if (released)
                return;

            _projectilePool.Release(this);
            released = true;
            GameObject collidedObject = other.gameObject;
            if (collidedObject.CompareTag("Enemy"))
                DealDamage(collidedObject.GetComponent<Enemy>());
        }

        protected virtual void DealDamage(Enemy enemy)
        {
            enemy.GetHit(damage * DamageMulti);
        }

        public void SetPool(ObjectPool<Projectile> projectilePool)
        {
            _projectilePool = projectilePool;
        }

        private IEnumerator DeactivateAfterTime()
        {
            float elapsedTime = 0f;

            while (elapsedTime < destroyTime)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _projectilePool.Release(this);
            released = true;
        }
    }
}
