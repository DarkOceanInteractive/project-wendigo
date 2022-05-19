using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectWendigo
{
    public class InterfaceManager : MonoBehaviour
    {
        public GameObject MessagePanel;
        public Text MessageText;

        public void OpenMessagePanel(string text = null) {
            if (text != null) {
                this.MessageText.text = text;
            }
            this.MessagePanel.SetActive(true);
        }

        public void CloseMessagePanel() {
            this.MessagePanel.SetActive(false);
        }
    }
}
