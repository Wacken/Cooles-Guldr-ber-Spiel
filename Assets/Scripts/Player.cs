﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/* This class just makes it faster to get certain components on the player. */

public class Player : MonoBehaviour
{

    #region Singleton

    public static Player instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject _player;

    public PlayerToShader _playerToShader;

}