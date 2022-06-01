using UnityEditor;
using UnityEngine;

namespace ProjectWendigo
{
    [CustomEditor(typeof(Paging), true)]
    public class PagingEditor : Editor
    {
        private Paging _script;
        private string _elementContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent sapien mi, feugiat vel odio sit amet, placerat vehicula lacus. Aliquam quis commodo ex.";

        private void Awake()
        {
            this._script = (Paging)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            this._elementContent = EditorGUILayout.TextField(this._elementContent);
            if (GUILayout.Button("Add element"))
            {
                this._script.AddElement(this._elementContent);
            }
            if (GUILayout.Button("Clear"))
            {
                this._script.Clear();
            }
        }
    }
}