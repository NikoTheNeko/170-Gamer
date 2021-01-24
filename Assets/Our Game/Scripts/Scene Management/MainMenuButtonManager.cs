﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonManager : MonoBehaviour{

    public void YesIDoTheCooking(){
        SceneManager.LoadScene("Kitchen but with Tilemaps", LoadSceneMode.Single);
    }

    public void GoToSceneExplore(){
        SceneManager.LoadScene("CombatPrototype", LoadSceneMode.Single);
    }

    public void FlushToiletCloseGame(){
        Application.Quit();
    }

}
