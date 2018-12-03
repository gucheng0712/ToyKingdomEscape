using UnityEngine;
using System.Collections;

public class Magnet : MonoBehaviour
{
    private AudioSource source;
    public AudioClip coinAudio;
    public AudioClip diamondAudio;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    // When the player have magnet, he can get coin and diamonds which in the certain range
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Coin")
        {
            source.PlayOneShot(coinAudio, 0.5f);
            Destroy(col.gameObject);
            Debug.Log("Get 10 Points");
            Globals.GetPoint(10);
        }

        if (col.gameObject.tag == "Diamond")
        {
            source.PlayOneShot(diamondAudio, 2.0f);
            Destroy(col.gameObject);
            Debug.Log("Get 50 points");
            Globals.GetPoint(50);
        }
    }
}
