using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePanel : PanelSingletone<DialoguePanel>
{
    public bool IsDialogueOn() =>  transform.GetChild(0).gameObject.activeSelf || transform.GetChild(1).gameObject.activeSelf; // 대화창이나 말풍선이 켜져있는지

    public override void Show()
    {
        base.Show();
        PlayerMove.canMove = false;
    }

    public override void Hide()
    {
        base.Hide();
        PlayerMove.canMove = true;
    }

    public override void Show(int i)
    {
        base.Show(i);
        PlayerMove.canMove = false;
    }

    public override void Hide(int i)
    {
        base.Hide(i);
        PlayerMove.canMove = true;
    }

    private void Awake()
    {
        Debug.Log(transform.position);
    }
}
