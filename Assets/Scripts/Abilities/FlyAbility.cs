using TMPro;
using UnityEngine;

namespace OZITK
{
    public class FlyAbility : Ability
    {
        [SerializeField] private float flyForce = 15;

        private TextMeshProUGUI debugText;

        protected override void OnInit()
        {
            debugText = _user.DebugWindow.CreateDebugText();
            debugText.text = "To fly press: " + GetInputKeyString();
        }
        public override void OnFixedUpdate()
        {
            FlyUp();
        }

        private void FlyUp()
        {
            if (input.action.IsPressed())
                _user.Rigidbody.AddForce(0, flyForce, 0, ForceMode.Force);
        }
    }
}
