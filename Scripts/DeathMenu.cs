using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    // set a image varible
    public Image backgroundImage;

    // if show the death menu(default is false)
    private bool isShow = false;

    private float transition = 0.0f;


    // Use this for initialization
    void Start()
    {
        // set deathmenu not active, when start the game
        gameObject.SetActive(false);
    }
	

    // Update is called once per frame
    void Update()
    {
        // if is not show do nothing
        if (!isShow)
        {
            return;
        }

        // if death menu is active, make the color from transparency to black by the time
        transition += Time.deltaTime * 0.5f;
        backgroundImage.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);
    }


    // A function when it is called the death menu will active
    public void EndMenu()
    {
        gameObject.SetActive(true);
        isShow = true;
    }

    // restart the scene and initialize the the score and bunus
    public void Restart()
    {
        SceneManager.LoadScene("MainGame");
        Globals.score = 0;
        Globals.bunus = 0;
    }

    // go to the "Menu" scene
    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
