using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// This class manages all the attributes of the player
public class playerManager : MonoBehaviour
{
    [SerializeField]
    private float playerHealth;
    [SerializeField]
    public float conquerRate; // how fast the player can conquer a point
    // Start is called before the first frame update
    public float damage { get; private set; }

    public float currentAmmo { get; private set; }

    public float maxAmmo; 

    [SerializeField]
    private float ammoRecharge;

    // How many victory points a player has decides how close to winning he/she is
    public float victoryPoints { get; private set;}

    //How many manets you have decides how many victoryPoints you gain
    public float manetCount { get; private set;}

    //How fast you gain victorypoints, same for all players
    public float victoryRate;

    public int playerNumber;

    GameObject gs;
    public GameObject ammoBar;
    public GameObject scoreBar;
    public GameObject HP;

    PhotonView photonView;

    

    void Start()
    {
        Object.Instantiate(scoreBar,new Vector3( -10f, 14.5f, 0f), Quaternion.identity);
        Object.Instantiate(ammoBar,new Vector3( -7.7f, 13f, 0f), Quaternion.identity);
        Object.Instantiate(HP,new Vector3( 7.0f, 13.0f, 0f), Quaternion.identity);
        currentAmmo = maxAmmo;
        gs = GameObject.Find("GameSetup");
        photonView = GetComponent<PhotonView>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentAmmo < maxAmmo)
            currentAmmo += ammoRecharge;
        //If the player has zero or less health it is destroyed
        //Accumulates victory points
        victoryPoints += victoryRate * manetCount;
        setHealth(0.5f);
        
        //Updates the player score(VictoryPoints) in the gameController
        for(int i = 0; i < PhotonNetwork.PlayerList.Length;i++)
        {
            if(PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[i])
            {
                gs.GetComponent<GameSetupController>().playerScores[i] = victoryPoints;
            }


        }
        
    }

    public void setHealth(float damage)
    {
        //To be sure it is not called several times we use photonview
        if(photonView.IsMine)
            playerHealth -= damage;
    }


    public void powerUp(string powerup) // Power up the player, based on the argument
    {
        //Cheks what powerup the player got and applies the powerup
        switch (powerup)
        {
            case "conquer":
                conquerRate += conquerRate * 0.3f;
                break;
            case "damage":
                damage += 0.3f * damage;
                break;
        }
    }

    //Calls everytime the player conquers a manet
    public void incrementManet()
    {
        manetCount+=1;
    }

    public void useAmmo()
    {
        currentAmmo -= 1;
    }
    
    public float getHP()
    {
        return playerHealth;
    }

    public void setHP(float health)
    {
        playerHealth = health;
    }

}
