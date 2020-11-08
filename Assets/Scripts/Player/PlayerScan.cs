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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 3.5f, LayerMask.GetMask("object"));
            //if (hit)
            //{
            //    NPC npc = hit.transform.GetComponent<NPC>();
            //    if (npc != null) npc.Interact();
            //}

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 4f, LayerMask.GetMask("object"));
            float minDist = float.PositiveInfinity;
            int index = -1;
            for(int i = 0; i < colliders.Length; i++)
            {
                if(Vector3.Distance(transform.position, colliders[i].transform.position) < minDist) // 가장 거리가 가까운 npc 검색
                {
                    minDist = Vector3.Distance(transform.position, colliders[i].transform.position);
                    index = i;
                }
            }
            if (index != -1)
            {
                NPC npc = colliders[index].transform.GetComponent<NPC>();
                if (npc != null) npc.Interact();
            }
        }
    }
}
