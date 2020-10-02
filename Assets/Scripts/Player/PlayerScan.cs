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
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 3.5f, LayerMask.GetMask("object"));
            if (hit)
            {
                NPC npc = hit.transform.GetComponent<NPC>();
                if (npc != null) npc.Interact();
            }
        }
    }
}
