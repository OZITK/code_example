using UnityEngine;

namespace OZITK
{
    public class ProjectileAOE : Projectile
    {
        [SerializeField] private float aoeRadius = 5;

        private Collider[] colliders;

        protected override void Awake()
        {
            base.Awake();
            colliders = new Collider[10];
        }

        protected override void DealDamage(Enemy enemy)
        {
            int inRange = Physics.OverlapSphereNonAlloc(transform.position, aoeRadius, colliders);
            for (int i = 0; i < inRange; i++)
            {
                Collider collider = colliders[i];

                if (collider.CompareTag("Enemy"))
                    base.DealDamage(collider.GetComponent<Enemy>());
            }
        }
    }
}
