using UnityEngine;

namespace ProjectWendigo
{
    public abstract class INotebookPageAdapter : ScriptableObject
    {
        public virtual GameObject CreatePage(INotebookPageAdapterOptions options)
        {
            return Instantiate(options.PagePrefab);
        }

        public abstract void AddChild(GameObject page, GameObject child, object entry, INotebookPageAdapterOptions options);
    }
}