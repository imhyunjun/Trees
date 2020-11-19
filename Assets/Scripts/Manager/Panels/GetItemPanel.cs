using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItemPanel : PanelSingletone<GetItemPanel>
{
    [SerializeField]
    private Text text;

    public IEnumerator IShowText(string itemName)
    {
        text.text = itemName + "를(을) 얻었다.";
        Show();

        yield return new WaitForSecondsRealtime(2);

        Hide();
    }
}
