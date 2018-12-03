using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour
{
    // Set static varible score and bunus;
    public static float score = 0;
    public static int bunus = 0;
    public static string loadName;



    // A static function for player to get point by collide with coins and diamonds
    public static void GetPoint(int value)
    {
        bunus += value;
    }

    // A static function for player to get point by the increasing of time
    public static void IncreasingScore()
    {
        score += 1 + (int)Time.deltaTime;
    }


    // A static function to stop the score's increaseing
    public static int GameOver()
    {
        return (int)score;
    }






}
