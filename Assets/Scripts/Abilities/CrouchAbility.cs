using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OZITK
{
    public class CrouchAbility : Ability
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float crouchOffset = 0.5f;

        private Vector3 initCameraPos;

        private TextMeshProUGUI debugText;
        private bool isCrouching = false;

        protected override void OnInit()
        {
            initCameraPos = cameraTransform.localPosition;
            debugText = _user.DebugWindow.CreateDebugText();
            debugText.text = $"To toggle crouch press: {GetInputKeyString()}";
            input.action.performed += Crouch;
        }
        private void OnDestroy()
        {
            input.action.performed -= Crouch;
        }

        private void Crouch(InputAction.CallbackContext obj)
        {
            isCrouching = !isCrouching;
            if (isCrouching)
            {
                cameraTransform.localPosition = initCameraPos + Vector3.down * crouchOffset;
            }
            else
            {
                cameraTransform.localPosition = initCameraPos;
            }
        }
    }
}
