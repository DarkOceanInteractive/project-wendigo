using UnityEngine;
using UnityEngine.UI;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewArchiveEntryAdapter", menuName = "Notebook/Archive/Adapter")]
    public class ArchiveEntryAdapter : INotebookElementAdapter
    {
        public override GameObject CreateElement(object value, NotebookElementAdapterOptions options)
        {
            var entry = (ArchiveCollectionEntry)value;
            var adapterOptions = (ArchiveEntryAdapterOptions)options;
            GameObject element = Instantiate(adapterOptions.EntryPrefab);
            var previewImage = element.transform.Find("ImageFrame/Image", true).GetComponent<Image>();
            if (entry.Image != null)
            {
                previewImage.gameObject.SetActive(true);
                previewImage.sprite = entry.Image;
            }
            else
            {
                previewImage.gameObject.SetActive(false);
            }
            element.transform.Find("Title").GetComponentInChildren<Text>().text = entry.Title;
            element.GetComponentInChildren<Text>().text = entry.Title;
            return element;
        }

        public override void OnAfterInsert(GameObject element, object value, GameObject page, NotebookElementAdapterOptions options)
        {
            var entry = (ArchiveCollectionEntry)value;
            var adapterOptions = (ArchiveEntryAdapterOptions)options;
            element.GetComponent<Button>().onClick.AddListener(() =>
            {
                var entryView = page.transform.parent.parent.Find("ArchiveEntryView", true);
                entryView.gameObject.SetActive(true);
                var photoFrame = entryView.Find("PhotoFrame", true);
                var entryViewImage = photoFrame.transform.Find("Mask/Image", true).GetComponentInChildren<Image>();
                var entryViewTitle = entryView.Find("Title").GetComponentInChildren<Text>();
                var entryViewDescription = entryView.Find("Description").GetComponentInChildren<Text>();
                if (entry.Image)
                {
                    photoFrame.gameObject.SetActive(true);
                    entryViewImage.sprite = entry.Image;
                }
                else
                {
                    photoFrame.gameObject.SetActive(false);
                }
                entryViewTitle.text = entry.Title;
                entryViewDescription.text = entry.Description;
            });
        }

        public override bool ElementFits(GameObject element, GameObject page)
        {
            return true;
        }
    }
}
