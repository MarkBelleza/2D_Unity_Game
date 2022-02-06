using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPC : MonoBehaviour
{
    public Animator anim;
    public int maxHealth = 100;
    int currentHealth;

    public Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
            
    }

    private void Update()
    {
        if (body.velocity.x >= 0.01f) //if going to the right
        {
            transform.localScale = new Vector2(1.4524f, 1.4524f);
        }
        else if (body.velocity.x < 0.01f) // if going to the left
        {
            transform.localScale = new Vector2(-1.4524f, 1.4524f);
        }

        //Animation for running left and right
        if (body.velocity.x != 0)  
        {
            anim.SetBool("Running", true);
        }
        else if (body.velocity.x == 0)
        {
            anim.SetBool("Running", false);
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //Damaged animation
        anim.SetTrigger("Hit");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        
        //Die Animation
        anim.SetBool("isDead", true); 

        //Disable enemy

        //Change Layer to Player 
        gameObject.layer = LayerMask.NameToLayer("Player");
        //Disable the NPC script
        this.enabled = false; 
    }

}
