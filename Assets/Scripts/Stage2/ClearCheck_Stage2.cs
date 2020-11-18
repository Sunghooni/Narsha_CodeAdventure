﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearCheck_Stage2 : MonoBehaviour
{
    public GameObject[] checkBocks;
    public GameObject[] answerBlocks;
    public GameObject ClearBlock;
    public GameObject MissionText, MissionImage;
    public bool arrayCheck = false;
    public bool templeCheck = false;
    public bool clearCheck = false;

    private string lengthTemp = null;
    public int lengthNum = 0;
    private string input;
    private string[] answers = new string[3]{ "intLength=1;", "intLength=3;", "intLength=5;" };
    private bool isFinished = false;

    private void Update()
    {
        if(!isFinished)
        {
            ArrayCheck();
            OpenBoxCheck();
        }
    }

    void ArrayCheck()
    {
        RaycastHit hit;
        for (int i = 0; i < checkBocks.Length; i++)
        {
            if (Physics.Raycast(checkBocks[i].transform.position, Vector3.up, out hit, 2) && hit.transform.tag != "Player")
            {
                CodeBlock codeBlock = hit.transform.gameObject.GetComponent<CodeBlock>();
                input += codeBlock.code;

                if(i == checkBocks.Length - 2)
                {
                    lengthTemp = codeBlock.code;
                }
            }
        }
        if (input != null)
        {
            for(int i = 0; i < answers.Length; i++)
            {
                if (input.Equals(answers[i]))
                {
                    lengthNum = int.Parse(lengthTemp);
                    arrayCheck = true;
                    break;
                }
                else if(i == answers.Length - 1)
                {
                    lengthNum = 0;
                }
            }
            if(lengthNum == 0)
            {
                arrayCheck = false;
            }
        }
        lengthTemp = null;
        input = null;
    }

    void OpenBoxCheck()
    {
        RaycastHit hit;
        Vector3 pos = ClearBlock.transform.position;
        pos = new Vector3(pos.x, pos.y, pos.z);
        Debug.DrawRay(pos, Vector3.up * 1, Color.red, 1f);
        if(lengthNum == 1)
        {
            if(Physics.Raycast(pos, Vector3.up, out hit, 0.6f) && hit.transform.tag.Equals("Player"))
            {
                templeCheck = true;
                DeleteExtraBlocks();
                if (!isFinished)
                {
                    MissionText.GetComponent<Text>().color = Color.green;
                    FindObjectOfType<AudioManager>().PlaySounds("CodeComplete");
                    Invoke("ChangeMissionText", 1.0f);
                }
                isFinished = true;
                FixBlocks();
            }
        }
    }

    void FixBlocks()
    {
        for(int i = 0; i < answerBlocks.Length; i++)
        {
            answerBlocks[i].tag = "Unmovable";
        }
    }

    void DeleteExtraBlocks()
    {
        Destroy(answerBlocks[3]);
        Destroy(answerBlocks[5]);
    }

    void ChangeMissionText()
    {
        MissionImage.GetComponent<RectTransform>().sizeDelta = new Vector2(MissionImage.GetComponent<RectTransform>().sizeDelta.x, 34);
        MissionText.GetComponent<Text>().text = "보물상자를 여세요";
        MissionText.GetComponent<Text>().color = Color.black;
    }
}
