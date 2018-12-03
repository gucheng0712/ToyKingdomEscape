using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public Text scoreText;
    public Text bunusText;
    public Text totalScoreText;

	

    void Update()
    {
        //Update the total score
        scoreText.text = Globals.score.ToString();
        bunusText.text = Globals.bunus.ToString();
        totalScoreText.text = (Globals.score + Globals.bunus).ToString();
    }


}
