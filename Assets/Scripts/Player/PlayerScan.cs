using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScan : MonoBehaviour
{
    private static PlayerScan instanced;
    public static PlayerScan instance => instanced;

    public ProgressStatus progressStatus { get; set; }

    private void Awake()
    {
        if (instanced == null)
            instanced = this;
        else
            Destroy(gameObject);
    }

    public void InteractWithEnvirontment()
    {
        int layerMask = (1 << LayerMask.NameToLayer("object")) + (1 << LayerMask.NameToLayer("door"));
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 4f, layerMask);
        float minDist = float.PositiveInfinity;
        int index = -1;
        for (int i = 0; i < colliders.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, colliders[i].transform.position);
            if (dist < minDist) // 가장 거리가 가까운 npc 검색
            {
                minDist = dist;
                index = i;
            }
        }
        if (index == -1) return;
        NPC npc = colliders[index].transform.GetComponent<NPC>();
        if (npc != null)
            npc.Interact();
        else
        {
            Door door = colliders[index].transform.GetComponent<Door>();
            if (door != null && door.isOpened)
            {
                door.OnUseDoor();
                if (door.playSfx) SoundManager.PlaySFX("door-open");
                gameObject.transform.position = door.arrivePoint.transform.position;
                Vector3 backGroundPoint = door.cameraArrivePoint.transform.position;
                Camera.main.transform.position = new Vector3(backGroundPoint.x, backGroundPoint.y, backGroundPoint.z - 10);
                GameManager.instance.locationPlayerIsIn = door.destinationName;
                GameManager.instance.map = door.cameraArrivePoint.name;
                door.AfterPlayerArrived();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InteractWithEnvirontment();
        }
    }
}
