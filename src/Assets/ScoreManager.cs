using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public static int Score;
    private GUIText PlayerScore;
    public GUITexture TimeLimit;
    private GUIText TimeRemains;

    private float timeRemaining;
    private float decrement = 12f;

    // Use this for initialization
    void Start ()
    {
        Score = 0;
        PlayerScore = GameObject.Find("Player Score").GetComponent<GUIText>();
        TimeRemains = GameObject.Find("Time Limit").GetComponent<GUIText>();
        TimeLimit = GameObject.Find("Lightsaber Beam").GetComponent<GUITexture>();

        timeRemaining = 30;
    }
    
    // Update is called once per frame
    void Update () 
    {
        if (timeRemaining > 0)
        {
            PlayerScore.text = "" + Score;
            float x = TimeLimit.pixelInset.x - decrement * Time.deltaTime;
            float y = TimeLimit.pixelInset.y;
            float width = TimeLimit.pixelInset.width;
            float height = TimeLimit.pixelInset.height;

            TimeLimit.pixelInset = new Rect(x, y, width, height);

            timeRemaining -= Time.deltaTime;
            TimeRemains.text = ""; //+ "Time Limit: " + timeRemaining;
            //TimeRemains.text = "Delta Time: " + Time.deltaTime;
        }
        else
        {
            TimeRemains.text = "Time's Up";
        }
    }

    void AddScore(int score)
    {
        Score += score;
    }
}
