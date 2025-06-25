using System.Linq;
using UnityEngine;

namespace OZITK
{
    public class Player : MonoBehaviour, AbilityUser
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private DebugWindow _debugWindow;
        public Rigidbody Rigidbody => _rigidbody;
        public DebugWindow DebugWindow => _debugWindow;
        public Transform MyTransform => _transform;

        [SerializeField] private Ability[] abilities;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            foreach (var ability in abilities)
            {
                ability.Init(this);
            }
        }
        private void Update()
        {
            foreach (var ability in abilities)
            {
                ability.OnUpdate();
            }
        }
        private void FixedUpdate()
        {
            foreach (var ability in abilities)
            {
                ability.OnFixedUpdate();
            }
        }

        public T GetAbility<T>() where T : Ability
        {
            return abilities.OfType<T>().FirstOrDefault();
        }
        public T[] GetAbilities<T>() where T : Ability
        {
            return abilities.OfType<T>().ToArray();
        }
    }
}
