﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public PlayerMissileLauncher[] playerMissileLauncher = new PlayerMissileLauncher[10];

    public int currentLauncher;

    public bool HasMissiles() => currentLauncher >= 0 && gameObject.activeInHierarchy;

    void Start()
    {
        ResetMissileLaunchers();
    }

    public void ResetMissileLaunchers()
    {
        currentLauncher = playerMissileLauncher.Length - 1;
        for (int i = 0; i < playerMissileLauncher.Length; i++)
        {
            playerMissileLauncher[i].gameObject.SetActive(true);
        }
    }
}
