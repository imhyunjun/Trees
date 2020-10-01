using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    float playerXAxis;
    float playerYAxis;

    public float playerSpeed;

    Vector3 playerMoveVec;
    Rigidbody2D playerRigid;
    SpriteRenderer playerSprite;

    Animator playerAnim;                //플레이어 애니메이션
   
    private void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();

        playerSprite.flipX = false;

        DontDestroyOnLoad(gameObject);

    }

    private void Update()
    {
        playerXAxis = Input.GetAxisRaw("Horizontal");
        playerYAxis = Input.GetAxisRaw("Vertical");
       
        if (!DialogueManager.Instance.isDialogueActive)
        {
            playerMoveVec.Set(playerXAxis, playerYAxis, 0f);

            if(playerMoveVec == Vector3.zero)
            {
                playerAnim.SetBool("IsPlayerMoving", false);
            }
            else
            {
                if (playerXAxis > 0) playerSprite.flipX = false;
                else playerSprite.flipX = true;
                playerAnim.SetBool("IsPlayerMoving", true);
            }
        }
        else
        {
            playerMoveVec = Vector3.zero;
            playerAnim.SetBool("IsPlayerMoving", false);
        }
      
    }



    private void FixedUpdate()
    {
        playerRigid.MovePosition(transform.position + playerMoveVec * playerSpeed * Time.fixedDeltaTime);
    }
}

