using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewDefaultNotebookPageAdapter", menuName = "Notebook/Default/PageAdapter")]
    public class DefaultNotebookPageAdapter : INotebookPageAdapter
    {
        public override void AddChild(GameObject page, GameObject child, object entry, INotebookPageAdapterOptions options)
        {
            child.transform.SetParent(page.transform, false);
        }
    }
}