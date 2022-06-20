using UnityEngine;

namespace ProjectWendigo
{
    public class PhoneInteraction : AInteractable
    {
        [SerializeField] private PhoneStateContext _phoneStateContext;

        public override void OnLookAt(GameObject target)
        {
            if (this._phoneStateContext.IsInState<PhoneStates.Ringing>())
            {
                var options = new MessagePanelOptions
                {
                    Text = $"- Press {Singletons.Main.Input.GetBinding("Player/Interact")} to pick up -",
                    Location = MessagePanelLocation.BottomCenter
                };
                Singletons.Main.Interface.OpenMessagePanel(options);
            }
        }

        public override void OnLookAway(GameObject target)
        {
            if (this._phoneStateContext.IsInState<PhoneStates.Ringing>())
            {
                Singletons.Main.Interface.CloseMessagePanel();
            }
        }

        public override void OnInteract(GameObject target)
        {
            if (this._phoneStateContext.IsInState<PhoneStates.Ringing>())
            {
                target.transform.parent.GetComponent<PhoneStateContext>().GoToCalling();
            }
        }
    }
}