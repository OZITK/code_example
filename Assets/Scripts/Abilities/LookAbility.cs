using UnityEngine;

namespace OZITK
{
    public class LookAbility : Ability
    {
        [SerializeField] private Transform cameraTransform;

        [Tooltip("Sensitivity multiplier for mouse/gamepad look input")]
        [SerializeField] private float lookSensitivity = 0.03f;
        [Tooltip("Maximum vertical look angle in degrees (up/down limit)")]
        [SerializeField] private float pitchClamp = 80f;

        private float cameraPitch = 0f;

        protected override void OnInit()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public override void OnUpdate()
        {
            PlayerLook();
        }

        /// <summary>
        /// Applies horizontal and vertical rotation based on input
        /// </summary>
        private void PlayerLook()
        {
            // Read input and scale by sensitivity (consider adding deltaTime for smoothness on gamepad)
            Vector2 lookDelta = input.action.ReadValue<Vector2>() * lookSensitivity;

            _user.MyTransform.Rotate(Vector3.up * lookDelta.x);

            cameraPitch -= lookDelta.y;
            cameraPitch = Mathf.Clamp(cameraPitch, -pitchClamp, pitchClamp);
            cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
        }
    }
}
