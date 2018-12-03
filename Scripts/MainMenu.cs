using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text highScoreText;


    void Start()
    {
        highScoreText.text = "HIGH SCORE: " + (int)PlayerPrefs.GetFloat("HighScore");
    }



    public void ToGame()
    {
        Globals.loadName = "MainGame";
        SceneManager.LoadScene("LoadingScene");
        Globals.score = 0;
        Globals.bunus = 0;
    }
}
