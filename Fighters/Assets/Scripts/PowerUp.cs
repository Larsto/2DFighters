using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool isHealth, isInvincible, isSpeed, isGravity;

    public float powerUpLength, powerUpAmount;

    public GameObject pickupEffect;

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
        if(other.tag == "Player")
        {
            if (isInvincible)
            {
                other.GetComponent<PlayerHealthController>().MakeInvincible(powerUpLength);
                AudioManager.instance.PlaySFX(8);
            }

            if (isHealth)
            {
                other.GetComponent<PlayerHealthController>().FillHealth();
                AudioManager.instance.PlaySFX(9);
            }

            if (isSpeed)
            {
                PlayerController player = other.GetComponent<PlayerController>();
                player.moveSpeed = powerUpAmount;
                player.powerUpCounter = powerUpLength;
                AudioManager.instance.PlaySFX(10);
            }

            if (isGravity)
            {
                PlayerController player = other.GetComponent<PlayerController>();
                player.powerUpCounter = powerUpLength;
                player.theRB.gravityScale = powerUpAmount;
                AudioManager.instance.PlaySFX(11);
            }

            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
