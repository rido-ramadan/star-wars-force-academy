using UnityEngine;
using Leap;
using System.Collections;

public class ScenePicker : MonoBehaviour {

    Controller m_leapController;
    private float offset = 100;
    private SpriteRenderer level1, level2;
    private float timeToStart = 3f;
    private float timeRemains;
    private string Level1 = "ForceAcademy2";
    private string Level2 = "ForceAcademy1";
    private string LevelToLoad;

    // Use this for initialization
    void Start()
    {
        m_leapController = new Controller();

        level1 = GameObject.Find("Level 1").GetComponent<SpriteRenderer>();
        level2 = GameObject.Find("Level 2").GetComponent<SpriteRenderer>();

        level1.enabled = false;
        level2.enabled = false;

        timeRemains = timeToStart;
    }

    Hand GetLeftMostHand(Frame f)
    {
        float smallestVal = float.MaxValue;
        Hand h = null;
        for (int i = 0; i < f.Hands.Count; ++i)
        {
            if (f.Hands[i].PalmPosition.ToUnity().x < smallestVal)
            {
                smallestVal = f.Hands[i].PalmPosition.ToUnity().x;
                h = f.Hands[i];
            }
        }
        return h;
    }

    Hand GetRightMostHand(Frame f)
    {
        float largestVal = -float.MaxValue;
        Hand h = null;
        for (int i = 0; i < f.Hands.Count; ++i)
        {
            if (f.Hands[i].PalmPosition.ToUnity().x > largestVal)
            {
                largestVal = f.Hands[i].PalmPosition.ToUnity().x;
                h = f.Hands[i];
            }
        }
        return h;
    }

    void FixedUpdate()
    {

        Frame frame = m_leapController.Frame();

        Hand hand = GetRightMostHand(frame);

        if (timeRemains <= 0f)
        {
            Debug.Log("START GAME");
            Application.LoadLevel(LevelToLoad);
        }
        else
        {

        }

        if (hand.PalmPosition.x < -offset)
        {
            if (!level1.enabled)
            {
                level1.enabled = true;
                level2.enabled = false;
                timeRemains = timeToStart; // hold 5 seconds 
                LevelToLoad = Level1;
            }
            else
            {
                timeRemains -= Time.deltaTime;
            }
            
        }
        else if (hand.PalmPosition.x > offset)
        {
            if (!level2.enabled)
            {
                level1.enabled = false;
                level2.enabled = true;
                timeRemains = timeToStart; // hold 5 seconds 
                LevelToLoad = Level2;
            }
            else
            {
                timeRemains -= Time.deltaTime;
            }
        }
        else
        {
            level1.enabled = false;
            level2.enabled = false;
            LevelToLoad = "";
        }
    }

}
