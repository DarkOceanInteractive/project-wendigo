using System.Collections;
using UnityEngine;

namespace ProjectWendigo
{
    public class DepartureEvent : MonoBehaviour
    {
        public float PhoneCallTriggerDelay = 5f;
        public GameObject PhoneInteractableCollider;
        public PhoneStateContext PhoneStateContext;
        public GameObject CarKeyInteractableCollider;
        public GameObject[] InteractableItems = new GameObject[0];

        private void Awake()
        {
            Singletons.Main.Quest.TryStartQuest("Intro_CollectLoreItems");
        }

        private IEnumerator TriggerPhoneRingAfterDelay()
        {
            yield return new WaitForSeconds(this.PhoneCallTriggerDelay);
            this.PhoneStateContext.GoToRing();
            this.PhoneInteractableCollider.SetInteractable();
            this.PhoneStateContext.OnChangeState.AddListener((ctx, _) =>
            {
                if (ctx.IsInState<PhoneStates.Calling>())
                    this.OnCallAnswered();
            });
        }

        public void OnAllLoreItemsCollected()
        {
            StartCoroutine(this.TriggerPhoneRingAfterDelay());
        }

        public void OnCallAnswered()
        {
            Singletons.Main.Quest.TryStartQuest("Intro_CollectGear");
            foreach (GameObject item in this.InteractableItems)
                item.SetInteractable();
        }

        public void OnAllGearCollected()
        {
            this.CarKeyInteractableCollider.SetInteractable();
        }
    }
}