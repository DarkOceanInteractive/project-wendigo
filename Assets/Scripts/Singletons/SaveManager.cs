using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class SaveManager : MonoBehaviour
{
    public UnityEvent OnSave;

    public string GetSavePath(string filepath)
    {
        return Path.Combine(Application.persistentDataPath, filepath);
    }

    [ContextMenu("Save")]
    public void Save()
    {
        this.OnSave?.Invoke();
    }
}
