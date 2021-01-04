﻿#define Test

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float playerSpeed;

    private float playerXAxis;
    private float playerYAxis;
    private Rigidbody2D playerRigid;
    private SpriteRenderer playerSprite;
    private Animator playerAnim;       //플레이어 애니메이션

    public static bool canMove = true;
    public static Vector3 playerMoveVec;
    public static bool isDraging;


    private void Awake()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        playerSprite.flipX = false;
        isDraging = false;
        DontDestroyOnLoad(gameObject);
    }

    //private void Update()
    //{
    //    playerXAxis = Input.GetAxisRaw("Horizontal");
    //    playerYAxis = Input.GetAxisRaw("Vertical");

    //    if (canMove)
    //    {
    //        playerMoveVec.Set(playerXAxis, playerYAxis, 0f);

    //        if (playerMoveVec == Vector3.zero)
    //        {
    //            playerAnim.SetBool("IsPlayerMoving", false);
    //        }
    //        else
    //        {
    //            if (playerXAxis > 0) playerSprite.flipX = false;
    //            else if (playerXAxis < 0)
    //                playerSprite.flipX = true;
    //            playerAnim.SetBool("IsPlayerMoving", true);
    //        }
    //    }
    //    else
    //    {
    //        playerMoveVec = Vector3.zero;
    //        playerAnim.SetBool("IsPlayerMoving", false);
    //    }


    //}

    private void FixedUpdate()
    {
        //playerRigid.MovePosition(transform.position + playerMoveVec * playerSpeed * Time.fixedDeltaTime);

        if(isDraging && canMove)
        {
            Debug.LogError("dragging");
            playerRigid.MovePosition(transform.position + playerMoveVec * playerSpeed * Time.fixedDeltaTime);
            if(playerMoveVec != Vector3.zero)
            {
                if (playerMoveVec.x > 0) playerSprite.flipX = false;
                else if (playerMoveVec.x < 0)
                    playerSprite.flipX = true;
                playerAnim.SetBool("IsPlayerMoving", true);
            }
        }
        else
        {
            playerAnim.SetBool("IsPlayerMoving", false);
        }
    }

    

}

