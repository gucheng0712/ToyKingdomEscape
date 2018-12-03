using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlashFade : MonoBehaviour
{
    private float transition;
    public Image introductionImg;
    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
        transition += Time.deltaTime * 0.01f;
        introductionImg.color = Color.Lerp(introductionImg.color, Color.black, transition);
        if (introductionImg.color == Color.black)
        {
            Globals.loadName = "Menu";
            SceneManager.LoadScene("LoadingScene");
        }
    }
}
