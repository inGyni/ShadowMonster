using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject obstaclesTile;
    [HideInInspector]
    public bool RedCollected = false;
    [HideInInspector]
    public bool GreenCollected = false;
    [HideInInspector]
    public bool BlueCollected = false;
    public Image redStar;
    public Image greenStar;
    public Image blueStar;
    private float timeRed = 0;
    private float timeGreen = 0;
    private float timeBlue = 0;
    [HideInInspector]
    public bool finishedLevel = false;

    public void Start()
    {
    }

    public void Update()
    {
        if (RedCollected)
        {
            timeRed += Time.deltaTime * 0.5f;
            redStar.color = Color.LerpUnclamped(new Color(1f, 0f, 0f, 0f), new Color(1f, 0f, 0f, 1f), timeRed);
        }
        if (GreenCollected)
        {
            timeGreen += Time.deltaTime;
            greenStar.color = Color.LerpUnclamped(new Color(0f, 1f, 0f, 0f), new Color(0f, 1f, 0f, 1f), timeGreen);
        }
        if (BlueCollected)
        {
            timeBlue += Time.deltaTime;
            blueStar.color = Color.LerpUnclamped(new Color(0f, 0f, 1f, 0f), new Color(0f, 0f, 1f, 1f), timeBlue);
        }

        if(RedCollected && GreenCollected && BlueCollected)
        {
            obstaclesTile.GetComponent<Renderer>().enabled = false;
            finishedLevel = true;
        }
    }


}