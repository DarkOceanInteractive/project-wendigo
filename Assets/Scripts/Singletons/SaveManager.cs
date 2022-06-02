using UnityEngine;
using UnityEngine.Events;

public class SaveManager : MonoBehaviour
{
    public UnityEvent OnSave;

    [ContextMenu("Save")]
    public void Save()
    {
        this.OnSave?.Invoke();
    }
}
