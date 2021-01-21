using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootyShooty : MonoBehaviour
{
    [SerializeField]
    Transform fireOrigin; //origin of the shoot, deduct this vector from firepoint position vector to get the direction of the shot
    
    Touch touch;
    
    private float accumulatedTime; // how long the tap has been held
    
    [SerializeField]
    private GameObject bulletPrefab; // Bullet weapon fires
    
    
    [SerializeField]
    Transform firePoint;//muzzle of the gun, point where bullet spawns
    
    PhotonView photonView;
    public float fireRate;
    private float damage; // how much damage a shoot does

    private Vector3 tapPosition; 

    //Used to check if a touch is a tap or a swipe
    private void Start()
    {
        damage = gameObject.GetComponent<playerManager>().damage;
        photonView = GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
         if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    tapPosition = touch.position;
                    break;
               
                case TouchPhase.Ended:
                    //Checks if it is a tap
                    if (Vector3.Distance(tapPosition,touch.position)<5.0) 
                        photonView.RPC("shootPrefab", RpcTarget.All);
                    break;


                }
            }
        
        
    }
    // if bullet shoots a bullet type like object
    [PunRPC]
    void shootPrefab()
    {
        if(gameObject.GetComponent<playerManager>().currentAmmo >=1)
        {
            gameObject.GetComponent<playerManager>().useAmmo();
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        
        else
        {
            //Maybe add sound of cliking
        }
        
    }

    /*
    //if bullet shoots a laserlike beam
    void shootRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, (firePoint.transform.position - fireOrigin.position));
        Debug.DrawRay(firePoint.transform.position, (firePoint.transform.position - fireOrigin.position) * 100, Color.red,0.05f);
        if(hit.collider != null)
        {
            /*if it hits other player
             * damage that players stamina*
            if (hit.collider.tag == "Player")
                hit.collider.gameObject.GetComponent<playerManager>().currentStamina -= damage * accumulatedTime; 
        }
    
    }
    */
    
    
    //While the shoot is charging
    void charge()
    {
        accumulatedTime += touch.deltaTime;
        Debug.Log(accumulatedTime);
        //TODO
    }
}
