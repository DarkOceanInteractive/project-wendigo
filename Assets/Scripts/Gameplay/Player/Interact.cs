using UnityEngine;

namespace ProjectWendigo
{
    public class Interact : MonoBehaviour
    {
        public float Reach = 3f;
        public LayerMask LayerMask;
        private bool _hasConnected = false;

        void Update()
        {
            if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo, this.Reach, LayerMask))
            {
                if (!this._hasConnected)
                {
                    Singletons.Main.Interface.OpenMessagePanel();
                    this._hasConnected = true;
                }
                else
                {
                    if (Singletons.Main.Input.PlayerItemPickedUp)
                    {
                        if (hitInfo.transform.TryGetComponent(out AInteractable pickable))
                        {
                            pickable.OnInteract(hitInfo.transform.gameObject);
                            Singletons.Main.Interface.CloseMessagePanel();
                            this._hasConnected = false;
                        }
                    }
                }
            }
            else
            {
                if (this._hasConnected)
                {
                    Singletons.Main.Interface.CloseMessagePanel();
                    this._hasConnected = false;
                }
                Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward) * Reach, Color.green);
            }
        }
    }
}
