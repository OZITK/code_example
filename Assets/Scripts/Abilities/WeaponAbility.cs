using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

namespace OZITK
{
    public class WeaponAbility : Ability
    {
        [Tooltip("Transform used to cast aim ray for shooting")]
        [SerializeField] private Transform aimTransform;
        [Tooltip("Position from which projectiles are fired")]
        [SerializeField] private Transform shootPoint;
        [Tooltip("Projectile prefab to spawn")]
        [SerializeField] private Projectile _projectile;
        [Tooltip("Layers that projectile raycast can hit")]
        [SerializeField] private LayerMask shootMask;

        [Tooltip("Damage multiplier applied to projectiles (e.g.buffs, debuffs)")]
        public float DamageMulti = 1;

        private TextMeshProUGUI debugText;

        private ObjectPool<Projectile> _projectilePool; // Object pool managing projectile instances
        protected override void OnInit()
        {
            debugText = _user.DebugWindow.CreateDebugText();
            input.action.performed += SpawnProjectile;
            _projectilePool = new ObjectPool<Projectile>(CreateProjectile, OnTakeProjectileFromPool, OnReturnProjectileToPool, OnDestroyProjectile, true, 50, 100);
        }

        private void OnDestroy()
        {
            input.action.performed -= SpawnProjectile;
        }

        public override void OnUpdate()
        {
            debugText.text = $"Weapon damage: {_projectile.Damage * DamageMulti}";
        }

        private void SpawnProjectile(InputAction.CallbackContext obj)
        {
            _projectilePool.Get();
        }

        /// <summary>
        /// Calculates aim direction using raycast
        /// </summary>
        private Quaternion GetShootRotation()
        {
            if (Physics.Raycast(aimTransform.position, aimTransform.forward, out RaycastHit hit, 100, shootMask, QueryTriggerInteraction.Ignore))
            {
                return Quaternion.LookRotation((hit.point - shootPoint.position).normalized, Vector3.up);
            }
            return shootPoint.rotation;
        }

        private Projectile CreateProjectile()
        {
            Projectile projectile = Instantiate(_projectile, shootPoint.position, GetShootRotation());

            projectile.DamageMulti = DamageMulti;

            // Link projectile to pool for recycling
            projectile.SetPool(_projectilePool);

            return projectile;
        }

        private void OnTakeProjectileFromPool(Projectile projectile)
        {
            projectile.transform.position = shootPoint.position;
            projectile.transform.rotation = GetShootRotation();

            projectile.DamageMulti = DamageMulti;

            projectile.gameObject.SetActive(true);
        }

        private void OnReturnProjectileToPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }

        private void OnDestroyProjectile(Projectile projectile)
        {
            Destroy(projectile.gameObject);
        }
    }
}
