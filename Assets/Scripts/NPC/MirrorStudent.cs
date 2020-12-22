using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorStudent : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private Transform destination;

    public void Move()
    {
        LeanTween.move(gameObject, destination.position, 1f).setSpeed(moveSpeed).setOnComplete(() =>
        {
            List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
            list.Add(new KeyValuePair<GameObject, string>(gameObject, "chapter_0_1")); 
            DialogueManager.instance.ShowDialogueBallon(list, 0.7f, 2f);              // 이동 완료후 머리 위에 "..." 말풍선
        });
    }
}
