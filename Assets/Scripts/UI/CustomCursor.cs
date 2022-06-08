using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectWendigo
{
    public class CustomCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CursorSettings _cursor;

        private void SetCursor()
        {
            Singletons.Main.Input.SetCursor(this._cursor);
        }

        private void ResetCursor()
        {
            Singletons.Main.Input.SetDefaultCursor();
        }

        public void OnDestroy() => this.ResetCursor();

        public void OnDisable() => this.ResetCursor();

        public void OnMouseOver() => this.SetCursor();

        public void OnMouseExit() => this.ResetCursor();

        public void OnPointerEnter(PointerEventData eventData) => this.SetCursor();

        public void OnPointerExit(PointerEventData eventData) => this.ResetCursor();
    }
}
