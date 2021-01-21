using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;

public class HookScript : MonoBehaviour
{
    String powerUpType; // The kind of powerup the dot gives the player who conquers it
    
    [SerializeField]
    GameObject bulletPrefab; // Bullet weapon fires
    PhotonView photonView;

    [SerializeField]
    float grapplingSpeed; // speed wich players move when using grappling
    Vector2 currentTarget; // the last hookPoint the player touched, and has not reached yet
    public bool onTheMove; // bool that decides if the player is moving towards the current target or not
    bool doubleClicked; // True if you tapped a hookpoint more than once;

    //Used to callanimations
    private Animator animator;

    private Transform landingPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
        onTheMove = false;
        landingPoint = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine){
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)// So a touch is only registered once
                {
                    if (containsHookPoint(touch))
                    {
                        Vector2 Start = transform.position;
                        RaycastHit2D hit = Physics2D.Linecast(Start, currentTarget, 8); // Hits all colliders exept those in layer 2(hookpoint and player)
                        if(hit.collider == null)
                        {
                            photonView.RPC("StartMoving", RpcTarget.All, currentTarget);
                        }
                        // if there are no colliders in the line, onTheMove is true
                    }
                }
            }
        }
    }

    private void FixedUpdate() 
    {   
        //move towards current target as long as onTheMove is true
        if(onTheMove)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentTarget, grapplingSpeed);
                //Does not start rotating until close to destination
                if(Vector2.Distance(transform.position,currentTarget)<3.2)
                {
                    landingRotation();
                }
                if (!onTheMove ) // if the player stops moving towards target, check if the player double clicked the target
                {
                    if (doubleClicked)
                    {
                        doubleClick(); //if he doubleclicked the target
                    }
                    else
                    {
                        stick(); // if he didnt doubleclick, then you stick to the grapplepoint
                    }
                    
                }
                   
            }    
    }

    //Checks if the touch is inside a hook point
    bool containsHookPoint(Touch touch)
    {

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);//creates a raycast on the position of the touch
        if (hit.collider != null) // if the ray hits something or if the touch hits something
        {
            if(hit.collider.tag == "HookPoint")
            {
               Vector2 target = hit.collider.gameObject.transform.position;
               doubleClicked = (target == currentTarget);// if you already move towards that position, or that grapplepoint
               if (currentTarget == target)
                   return false;
               currentTarget = target;
               return true;
            }
        }
        return false;


    }



    void doubleClick() // What happens at the end of the grappling if you double clicked TODO
    {
        Debug.Log("DoubleClicked!");
    }

    void stick()//If you just clicked the target once, you stick to the target TODO
    {
        Debug.Log("Clicked once!");

    }


  [PunRPC]
  void StartMoving(Vector2 target)
  {
    currentTarget = target;
    animator.SetBool("OnTheMove",true);
  }

  //What happens after takeoff anim
  void finishedAnim()
  {
    onTheMove = true;
    animator.SetBool("OnTheMove",false);
  }

  // Function for landing on feet
  void landingRotation()
  {
    float feetDist = Vector2.Distance(landingPoint.position,currentTarget);
    float generalDist = Vector2.Distance(transform.position,currentTarget);
    if(feetDist > generalDist*0.85)
    {
        transform.Rotate(0.0f,0.0f,50.0f);
    }
  }

}
