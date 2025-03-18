using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameMode gameMode = GameMode.Auditorium;

    private void Start()
    {
        if (Application.isMobilePlatform)
        {
            Application.targetFrameRate = 120;
        }
        else
        {
            Application.targetFrameRate = 60;
        }
    }

    private void Update()
    {
        
    }


    public GameMode GameMode
    {
        get => gameMode;
        set => gameMode = value;
    }
}
