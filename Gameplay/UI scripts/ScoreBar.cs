using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBar : MonoBehaviour
{
    float maxScore;
    float currentScore;
    GameObject gs;
    GameObject player;

    //Front part of the scoreBar
    Transform front;
    // Start is called before the first frame update
    void Start()
    {
      gs = GameObject.Find("GameSetup");
      player = gs.GetComponent<GameSetupController>().localPlayer;
      maxScore = gs.GetComponent<GameSetupController>().winningScore;
      front = gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = player.GetComponent<playerManager>().victoryPoints; 
        
        //Changes the x scale based on the ratio between max and current victory points
        Vector3 lTemp = front.localScale;
        lTemp.x = currentScore/maxScore;
        front.localScale = lTemp;
        
    }
}
