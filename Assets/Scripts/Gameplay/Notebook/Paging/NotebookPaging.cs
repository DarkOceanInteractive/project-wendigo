using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectWendigo
{
    public class NotebookPaging : MonoBehaviour
    {
        public UnityEvent OnAddPage;

        [SerializeField] private GameObject[] _pagesPrefabs;
        private int _lastPagePrefabIndex = 0;
        public int CurrentPageGroup { get; private set; } = 0;
        public int TotalPageGroups => this._pageGroups.Count;
        public INotebookElementAdapter Adapter;
        public NotebookElementAdapterOptions AdapterOptions;
        private List<GameObject> _elements = new List<GameObject>();
        private List<List<GameObject>> _pageGroups = new List<List<GameObject>>();

        public void AddElement(object value)
        {
            this.AddElement(this.Adapter.CreateElement(value, this.AdapterOptions));
        }

        private void AddElement(GameObject element)
        {
            this._elements.Add(element);
            GameObject lastPage = this.QueryLastPage();
            element.transform.SetParent(lastPage.transform, false);
            if (!this.Adapter.ElementFits(element, lastPage))
                element.transform.SetParent(this.CreateNewPage().transform);
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
            this._lastPagePrefabIndex = 0;
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
            Debug.Assert(this._pagesPrefabs.Length > 0);
            GameObject newPage = Instantiate(this._pagesPrefabs[this._lastPagePrefabIndex], this.transform);
            // Create new pages container
            if (this._lastPagePrefabIndex == 0)
                this._pageGroups.Add(new List<GameObject>());
            // Add new page to the last pages container
            this._pageGroups[this._pageGroups.Count - 1].Add(newPage);
            this._lastPagePrefabIndex = (this._lastPagePrefabIndex + 1) % this._pagesPrefabs.Length;
            this.OnAddPage?.Invoke();
            this.UpdatePageDisplay();
            return newPage;
        }
    }
}
