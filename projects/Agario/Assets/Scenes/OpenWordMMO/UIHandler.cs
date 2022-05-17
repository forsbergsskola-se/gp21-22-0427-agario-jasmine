using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {
    [SerializeField] private Text storyText;
    [SerializeField] private Text uxHelpText;

    //Vill invoka att nytt meddelande fåtts
    //Om error message inte är none invoka show error message
    //Annars uppdatera story och göm error message

    private void OnEnable() {
        OpenWordClient.OnErrorMessageReceived += ShowErrorMessage;
    }

    private void OnDisable() {
        OpenWordClient.OnErrorMessageReceived -= ShowErrorMessage;

    }

    public void HideDescription() {
        uxHelpText.text = "";
    }

    private void ShowErrorMessage(string errorMessage) {
        uxHelpText.text = errorMessage;
    }

}
