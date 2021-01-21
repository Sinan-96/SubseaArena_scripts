using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RotateScript : MonoBehaviour
{
    Vector2 fingerUpPosition;
    Vector2 fingerDownPosition;
    [SerializeField]
    float rotationSpeed;
    private PhotonView photonView;

    private Animator animator;


    private void Start() 
    {
        animator = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        animator.SetBool("Sliding",false);
        checkRotation(); // checks if the player has made a movement(swipe) that rotates the player
        
    }


    void checkRotation()// checks if the player has made a movement(swipe) that rotates the player
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    fingerDownPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    if(Math.Abs(fingerDownPosition.x - touch.position.x)> 5) // min distance to be considered a swipe
                        {
                        animator.SetBool("Sliding",true);
                        if (fingerDownPosition.x > touch.position.x) // if you have moved left(swiped left)
                        {
                            //transform.parent is a gameobject called rotation point at the manet the player is currently on
                            transform.parent.transform.Rotate(Vector3.forward, - rotationSpeed * Time.deltaTime);

                            }
                            else if (fingerDownPosition.x < touch.position.x) //if you have moved right(swiped right)
                            {
                            transform.parent.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
                            }


                        }
    
                        break;
                    

            }
        }
    }
}
    


