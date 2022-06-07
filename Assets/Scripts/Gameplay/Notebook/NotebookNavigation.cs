using UnityEngine;

public class NotebookNavigation : MonoBehaviour
{
    private void Awake()
    {
        foreach (Transform child in this.transform)
            child.gameObject.SetActive(false);
        this.transform.GetChild(0)?.gameObject.SetActive(true);
    }

    private Transform GetActiveSection()
    {
        Transform[] transforms = this.transform.GetComponentsInChildren<Transform>(false);
        return transforms.Length > 1 ? transforms[1] : null;
    }

    public void GoToSection(GameObject section)
    {
        this.GetActiveSection()?.gameObject.SetActive(false);
        section.SetActive(true);
    }
}
