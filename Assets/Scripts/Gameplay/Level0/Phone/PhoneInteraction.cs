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
                Singletons.Main.Interface.OpenMessagePanel("- Press F to pick up -");
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