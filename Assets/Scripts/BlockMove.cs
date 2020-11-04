﻿using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockMove : MonoBehaviour
{
    public GameObject player;
    public bool canInput = true;
    //private const float tolerance = 0.3f; // 허용 오차범위 (공차)
    private bool canMove = true;
    private bool isPressed = false;
    private Coroutine MoveCoroutine;
    Vector2 InputAxis;
    private Vector3 initialRotator;

    private void Awake()
    {
        initialRotator = transform.rotation.eulerAngles;
    }

    void Update()
    {
        GetInput();
        ResetBlock();
    }

    void GetInput()
    {
        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        InputAxis = new Vector2(horz, vert);

        if(InputAxis.sqrMagnitude > 0 && canInput)
        {
            if(!isPressed)
            {
                isPressed = true;
                MoveCoroutine = StartCoroutine(KeepMoving(0.3f));
            }
        }
        else
        {
            isPressed = false;
            if (MoveCoroutine != null)
            {
                StopCoroutine(MoveCoroutine);
            }
        }
    }

    IEnumerator KeepMoving(float WaitBetweenMove)
    {
        while(isPressed)
        {
            float degree = MathFunctionLibrary.VectorToDegree(new Vector2(InputAxis.x, InputAxis.y));
            player.transform.eulerAngles = new Vector3(initialRotator.x, -1 * MathFunctionLibrary.SnapByUnit(degree, 90));
            //Test
            /*
            if (InputAxis.x > 0)
                player.transform.eulerAngles = new Vector3(-90, 0, 0);
            else if (InputAxis.x < 0)
                player.transform.eulerAngles = new Vector3(-90, -180, 0);
            else if (InputAxis.y > 0)
                player.transform.eulerAngles = new Vector3(-90, -90, 0);
            else if (InputAxis.y < 0)
                player.transform.eulerAngles = new Vector3(-90, 90, 0);
            */
            Move(player);
            yield return new WaitForSeconds(WaitBetweenMove);
        }
    }

    

    

    void Move(GameObject me)
    {
        RaycastHit moveHit;
        Vector3 movePos = me.transform.position;

        if (me == player)
            canMove = true;

        if (Physics.Raycast(movePos, transform.right, out moveHit, 1f))
        {
            if (moveHit.transform.tag.Equals("Unmovable"))
            {
                canMove = false;
                return;
            }
            else
                Move(moveHit.transform.gameObject);
        }
        if(canMove)
        {
            movePos += transform.right * 1f;
            me.transform.position = movePos;
            AudioManager.GetInstance().PlaySounds("BasicMove");
        }
        return;
    }

    void ResetBlock()
    {
        if(Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}