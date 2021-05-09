using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MENU : MonoBehaviour
{
    // Load game
    public void StartGame(){
        SceneManager.LoadSceneAsync(1);
    }
    // Load game menu
    public void LoadMenu(){
        SceneManager.LoadSceneAsync(0);
    }

    // Quit Application
    public void Quit(){
        Application.Quit();
    }
    
}
