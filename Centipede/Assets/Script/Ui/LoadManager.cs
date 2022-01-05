using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public void LoadRegister(){
        SceneManager.LoadScene("RegisterMenu");
    }

    // Back button
    public void BackToMenu(){
        SceneManager.LoadScene("Menu");
    }    

    // Load scene to gameplay
    public void LoadGame(){    
        SceneManager.LoadScene("GameMap",LoadSceneMode.Single);
    }         

    // Exit game button
    public void QuitGame(){
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
