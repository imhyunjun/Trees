using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonPanelTemp : MonoBehaviour
{
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;

    private static ButtonPanelTemp instance;
    public static ButtonPanelTemp Instance => instance;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
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
            DialogueManager.Instance.dialText.text = string.Empty;
            DialogueManager.Instance.isDialogueActive = false;
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);
            yesEvent?.Invoke();
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
        });
        noButton.onClick.AddListener(() =>
        {
            audioSource.Play();
            DialogueManager.Instance.dialText.text = string.Empty;
            noEvent?.Invoke();
        });

        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
    }
}