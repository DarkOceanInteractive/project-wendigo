using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectWendigo
{
    public class NotebookPaging : MonoBehaviour
    {
        public UnityEvent OnAddPage;

        [System.Serializable]
        public struct PageAdapterInfo
        {
            public INotebookPageAdapter adapter;
            public INotebookPageAdapterOptions options;
        }
        [SerializeField] private PageAdapterInfo[] _pagesAdapters;
        private int _currentPageAdapterIndex = -1;
        private int _nextPageAdapterIndex = 0;
        public int CurrentPageGroup { get; private set; } = 0;
        public int TotalPageGroups => this._pageGroups.Count;
        public INotebookElementAdapter ElementAdapter;
        public NotebookElementAdapterOptions ElementAdapterOptions;
        private List<GameObject> _elements = new List<GameObject>();
        private List<List<GameObject>> _pageGroups = new List<List<GameObject>>();

        public void AddElement(object value)
        {
            this.AddElement(this.ElementAdapter.CreateElement(value, this.ElementAdapterOptions), value);
        }

        private void AddElement(GameObject element, object value)
        {
            this._elements.Add(element);
            GameObject lastPage = this.QueryLastPage();
            PageAdapterInfo pageAdapterInfo = this._pagesAdapters[this._currentPageAdapterIndex];
            pageAdapterInfo.adapter.AddChild(lastPage, element, value, pageAdapterInfo.options);
            bool sectionActiveState = this.transform.parent.gameObject.activeSelf;
            this.transform.parent.gameObject.SetActive(true);
            if (!this.ElementAdapter.ElementFits(element, lastPage))
            {
                lastPage = this.CreateNewPage();
                pageAdapterInfo.adapter.AddChild(lastPage, element, value, pageAdapterInfo.options);
            }
            this.transform.parent.gameObject.SetActive(sectionActiveState);
            this.ElementAdapter.OnAfterInsert(element, value, lastPage, this.ElementAdapterOptions);

        }

        public void SetCurrentPageGroup(int index)
        {
            if (index < 0 || index > this.TotalPageGroups - 1)
                throw new System.Exception($"Cannot change page group outside of bounds 0-{this._pageGroups.Count}");
            foreach (GameObject page in this._pageGroups[this.CurrentPageGroup])
                page.SetActive(false);
            this.CurrentPageGroup = index;
            foreach (GameObject page in this._pageGroups[this.CurrentPageGroup])
                page.SetActive(true);
        }

        private void UpdatePageDisplay()
        {
            for (int i = 0; i < this._pageGroups.Count; ++i)
                foreach (GameObject page in this._pageGroups[i])
                    page.SetActive(i == this.CurrentPageGroup);
        }

        public void Clear()
        {
            this._currentPageAdapterIndex = -1;
            this._nextPageAdapterIndex = 0;
            this.CurrentPageGroup = 0;
            this._elements = new List<GameObject>();
            this._pageGroups = new List<List<GameObject>>();
            for (int i = this.transform.childCount; i > 0; --i)
                DestroyImmediate(this.transform.GetChild(0).gameObject);
        }

        private GameObject QueryLastPage()
        {
            if (this._pageGroups.Count > 0)
            {
                var lastPagesContainer = this._pageGroups[this._pageGroups.Count - 1];
                return lastPagesContainer[lastPagesContainer.Count - 1];
            }
            return this.CreateNewPage();
        }

        private GameObject CreateNewPage()
        {
            Debug.Assert(this._pagesAdapters.Length > 0);
            this._currentPageAdapterIndex = (this._currentPageAdapterIndex + 1) % this._pagesAdapters.Length;
            PageAdapterInfo pageAdapterInfo = this._pagesAdapters[this._nextPageAdapterIndex];
            GameObject newPage = pageAdapterInfo.adapter.CreatePage(pageAdapterInfo.options);
            newPage.transform.SetParent(this.transform, false);
            // Create new pages container
            if (this._nextPageAdapterIndex == 0)
                this._pageGroups.Add(new List<GameObject>());
            // Add new page to the last pages container
            this._pageGroups[this._pageGroups.Count - 1].Add(newPage);
            this._nextPageAdapterIndex = (this._nextPageAdapterIndex + 1) % this._pagesAdapters.Length;
            this.OnAddPage?.Invoke();
            this.UpdatePageDisplay();
            return newPage;
        }
    }
}
