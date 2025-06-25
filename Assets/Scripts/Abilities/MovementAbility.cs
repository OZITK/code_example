using TMPro;
using UnityEngine;

namespace OZITK
{
    public class MovementAbility : Ability
    {
        [SerializeField] private float moveSpeed = 0.3f;
        [SerializeField] private float maxMoveSpeed = 5;

        [Tooltip("Movement speed multiplier(e.g.buffs, debuffs)")]
        public float VelocityMulti = 1f;

        private TextMeshProUGUI debugText;

        public Vector3 Velocity { get; private set; } = Vector3.zero;
        private GroundDetectionAbility groundDetectionAbility; //Recommended but optional dependency for ground checks
        private bool usingGroundDetection = false;

        protected override void OnInit()
        {
            debugText = _user.DebugWindow.CreateDebugText();
            groundDetectionAbility = _user.GetAbility<GroundDetectionAbility>();
            usingGroundDetection = groundDetectionAbility != null;
        }

        public override void OnUpdate()
        {
            debugText.text = $"Velocity: {Velocity.ToString("F1")}\nSpeed: x{VelocityMulti}";
        }

        public override void OnFixedUpdate()
        {
            Move();
        }
        /// <summary>
        /// Calculates and applies movement based on input and ground checks if possible
        /// </summary>
        private void Move()
        {
            Vector2 inputValue = input.action.ReadValue<Vector2>();

            // Converts 2D input into 3D movement direction relative to player orientation
            Vector3 moveDirection = _user.MyTransform.right * inputValue.x + _user.MyTransform.forward * inputValue.y;
            Velocity = moveDirection.normalized;

            if (usingGroundDetection)
            {
                if (groundDetectionAbility.IsGrounded)
                {
                    if (groundDetectionAbility.IsSlopeTooSteep())
                    {
                        Velocity = Vector3.zero;
                    }
                    else
                    {
                        Velocity = Vector3.ProjectOnPlane(Velocity, groundDetectionAbility.GroundNormal).normalized * moveSpeed;

                        // Checks if there's ground in front to avoid stepping off a ledge
                        groundDetectionAbility.UpdateIsGroundAhead(_user.Rigidbody.linearVelocity + Velocity);

                        if (!groundDetectionAbility.IsGroundAhead)
                        {
                            Velocity = Vector3.zero;
                        }
                    }
                }
                else
                {
                    Velocity *= maxMoveSpeed; //Set to max speed while airborne to allow jumping from edges
                }
            }
            else
            {
                Velocity *= moveSpeed;
            }

            Velocity *= VelocityMulti;

            Velocity += _user.Rigidbody.linearVelocity;

            Vector3 velocity = Velocity;

            velocity.y = 0;
            // Limits final velocity to prevent excessive speed
            velocity = Vector3.ClampMagnitude(Velocity, maxMoveSpeed * VelocityMulti);
            velocity.y = Velocity.y;

            Velocity = velocity;

            _user.Rigidbody.linearVelocity = Velocity;
        }
    }
}
