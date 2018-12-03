using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{

    AsyncOperation async;
    int progress = 0;
    // Use this for initialization
    void Start()
    {
        StartCoroutine("LoadScene");
    }
	
    // Update is called once per frame
    void Update()
    {
        progress = (int)(async.progress * 100);
        Debug.Log("guc" + progress);
    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(Globals.loadName);
        yield return async;
    }
}
