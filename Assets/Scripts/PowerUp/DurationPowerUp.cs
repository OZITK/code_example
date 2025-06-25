using System.Collections;
using UnityEngine;

namespace OZITK
{
    public abstract class DurationPowerUp : PowerUp
    {
        [Tooltip("Duration of the power-up effect in seconds")]
        [SerializeField] private float duration = 1;

        protected override void OnPlayerEnter()
        {
            player.StartCoroutine(PowerUpLoop());
        }

        protected override void OnPlayerExit()
        {

        }
        /// <summary>
        /// Called once at start of power-up
        /// </summary>
        protected abstract void OnPowerUpStart();
        /// <summary>
        /// Called every frame during power-up
        /// </summary>
        protected abstract void OnPowerUpUpdate();
        /// <summary>
        /// Called once when power-up ends
        /// </summary>
        protected abstract void OnPowerUpEnd();

        /// <summary>
        /// Coroutine managing duration and periodic updates
        /// </summary>
        private IEnumerator PowerUpLoop()
        {
            OnPowerUpStart();
            gameObject.SetActive(false);
            float timer = 0;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                yield return null;
                OnPowerUpUpdate();
            }
            OnPowerUpEnd();
        }
    }
}
