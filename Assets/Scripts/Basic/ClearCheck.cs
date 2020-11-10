﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearCheck : MonoBehaviour
{
    public GameObject[] checkBocks;
    public IdentifyCode[] answerBocks;
    public bool winCheck = false;
    public InGameUIManager IGUI;

    public string answerCode = "START";
    private string input;
    public GameObject HelpText;

    void Update()
    {
        ArrayCheck();
    }

    void ArrayCheck()
    {
        RaycastHit hit;
        for(int i = 0; i < checkBocks.Length; i++)
        {
            if(Physics.Raycast(checkBocks[i].transform.position, Vector3.up, out hit, 2) && hit.transform.tag != "Player")
            {
                IdentifyCode identifyCode = hit.transform.gameObject.GetComponent<IdentifyCode>();
                input += identifyCode.code;
            }
        }
        if (input != null && input.Equals(answerCode))
        {
            if(answerCode == "START")
            {
                Debug.Log("Clear");
                winCheck = true;
                HelpText.GetComponent<Text>().color = Color.green;
                IGUI.Clear();
            }
            else
            {
                winCheck = true;
            }
        }
        else
            input = null;
    }
}