using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace ProjectWendigo
{
    public class QuestManager : MonoBehaviour
    {
        [System.Serializable]
        public class Quest
        {
            public string Name;
            [SerializeField, ReadOnly] private int _progress;
            public int Progress
            {
                get => this._progress;
                private set => this._progress = value;
            }
            public int Total;
            [SerializeField, ReadOnly] private bool _isStarted = false;

            public UnityEvent<Quest> OnStart;
            public UnityEvent<Quest> OnProgress;
            public UnityEvent<Quest> OnComplete;

            public void Start()
            {
                if (this._isStarted)
                {
                    Debug.LogWarning($"Quest {this.Name} already started");
                    return;
                }
                this._isStarted = true;
                this.Progress = 0;
                this.OnStart?.Invoke(this);
            }

            public void UpdateProgress(int increment)
            {
                if (!this._isStarted)
                {
                    Debug.LogWarning($"Quest {this.Name} not started yet");
                    return;
                }
                this.Progress = Mathf.Min(this.Progress + increment, this.Total);
                this.OnProgress?.Invoke(this);
                if (this.Progress == this.Total)
                    this.OnComplete?.Invoke(this);
            }
        }

        [SerializeField] private Quest[] _quests;

        public Quest GetQuest(string name)
        {
            Quest quest = this._quests.First(quest => quest.Name == name);
            if (quest == null)
                Debug.LogWarning($"Quest `{name}` does not exist");
            return quest;
        }

        public bool TryStartQuest(string name)
        {
            Quest quest = this.GetQuest(name);
            if (quest == null)
                return false;
            quest.Start();
            return true;
        }
        public void StartQuest(string name)
        {
            this.TryStartQuest(name);
        }

        public bool TryUpdateQuestProgress(string name, int increment)
        {
            Quest quest = this.GetQuest(name);
            if (quest == null)
                return false;
            quest.UpdateProgress(increment);
            return true;
        }
        public void IncrementQuestProgress(string name)
        {
            this.TryUpdateQuestProgress(name, 1);
        }

        public bool TryGetQuestProgress(string name, out int progress)
        {
            progress = 0;
            Quest quest = this.GetQuest(name);
            if (quest == null)
                return false;
            progress = quest.Progress;
            return true;
        }
    }
}