using Photon.Pun;
using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSetupController : MonoBehaviourPun
{
    // The amount of points a player must get to win the game
    public float winningScore;
    public GameObject localPlayer;

    private float respawnTime = 5f;


    // Different points player can start in
    public Transform[] spawnPoints;
    public float[] playerScores = {0,0,0,0};
    // This script will be added to any multiplayer scene
    void Start()
    {
        CreatePlayer(); //Create a networked player object for each player that loads into the multiplayer scenes.
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        localPlayer = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), pickSpawn().position, Quaternion.identity);
    }

    private void FixedUpdate() 
    {
        for (int i = 0; i <playerScores.Length; i++)
        {

            if(playerScores[i] > winningScore)
                winGame(i);
            if(localPlayer.GetComponent<playerManager>().getHP() <= 0)
                respawn();
        }
        if(respawnTime <= 0)
            localPlayer.SetActive(true); 
        else
         respawnTime-= Time.deltaTime;
           
    }

    private void winGame(int player)
    {
        Debug.Log("Player: " + PhotonNetwork.PlayerList[player] + " won!");
        StartCoroutine(EndGame());

    }

    private IEnumerator EndGame()
    {
        PhotonNetwork.LeaveRoom();
        while(PhotonNetwork.InRoom)
            yield return null;
        SceneManager.LoadScene(0);
        
    }

    //Gets the index of the players spawn point
    private Transform pickSpawn()
    {
        return spawnPoints[System.Array.IndexOf(PhotonNetwork.PlayerList,PhotonNetwork.LocalPlayer)];
    }

    private void respawn()
    {
        localPlayer.SetActive(false);
        localPlayer.transform.position = pickSpawn().position;
        localPlayer.GetComponent<playerManager>().setHP(100);
        respawnTime = 5f;


    }

}

