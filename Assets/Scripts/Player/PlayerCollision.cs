using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Doors"))    //각 문을 doors로 혹은 다른것으로 태그 설정 -> 플레이어가 충돌시 지정된 좌표로 이동
        {
            Door door = collision.collider.transform.GetComponent<Door>();
            if (door.isOpened)
            {
                door.OnUseDoor();
                if (door.playSfx) SoundManager.PlaySFX("door-open");
                gameObject.transform.position = door.arrivePoint.transform.position;
                Vector3 backGroundPoint = door.cameraArrivePoint.transform.position;
                Camera.main.transform.position = new Vector3(backGroundPoint.x, backGroundPoint.y, backGroundPoint.z - 10);
                GameManager.instance.locationPlayerIsIn = door.destinationName;
                door.AfterPlayerArrived();
            }
        }
    }

}
