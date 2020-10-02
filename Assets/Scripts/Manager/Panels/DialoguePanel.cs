using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePanel : PanelSingletone<DialoguePanel>
{
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

}
