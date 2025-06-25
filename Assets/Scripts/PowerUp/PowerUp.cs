using UnityEngine;

namespace OZITK
{
    public abstract class PowerUp : MonoBehaviour
    {
        protected Player player;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                player = other.GetComponentInParent<Player>();

                OnPlayerEnter();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerExit();

                player = null;
            }
        }

        /// <summary>
        /// Called when player enters power-up trigger
        /// </summary>
        protected abstract void OnPlayerEnter();
        /// <summary>
        /// Called when player exits power-up trigger
        /// </summary>
        protected abstract void OnPlayerExit();
    }
}
