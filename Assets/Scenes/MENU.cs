using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MENU : MonoBehaviour
{
    
    public void StartGame(){
        SceneManager.LoadSceneAsync(1);
    }

    public void LoadMenu(){
        SceneManager.LoadSceneAsync(0);
    }
    public void Quit(){
        Application.Quit();
    }
    
}
