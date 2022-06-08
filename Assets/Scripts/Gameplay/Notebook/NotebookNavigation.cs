using UnityEngine;
using UnityEngine.UI;

public class NotebookNavigation : MonoBehaviour
{
    [SerializeField] GameObject _bookmarks;
    [SerializeField] GameObject _sections;

    private void Awake()
    {
        foreach (Transform child in this._sections.transform)
            child.gameObject.SetActive(false);
        foreach (Transform child in this._bookmarks.transform)
            child.Find("InnerPart", true).gameObject.SetActive(false);
        this.GoToSection(this._sections.transform.GetChild(0).gameObject);
    }

    public Transform GetActiveSection()
    {
        Transform[] transforms = this._sections.transform.GetComponentsInChildren<Transform>(false);
        return transforms.Length > 1 ? transforms[1] : null;
    }

    private Transform GetSectionBookmark(GameObject section)
    {
        return this._bookmarks.transform.Find($"{section.name}Bookmark");
    }

    private void EnableBookmark(GameObject bookmark, bool enabled = true)
    {
        bookmark.transform.Find("InnerPart", true)?.gameObject.SetActive(enabled);
        bookmark.transform.GetComponent<Button>().interactable = !enabled;
    }

    private void EnableSection(GameObject section, bool enabled = true)
    {
        section.SetActive(enabled);
        Transform bookmark = this.GetSectionBookmark(section.gameObject);
        if (bookmark != null)
            this.EnableBookmark(bookmark.gameObject, enabled);
    }

    public void GoToSection(GameObject section)
    {
        // Disable previous section
        Transform activeSection = this.GetActiveSection();
        if (activeSection != null)
            this.EnableSection(activeSection.gameObject, false);
        // Enable new section
        this.EnableSection(section.gameObject, true);
    }
}
