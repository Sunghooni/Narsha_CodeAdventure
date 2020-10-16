﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCheck : MonoBehaviour
{
    public GameObject[] checkBocks;
    public IdentifyCode[] answerBocks;
    public bool winCheck = false;

    private string answerCode = "START";
    private string input;

    void Update()
    {
        ArrayCheck();
    }

    void ArrayCheck()
    {
        RaycastHit hit;
        for(int i = 0; i < checkBocks.Length; i++)
        {
            if(Physics.Raycast(checkBocks[i].transform.position, Vector3.up, out hit, 2))
            {
                IdentifyCode identifyCode = hit.transform.gameObject.GetComponent<IdentifyCode>();
                input += identifyCode.code;
            }
        }
        if (input != null && input.Equals(answerCode))
        {
            Debug.Log("Clear");
            winCheck = true;
        }
        else
            input = null;
    }
}
