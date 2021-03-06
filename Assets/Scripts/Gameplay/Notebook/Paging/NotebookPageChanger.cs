using UnityEngine;
using UnityEngine.UI;

namespace ProjectWendigo
{
    [ExecuteAlways]
    public class NotebookPageChanger : MonoBehaviour
    {
        [SerializeField] private GameObject _previousButton;
        [SerializeField] private GameObject _nextButton;
        [SerializeField] private NotebookPaging _pagingScript;

        private void Awake()
        {
            Debug.Assert(this._pagingScript != null || this.TryGetComponent(out this._pagingScript));
            this.UpdateControlsDisplay();
        }

        public void UpdateControlsDisplay()
        {
            this._previousButton.SetActive(this._pagingScript.CurrentPageGroup > 0);
            this._nextButton.SetActive(this._pagingScript.CurrentPageGroup < this._pagingScript.TotalPageGroups - 1);
        }

        public void GoToPreviousPage()
        {
            this._pagingScript.SetCurrentPageGroup(this._pagingScript.CurrentPageGroup - 1);
            this.UpdateControlsDisplay();
            Singletons.Main.Sound.Play("notebook_swap_page");
        }

        public void GoToNextPage()
        {
            this._pagingScript.SetCurrentPageGroup(this._pagingScript.CurrentPageGroup + 1);
            this.UpdateControlsDisplay();
            Singletons.Main.Sound.Play("notebook_swap_page");
        }
    }
}