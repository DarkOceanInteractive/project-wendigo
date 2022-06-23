using UnityEngine;

namespace ProjectWendigo
{
    public class LanternEvent : MonoBehaviour
    {
        public GameObject[] InteractableItems = new GameObject[0];
        [Tooltip("Name of the quest journal entries, with {progress} and {total} variables")]
        [SerializeField] private string _progressJournalEntryName;

        private void Awake()
        {
            Singletons.Main.Event.On("LanternEvent", this.EnterLanternEvent);
            foreach (GameObject item in this.InteractableItems)
                item.SetActive(false);
        }

        private void EnterLanternEvent()
        {
            Singletons.Main.Notebook.AddJournalEntryByTitle("Lantern messages");
            Singletons.Main.Quest.TryStartQuest("Mine_CollectWritings");
            foreach (GameObject item in this.InteractableItems)
            {
                item.SetActive(true);
                item.SetInteractable();
            }
            LevelMineStateContext.Instance.EnterLanternEvent();
        }

        public void UpdateNotebookQuest(QuestManager.Quest quest)
        {
            string prevEntryTitle = this._progressJournalEntryName
                .Replace("{progress}", $"{quest.Progress - 1}")
                .Replace("{total}", $"{quest.Total}");
            Singletons.Main.Notebook.RemoveJournalEntryByTitle(prevEntryTitle);
            string currEntryTitle = this._progressJournalEntryName
                .Replace("{progress}", $"{quest.Progress}")
                .Replace("{total}", $"{quest.Total}");
            Singletons.Main.Notebook.AddJournalEntryByTitle(currEntryTitle);
        }
    }
}
