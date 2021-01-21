using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ammo : MonoBehaviour
{
    GameObject gs;
    public List<GameObject> childrens;
    float maxAmmo;
    float currentAmmo;


    private void Start() 
    {
     //Get local player gameobject
     gs = GameObject.Find("GameSetup").GetComponent<GameSetupController>().localPlayer;
     maxAmmo = gs.GetComponent<playerManager>().maxAmmo;

     //Creates list with all the children AKA the bullet icons
     childrens = new List<GameObject>();
     foreach (Transform child in transform)
      {           
        if (!childrens.Contains(child.gameObject))
        {
            childrens.Add(child.gameObject);
        }            
      }
    }
    // Update is called once per frame
    void Update()
    {
        currentAmmo = gs.GetComponent<playerManager>().currentAmmo;
        makeInvisible(Mathf.FloorToInt(maxAmmo) - Mathf.FloorToInt(currentAmmo));
        makeVisible();
    }

    void makeInvisible(int num)
    {

        for(int i = 0; i<num; i++)
        {
            //Make that bullet icon false
            childrens[Mathf.RoundToInt(maxAmmo)-1-i].GetComponent<Renderer>().enabled = false;
        }
    
    }

    void makeVisible()
    {
        for(int i = 0 ; i< Mathf.FloorToInt(currentAmmo); i++)
        {
           childrens[i].GetComponent<Renderer>().enabled = true; 
        }

    }
}
