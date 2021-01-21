using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    float hp;
    GameObject gs;

    TextMesh txt;
    // Start is called before the first frame update
    void Start()
    {
    //Get local player gameobject
     gs = GameObject.Find("GameSetup").GetComponent<GameSetupController>().localPlayer;
     txt =  GetComponent<TextMesh>();
     hp = gs.GetComponent<playerManager>().getHP();
     setHP(hp);
        
    }

    private void Update() 
    {
        hp = gs.GetComponent<playerManager>().getHP();
        setHP(hp);
        
    }

    public void setHP(float hp)
    {
        txt.text = hp.ToString("F0");
        Debug.Log("Written"); 
    }
}
