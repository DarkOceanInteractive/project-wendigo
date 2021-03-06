using UnityEngine;
using UnityEngine.UI;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewJournalEntryAdapter", menuName = "Notebook/Journal/Adapter")]
    public class JournalEntryAdapter : INotebookElementAdapter
    {
        public override GameObject CreateElement(object value, NotebookElementAdapterOptions options)
        {
            var entry = (JournalCollectionEntry)value;
            var adapterOptions = (JournalEntryAdapterOptions)options;
            GameObject element = Instantiate(adapterOptions.EntryPrefab);
            element.GetComponentInChildren<Text>().text = entry.Content;
            return element;
        }

        public override bool ElementFits(GameObject element, GameObject page)
        {
            bool pageActiveState = page.activeSelf;
            bool notebookActiveState = Singletons.Main.Notebook.Notebook.activeSelf;
            // Force-enable page while calculating element height, otherwise it would be 0
            Singletons.Main.Notebook.Notebook.SetActive(true);
            page.SetActive(true);
            // Force rebuild layout to get up-to-date dimensions
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)page.transform);
            var text = element.GetComponentInChildren<Text>();
            // Restore previous active state
            page.SetActive(pageActiveState);
            Singletons.Main.Notebook.Notebook.SetActive(notebookActiveState);
            return text.rectTransform.rect.height >= text.preferredHeight;
        }
    }
}
