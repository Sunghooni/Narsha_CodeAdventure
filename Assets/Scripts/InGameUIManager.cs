﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    public GameObject PausePanel, ClearPanel, Player;
    bool isClear;

    private void Update()
    {
        isClear = Player.GetComponent<BlockMove>().winCheck;
        if(Input.GetKeyDown(KeyCode.Escape) && isClear == false)
        {
            if (!PausePanel.active)
            {
                PausePanel.SetActive(true);
                Player.GetComponent<BlockMove>().canInput = false;
            }
            else
            {
                PausePanel.SetActive(false);
                Player.GetComponent<BlockMove>().canInput = true;
            }
        }

        if (isClear)
            ClearPanel.SetActive(true);
        else
            ClearPanel.SetActive(false);

    }

    public void BackToStage()
    {
        SceneManager.LoadScene("Stage");
    }

}
