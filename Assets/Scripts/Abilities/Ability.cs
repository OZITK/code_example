using UnityEngine;
using UnityEngine.InputSystem;

namespace OZITK
{
    public abstract class Ability : MonoBehaviour // TODO: Remove from MonoBehaviour and make it serializable
    {
        [Tooltip("Reference to input action triggering this ability")]
        [SerializeField] protected InputActionReference input;

        protected AbilityUser _user; // Reference to the owner using this ability

        /// <summary>
        /// Initializes the ability with its owner
        /// </summary>
        public void Init(AbilityUser user)
        {
            _user = user;
            OnInit();
        }
        /// <summary>
        /// Called once during initialization
        /// </summary>
        protected virtual void OnInit()
        {

        }
        /// <summary>
        /// Called every frame
        /// </summary>
        public virtual void OnUpdate()
        {

        }
        /// <summary>
        /// Called every physics frame
        /// </summary>
        public virtual void OnFixedUpdate()
        {

        }

        /// <summary>
        /// Returns the bound key as a human-readable string
        /// </summary>
        public string GetInputKeyString()
        {
            var bindings = input.action.bindings;

            foreach (var binding in bindings)
            {
                if (binding.isComposite || binding.isPartOfComposite)
                    continue;

                string path = binding.effectivePath;

                var displayString = InputControlPath.ToHumanReadableString(
                    path,
                    InputControlPath.HumanReadableStringOptions.OmitDevice
                );

                return displayString;
            }

            return "<NULL_KEY>";
        }
    }
}
