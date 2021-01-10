using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour{

    public Collider2D enemyHitbox;

    public Collider2D knifeHit;

    private int health = 100;

    // Update is called once per frame
    void Update(){
        
        Debug.Log(health);

        if(health < 0){
            Debug.Log("Holy fuck I'm DEAD! LmaOO!!!");
            Destroy(gameObject);
        }

    }

    public void TakeDamage(){
        health -= 10;
    }
}
