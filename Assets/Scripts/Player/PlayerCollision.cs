using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Front"))                            //태그명은 나중에 정하기, 신발장 벗어날시
        {
            StartCoroutine(DialogueManager.instance.IContinueDialogue(5, 5)); //경계 지우기
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Door"))
        {
            StartCoroutine(DialogueManager.instance.IContinueDialogue(6, 6)); //- 수정 필요
        }
        else if(collision.collider.CompareTag("Doors"))                              //각 문을 doors로 혹은 다른것으로 태그 설정 -> 플레이어가 충돌시 지정된 좌표로 이동
        {
            Door door = collision.collider.transform.GetComponent<Door>();
            if (door.isOpened)
            {
                if(door.playSfx) SoundManager.PlaySFX("door-open");
                gameObject.transform.position = door.arrivePoint.transform.position;
                Vector3 backGroundPoint = door.cameraArrivePoint.transform.position;
                Camera.main.transform.position = new Vector3(backGroundPoint.x, backGroundPoint.y, backGroundPoint.z - 10);
                GameManager.instance.locationPlayerIsIn = door.destinationName;
            }
        }
    }

}
