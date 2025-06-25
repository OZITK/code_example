using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OZITK
{
    public class JumpAbility : Ability
    {
        [SerializeField] private float jumpForce = 10;

        public float JumpMulti = 1f;

        private TextMeshProUGUI debugText;

        private GroundDetectionAbility groundDetectionAbility; //Recommended but optional dependency for ground checks
        private bool usingGroundDetection = false;
        private float lastTimeJump = 0;
        private bool jumpedRecently => Time.time - lastTimeJump < 0.5f;

        protected override void OnInit()
        {
            debugText = _user.DebugWindow.CreateDebugText();
            groundDetectionAbility = _user.GetAbility<GroundDetectionAbility>();
            usingGroundDetection = groundDetectionAbility != null;
            input.action.performed += PlayerJump;
        }
        private void OnDestroy()
        {
            input.action.performed -= PlayerJump;
        }

        public override void OnUpdate()
        {
            debugText.text = $"Jumped recently: {jumpedRecently}\nJump force: x{JumpMulti}";
        }

        private void PlayerJump(InputAction.CallbackContext obj)
        {
            if (!usingGroundDetection || groundDetectionAbility.IsGrounded)
            {
                lastTimeJump = Time.time;
                _user.Rigidbody.AddForce(0, jumpForce * JumpMulti, 0, ForceMode.Impulse);
            }
        }
    }
}
