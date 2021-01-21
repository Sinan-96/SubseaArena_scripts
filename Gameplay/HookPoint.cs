

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
public class HookPoint : MonoBehaviour
{
    GameObject player;
    private float currentConquerPercentage;
    public bool Conquered; 
    [SerializeField]
    String powerUpType;

    //Identifies the player that currently owns the manet
    public int playerNumber;
    private void OnCollisionEnter2D(Collision2D other)
    {
        // If a player hits the grappling point
        if(other.collider.tag == "Player" && player == null)
        {
            stick(other.collider.gameObject);
        }
    }

    // makes the player stick to the grapplingPoint TODO
    void stick(GameObject pl) 
    {
        player = pl;
        player.transform.SetParent(gameObject.transform.GetChild(0).transform);

        Debug.Log("Stuck");
        // sets the players ontheMove variable to false, to stop further movement
        player.GetComponent<HookScript>().onTheMove = false; 
        // enables rotation controls for player
        player.gameObject.GetComponent<RotateScript>().enabled = true; 





    }

    // unstick player from grapplePoint TODO
    void unStick()
    {
        // disables rotation controls for the player
        player.gameObject.GetComponent<RotateScript>().enabled = false; 
        player.transform.SetParent(player.transform);
        player = null;
        
        //If player leaves the conquering resets
        if (!Conquered)
            currentConquerPercentage = 0;

    }

    void Update()
    {
        if (player != null)
        {
            // if the player is moving to another dot
            if (player.GetComponent<HookScript>().onTheMove) 
            {
                unStick();
            }
            
            // If the point is conquered
            if (currentConquerPercentage >= 100 && !Conquered)
            {
                Debug.Log("I am conquered");
                conquered();
            }
            
            /* if there is a player on the dot, 
             * increment the currentConquerPercentage
             * with the players conquerrate*/
            else if (!Conquered)
            {
                currentConquerPercentage += player.GetComponent<playerManager>().conquerRate;
            }
        }
    }

    //What happens when a dot is conquered
    private void conquered() 
    {
        Conquered = true;
        player.GetComponent<playerManager>().powerUp(powerUpType); // Gives the player the dot powerup
        int pn = player.GetComponent<playerManager>().playerNumber;
        player.GetComponent<playerManager>().incrementManet();
        playerNumber = pn;

        grow(pn);
    }
    
    
    /* growth that happens after
     * the dot is conquered, different
     * dependig on what player conquers it*/
    private void grow(int player)
    {
        Debug.Log("growing");
        //TODO
    }

}
