using UnityEngine;

namespace ProjectWendigo
{
    public abstract class INotebookElementAdapter : ScriptableObject
    {
        public abstract GameObject CreateElement(object value, NotebookElementAdapterOptions options);

        public abstract bool ElementFits(GameObject element, GameObject page);
    }
}