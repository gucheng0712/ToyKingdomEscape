using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PressToStart : MonoBehaviour
{
    public Text pressStartText;
    private bool isFading = true;
    public AudioClip click;
    private AudioSource source;


    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            source.PlayOneShot(click);
            StartCoroutine("FadeTime");
 
        }
        if (!isFading)
        {
            Globals.loadName = "Menu";
            SceneManager.LoadScene("LoadingScene");
        }
  
    }

    IEnumerator FadeTime()
    {
        pressStartText.CrossFadeAlpha(0, 1, true);
        yield return new WaitForSeconds(1f);
        isFading = false;
    }

}
