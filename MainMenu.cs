using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text highScoreUI;

    string newGameScene = "SampleScene";

    public AudioSource main_channel;
    public AudioClip background_music;

    void Start()
    {
        main_channel.PlayOneShot(background_music);

        // Set the High Score Text
        int highScore = SaveLoadManager.Instance.LoadHighScore();
        highScoreUI.text = $"Highest Wave Survived: {highScore}";
    }

    public void StartNewGame()
    {
        main_channel.Stop();

        SceneManager.LoadScene(newGameScene);
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
