using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : Player
{
    public Animator anima; // Animator component already taken from player inspector screen 
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackPower = 15;
    public float attackRate = 1.8f;

    float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                
                //wait for 1/attackRate time before attacking again
            }
        }
    }

    void Attack()
    {
        setMoveForce(7f);
        anima.SetTrigger("Attack1");

        // Detects enemies in range of attack
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers); 

        //Damage enemies hit
        foreach(Collider2D enemy in enemiesHit)
        {
            if (enemy.tag == "Player")
            {
                enemy.GetComponent<NPC>().TakeDamage(attackPower);
                Vector2 difference = -(transform.position - enemy.transform.position).normalized;
                enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(difference.x * 8, difference.y * 8), ForceMode2D.Impulse);
            }
        }
        setMoveForce(10f);
    }

    //Draws the attack range
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
