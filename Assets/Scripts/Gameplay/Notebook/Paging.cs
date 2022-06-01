using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectWendigo
{
    public class Paging : MonoBehaviour
    {
        [SerializeField] private GameObject[] _pagesPrefabs;
        private int _lastPagePrefabIndex = 0;
        public INotebookElementAdapter Adapter;
        public NotebookElementAdapterOptions AdapterOptions;
        public List<GameObject> _elements = new List<GameObject>();
        public List<List<GameObject>> _pages = new List<List<GameObject>>();

        public void AddElement(object value)
        {
            StartCoroutine(this.AddElement(this.Adapter.CreateElement(value, this.AdapterOptions)));
        }

        private IEnumerator AddElement(GameObject element)
        {
            this._elements.Add(element);
            GameObject lastPage = this.QueryLastPage();
            element.transform.SetParent(lastPage.transform, false);
            if (!this.Adapter.ElementFits(element, lastPage))
                element.transform.SetParent(this.CreateNewPage().transform);
            yield return null;
        }

        public void Clear()
        {
            this._lastPagePrefabIndex = 0;
            this._elements = new List<GameObject>();
            this._pages = new List<List<GameObject>>();
            for (int i = this.transform.childCount; i > 0; --i)
                DestroyImmediate(this.transform.GetChild(0).gameObject);
        }

        private GameObject QueryLastPage()
        {
            if (this._pages.Count > 0)
            {
                var lastPagesContainer = this._pages[this._pages.Count - 1];
                return lastPagesContainer[lastPagesContainer.Count - 1];
            }
            return this.CreateNewPage();
        }

        private GameObject CreateNewPage()
        {
            Debug.Assert(this._pagesPrefabs.Length > 0);
            GameObject newPage = Instantiate(this._pagesPrefabs[this._lastPagePrefabIndex++], this.transform);
            // Create new pages container
            if (this._pages.Count == 0 || this._lastPagePrefabIndex == this._pagesPrefabs.Length)
                this._pages.Add(new List<GameObject>());
            // Add new page to the last pages container
            this._pages[this._pages.Count - 1].Add(newPage);
            this._lastPagePrefabIndex %= this._pagesPrefabs.Length;
            return newPage;
        }
    }
}