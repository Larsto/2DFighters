using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{
    public int stompDamage;
    public float bounceForce = 12f;
    //public PlayerController thePlayer;
    public PlayerControllerMultiple thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealthController>().DamagePlayer(stompDamage);
            thePlayer.theRB.velocity = new Vector2(thePlayer.theRB.velocity.x, bounceForce);
        }
    }
}
