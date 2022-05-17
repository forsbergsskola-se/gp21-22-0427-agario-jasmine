using UnityEngine;
using UnityEngine.UI;

namespace OpenWord_MMO {
    public class UIHandler : MonoBehaviour {
        [SerializeField] private Text storyText;
        [SerializeField] private int storyFontSize;
        [SerializeField] private Text uxHelpText;
        [SerializeField] private InputField inputField;

        private void OnEnable() {
            OpenWordClient.OnErrorMessageReceived += ShowErrorMessage;
            OpenWordClient.OnMessageReceived += DisplayMessage;
        }

        private void OnDisable() {
            OpenWordClient.OnErrorMessageReceived -= ShowErrorMessage;
            OpenWordClient.OnMessageReceived -= DisplayMessage;
        }

        private void ShowErrorMessage(string errorMessage) {
            uxHelpText.text = errorMessage;
        }

        private void DisplayMessage(string message) {
            storyText.fontSize = storyFontSize;
            storyText.text = message;
            HideDescription();
            ClearInputFieldText();
        }

        private void HideDescription() {
            uxHelpText.text = "";
        }

        private void ClearInputFieldText() {
            inputField.text = "";
        }
    }
}
