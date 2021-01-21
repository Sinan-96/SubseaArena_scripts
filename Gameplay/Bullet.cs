using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Animator animator;
    public float speed = 20f;
    public float damage;
    
    // Start is called before the first frame update

    private void FixedUpdate() 
    {
        transform.position += transform.right * speed * Time.deltaTime;   
    }
     //When the bullet hits something
     private void OnTriggerEnter2D(Collider2D other) {

        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<playerManager>().setHealth(damage);
            Debug.Log("Gjorde skade!");
        }

         Destroy(gameObject);
        
        
    
    }
    //Waits for the animation to finish
    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
    }

    
}
