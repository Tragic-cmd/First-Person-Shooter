using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    string newGameScene = "SampleScene";
    string menuScene = "MainMenu";
    public void StartNewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

}
