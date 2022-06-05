using UnityEngine;
using UnityEngine.UI;
using ProjectWendigo.Database.Extensions.Reference;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewArchiveEntryAdapter", menuName = "Notebook/Archive/Adapter")]
    public class ArchiveEntryAdapter : INotebookElementAdapter
    {
        private ArchiveCategoryEntry GetCategory(ArchiveCollectionEntry element, ArchiveEntryAdapterOptions options)
        {
            ArchiveCategoryEntry collectionEntry = element.ResolveReference<ArchiveCategoryEntry>(new ReferenceToOne
            {
                KeyName = "CategoryTitle",
                ForeignKeyName = "Title",
                ForeignTable = options.Categories
            });
            Debug.Assert(collectionEntry != null, $"No category entry found matching given title");
            return collectionEntry;
        }

        public override GameObject CreateElement(object value, NotebookElementAdapterOptions options)
        {
            var entry = (ArchiveCollectionEntry)value;
            var adapterOptions = (ArchiveEntryAdapterOptions)options;
            var category = this.GetCategory(entry, adapterOptions);
            //GameObject element = Instantiate(adapterOptions.TextElementPrefab);
            //element.GetComponentInChildren<Text>().text = entry.content;
            return new GameObject();
        }

        public override bool ElementFits(GameObject element, GameObject page)
        {
            return true;
            bool pageActiveState = page.activeSelf;
            // Force-enable page while calculating element height, otherwise it would be 0
            page.SetActive(true);
            // Force rebuild layout to get up-to-date dimensions
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)page.transform);
            var text = element.GetComponentInChildren<Text>();
            // Restore previosu active state
            page.SetActive(pageActiveState);
            return text.rectTransform.rect.height >= text.preferredHeight;
        }
    }
}
