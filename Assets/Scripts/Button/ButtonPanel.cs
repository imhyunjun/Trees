using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPanel : MonoBehaviour
{
    public static bool yButtonisClicked;                       //y button 이 눌렸는지

    IEnumerator Start()
    {
        yButtonisClicked = false;
        yield return new WaitForSeconds(1f);
        OnOffTheChildButton(0, true);
        OnOffTheChildButton(1, true);                    //1초 후 0, 1번 버튼 키기
        
    }

    public void OnOffTheChildButton(int _childorder, bool _onoff)               //원하는 자식 버튼 키고 끄기
    {
        gameObject.transform.GetChild(_childorder).gameObject.SetActive(_onoff);
    }
}

