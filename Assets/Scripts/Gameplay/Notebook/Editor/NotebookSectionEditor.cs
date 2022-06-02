using UnityEditor;
using UnityEngine;

namespace ProjectWendigo
{
    [CustomEditor(typeof(NotebookSection), true)]
    public class NotebookSectionEditor : Editor
    {
        private NotebookSection _section;
        private string _elementContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent sapien mi, feugiat vel odio sit amet, placerat vehicula lacus. Aliquam quis commodo ex.";

        private void Awake()
        {
            this._section = (NotebookSection)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            this._elementContent = EditorGUILayout.TextField(this._elementContent);
            if (GUILayout.Button("Add element"))
            {
                this._section.AddElement(new JournalEntry { content = this._elementContent });
            }
            if (GUILayout.Button("Clear"))
            {
            }
        }
    }
}
