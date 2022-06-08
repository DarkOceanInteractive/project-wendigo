using UnityEngine;
using UnityEngine.UI;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewArchiveCategoryAdapter", menuName = "Notebook/Archive/Category Adapter")]
    public class ArchiveCategoryAdapter : INotebookElementAdapter
    {
        public override GameObject CreateElement(object value, NotebookElementAdapterOptions options)
        {
            var entry = (ArchiveCategoryEntry)value;
            var adapterOptions = (ArchiveCategoryAdapterOptions)options;
            GameObject element = Instantiate(adapterOptions.CategoryPrefab);
            element.transform.Find("Title").GetComponent<Text>().text = entry.Title;
            return element;
        }

        public override bool ElementFits(GameObject element, GameObject page)
        {
            return true;
        }
    }
}
