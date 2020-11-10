﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public GameObject PausePanel, ClearPanel, DiePanel,  firstBlock, player, Help, ShowHelpImage, Mission;
    public string HelpMessage;
    bool HelpSelect = false;

    private void Start()
    {
        Mission.SetActive(false);
        Help.SetActive(false);
    }

    private void Update()
    {
        if(FindObjectOfType<ClearCheck>().answerCode == "START")
        {
            if(FindObjectOfType<Tutorial>().idx > 2)
            {
                Help.SetActive(true);
            }
        }
        else
        {
            Help.SetActive(true);
        }
        ShowMission();
        if (Input.GetKeyDown(KeyCode.Escape) && !ClearPanel.activeInHierarchy)
        {
            if (!PausePanel.activeInHierarchy)
            {
                PausePanel.SetActive(true);
                player.GetComponent<BlockMove>().canInput = false;
                Time.timeScale = 0f;
            }
            else
            {
                PausePanel.SetActive(false);
                if (FindObjectOfType<ClearCheck>().answerCode == "START")
                {
                    if (FindObjectOfType<Tutorial>().idx > 4)
                    {
                        player.GetComponent<BlockMove>().canInput = true;
                        Time.timeScale = 1f;
                    }
                }
                else
                {
                    player.GetComponent<BlockMove>().canInput = true;
                    Time.timeScale = 1f;
                }
            }
        }
    }

    public void Die()
    {
        DiePanel.SetActive(true);
        player.GetComponent<BlockMove>().canInput = false;
    }

    public void ReSpawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToStage()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void Clear()
    {
        player.GetComponent<BlockMove>().canInput = false;
        ClearPanel.SetActive(true);
    }

    public void OnClickHelp()
    {
        if (FindObjectOfType<ClearCheck>().answerCode == "START")
        {
            if (FindObjectOfType<Tutorial>().idx > 2)
            {
                if (!HelpSelect)
                {
                    this.gameObject.SetActive(false);
                    ShowHelpImage.SetActive(true);
                    HelpSelect = true;
                }
                else
                {
                    this.gameObject.SetActive(true);
                    ShowHelpImage.SetActive(false);
                    HelpSelect = false;
                }
                ShowHelpImage.GetComponentInChildren<Text>().text = HelpMessage;
            }
        }
        else
        {
            if (!HelpSelect)
            {
                this.gameObject.SetActive(false);
                ShowHelpImage.SetActive(true);
                HelpSelect = true;
            }
            else
            {
                this.gameObject.SetActive(true);
                ShowHelpImage.SetActive(false);
                HelpSelect = false;
            }
            ShowHelpImage.GetComponentInChildren<Text>().text = HelpMessage;
        }

    }

    void ShowMission()
    {
        if (FindObjectOfType<ClearCheck>().answerCode == "START")
        {
            if (FindObjectOfType<Tutorial>().idx > 3)
            {
                Mission.SetActive(true);
            }
        }
        else
        {
            Mission.SetActive(true);
        }
    }

}