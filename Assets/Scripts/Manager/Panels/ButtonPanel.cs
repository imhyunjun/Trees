using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonPanel : PanelSingletone<ButtonPanel>
{
    [SerializeField]
    private Button yesButton;
    [SerializeField]
    private Button noButton;

    public void SetUp(Action yesEvent, Action noEvent)
    {
        yesButton.onClick.AddListener(() =>
        {
            SoundManager.PlaySFX("Click_3");
            DialogueManager.instance.dialText.text = string.Empty;
            DialoguePanel.instance.Hide();
            yesEvent?.Invoke();
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
            Hide();
        });
        noButton.onClick.AddListener(() =>
        {
            SoundManager.PlaySFX("Click_3");
            DialogueManager.instance.dialText.text = string.Empty;
            noEvent?.Invoke();
        });

        Show();
    }
}
