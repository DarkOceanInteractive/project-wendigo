using UnityEngine;

namespace ProjectWendigo
{
    public class Interact : MonoBehaviour
    {
        public float Reach = 3f;
        public LayerMask LayerMask;
        [SerializeField] private GameObject _playerHead;
        private Transform _lastHit;

        private Transform GetObjectLookedAt()
        {
            if (Physics.Raycast(this._playerHead.transform.position, this._playerHead.transform.forward, out RaycastHit hitInfo, this.Reach, LayerMask))
                return hitInfo.transform;
            return null;
        }

        private void OnLookAt(GameObject target)
        {
            if (target.TryGetComponent(out AInteractable pickable))
            {
                pickable.OnLookAt(target);
            }
            else
            {
                Debug.LogWarning($"Object looked at doesn't have an AInteractable script attached to it: {target.name}");
            }
        }

        private void OnLookAway(GameObject target)
        {
            if (this._lastHit.TryGetComponent(out AInteractable pickable))
            {
                pickable.OnLookAway(target);
            }
            else
            {
                Debug.LogWarning($"Object looked away from doesn't have an AInteractable script attached to it: {target.name}");
            }
        }

        private void OnInteract(GameObject target)
        {
            if (target.TryGetComponent(out AInteractable pickable))
            {
                pickable.OnInteract(target);
                Singletons.Main.Interface.CloseMessagePanel();
            }
            else
            {
                Debug.LogWarning($"Object interacted with doesn't have an AInteractable script attached to it: {target.name}");
            }
        }

        private void Update()
        {
            Transform lookAt = this.GetObjectLookedAt();
            if (lookAt != null)
            {
                if (this._lastHit == null || this._lastHit != lookAt)
                {
                    if (this._lastHit != null && this._lastHit != lookAt)
                        this.OnLookAway(this._lastHit.gameObject);
                    this._lastHit = lookAt;
                    this.OnLookAt(lookAt.gameObject);
                }
                if (Singletons.Main.Input.PlayerInteracted)
                {
                    this.OnInteract(lookAt.gameObject);
                }
                //Debug.DrawRay(this._playerHead.transform.position, this._playerHead.transform.forward * Reach, Color.red);
            }
            else
            {
                if (this._lastHit != null)
                {
                    this.OnLookAway(this._lastHit.gameObject);
                    this._lastHit = null;
                }
                //Debug.DrawRay(this._playerHead.transform.position, this._playerHead.transform.forward * Reach, Color.green);
            }
        }
    }
}
