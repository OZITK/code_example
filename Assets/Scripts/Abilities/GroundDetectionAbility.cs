using TMPro;
using UnityEngine;

namespace OZITK
{
    public class GroundDetectionAbility : Ability
    {
        [Tooltip("Layer considered as walkable ground")]
        [SerializeField] private LayerMask groundLayer;
        [Tooltip("Radius for spherecast ground check")]
        [SerializeField] private float groundCheckRadius = 0.5f;
        [Tooltip("Distance below player to check for ground")]
        [SerializeField] private float groundCheckRange = 0.2f;
        [Tooltip("Max slope angle considered walkable")]
        [SerializeField] private float maxSlopeAngle = 35f;

        public bool IsGrounded { get; private set; } // True if player is standing on ground
        public bool IsGroundAhead { get; private set; } // True if ground exists ahead in movement direction
        public Vector3 GroundNormal { get; private set; } // Surface normal of current ground

        private TextMeshProUGUI debugText;

        protected override void OnInit()
        {
            debugText = _user.DebugWindow.CreateDebugText();
        }

        public override void OnUpdate()
        {
            debugText.text = $"Grounded: {IsGrounded}";
        }

        public override void OnFixedUpdate()
        {
            IsGrounded = GetGroundNormal();
        }
        /// <summary>
        /// Detects ground below player and stores its normal
        /// </summary>
        private bool GetGroundNormal()
        {
            RaycastHit hit;
            if (Physics.SphereCast(_user.MyTransform.position + Vector3.up * (groundCheckRadius + groundCheckRange / 2f), groundCheckRadius,
                Vector3.down, out hit, groundCheckRange, groundLayer))
            {
                GroundNormal = hit.normal;
                //Debug.DrawRay(hit.point, hit.normal, Color.red, 1);
                return true;
            }

            GroundNormal = Vector3.up;
            return false;
        }

        /// <summary>
        /// Checks if ground exists in direction of movement
        /// </summary>
        public void UpdateIsGroundAhead(Vector3 velocity)
        {
            RaycastHit hit;
            Vector3 origin = transform.position + Vector3.up + velocity;
            IsGroundAhead = Physics.Raycast(origin, Vector3.down, out hit, 2f, groundLayer);
        }
        /// <summary>
        /// Returns true if ground slope exceeds max allowed
        /// </summary>
        public bool IsSlopeTooSteep()
        {
            float angle = Vector3.Angle(GroundNormal, Vector3.up);
            return angle > maxSlopeAngle;
        }
    }
}
