using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour{

    public Collider2D enemyHitbox;

    public Collider2D knifeHit;

    public float attackRange;
    //mf to be killed
    public GameObject player;

    private string enemyState = "idle";
    public float speed = 10.0f;
    private int health = 100;
    private Vector3 spawnPos;
    private int idleTimer;
    private int walkTimer;

    private void Start()
    {
        idleTimer = 500;
        walkTimer = 800;
        spawnPos = transform.position;
    }

    // Update is called once per frame
    void Update(){
        checkDead();
        
        switch (enemyState)
        {
            case "idle":
                EnemyIdle();
                break;

            case "walkAround":
                EnemyWalk();
                break;

            case "combat":
                EnemyCombat();
                break;

            case "escape":
                EnemyEscape();
                break;

            case "goHome":
                EnemyGoHome();
                break;
        }
        Debug.Log(health);
    }

    private void checkDead()
    {
        if (health < 0)
        {
            Debug.Log("Holy fuck I'm DEAD! LmaOO!!!");
            Destroy(gameObject);
        }
    }

    private void checkCombat()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            enemyState = "combat";
        }
    }
    private void EnemyIdle()
    {
        if (idleTimer > 0)
        {
            idleTimer--;
        }
        else
        {
            idleTimer = (int)Random.Range(20, 700);
            enemyState = "walkAround";
        }
    }

    private void EnemyWalk()
    {
        if (walkTimer > 0)
        {
            walkTimer--;
            //move
        }
        else
        {
            walkTimer = (int)Random.Range(300, 1000);
            enemyState = "idle";
        }
    }
    private void EnemyCombat()
    {

    }
    private void EnemyEscape()
    {
    }
    private void EnemyGoHome()
    {
    }

    public void TakeDamage(int amount){
        health -= amount;
    }
}
