using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonPanelTemp : MonoBehaviour
{
    [SerializeField]
    private Button yesButton;
    [SerializeField]
    private Button noButton;

    private static ButtonPanelTemp instanced;
    public static ButtonPanelTemp instance => instanced;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instanced == null)
            instanced = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetUp(Action yesEvent, Action noEvent)
    {
        yesButton.onClick.AddListener(( ) =>
        {
            audioSource.Play();
            DialogueManager.instance.dialText.text = string.Empty;
            DialogueManager.instance.isDialogueActive = false;
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);
            yesEvent?.Invoke();
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
        });
        noButton.onClick.AddListener(() =>
        {
            audioSource.Play();
            DialogueManager.instance.dialText.text = string.Empty;
            noEvent?.Invoke();
        });

        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
    }
}