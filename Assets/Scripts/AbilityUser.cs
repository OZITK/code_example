using UnityEngine;

namespace OZITK
{
    public interface AbilityUser
    {
        public Rigidbody Rigidbody { get; }
        public DebugWindow DebugWindow { get; }
        public Transform MyTransform { get; }

        /// <summary>
        /// Returns first ability of type T
        /// </summary>
        public T GetAbility<T>() where T : Ability;
        /// <summary>
        /// Returns all abilities of type T
        /// </summary>
        public T[] GetAbilities<T>() where T : Ability;
    }
}
