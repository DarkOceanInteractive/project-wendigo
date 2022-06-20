using UnityEngine;
using UnityEngine.UI;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewMapsEntryAdapter", menuName = "Notebook/Maps/Adapter")]
    public class MapsEntryAdapter : INotebookElementAdapter
    {
        public override GameObject CreateElement(object value, NotebookElementAdapterOptions options)
        {
            var entry = (MapsCollectionEntry)value;
            var adapterOptions = (MapsEntryAdapterOptions)options;
            GameObject element = Instantiate(adapterOptions.EntryPrefab);
            element.GetComponentInChildren<Text>().text = entry.Title;
            return element;
        }

        public override void OnAfterInsert(GameObject element, object value, GameObject page, NotebookElementAdapterOptions options)
        {
            var entry = (MapsCollectionEntry)value;
            var adapterOptions = (MapsEntryAdapterOptions)options;
            element.GetComponent<Button>().onClick.AddListener(() =>
            {
                Singletons.Main.Sound.Play("notebook_unfolding");
                var entryView = page.transform.parent.parent.Find("MapsEntryView", true);
                entryView.gameObject.SetActive(true);
                var entryViewImage = entryView.GetComponentInChildren<Image>();
                entryViewImage.sprite = entry.Image;
            });
        }

        public override bool ElementFits(GameObject element, GameObject page)
        {
            return page.transform.childCount <= 2;
        }
    }
}
