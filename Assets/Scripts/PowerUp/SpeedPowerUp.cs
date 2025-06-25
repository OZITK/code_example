using UnityEngine;

namespace OZITK
{
    public class SpeedPowerUp : DurationPowerUp
    {
        [Tooltip("Multiplier applied to movement and jump abilities")]
        [SerializeField] private float value = 1.5f;

        private MovementAbility movementAbility;
        private JumpAbility jumpAbility;

        private bool usingMovementAbility;
        private bool usingJumpAbility;

        protected override void OnPowerUpStart()
        {
            movementAbility = player.GetAbility<MovementAbility>();
            jumpAbility = player.GetAbility<JumpAbility>();
            usingMovementAbility = movementAbility != null;
            usingJumpAbility = jumpAbility != null;
            OnPowerUpUpdate();
        }
        protected override void OnPowerUpUpdate()
        {
            // Prevents overwriting stronger active buffs
            if (usingMovementAbility && movementAbility.VelocityMulti < value)
                movementAbility.VelocityMulti = value;
            if (usingJumpAbility && jumpAbility.JumpMulti < value)
                jumpAbility.JumpMulti = value;
        }

        protected override void OnPowerUpEnd()
        {
            if (usingMovementAbility)
                movementAbility.VelocityMulti = 1;
            if (usingJumpAbility)
                jumpAbility.JumpMulti = 1;
        }

    }
}
