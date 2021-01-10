using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatTesting : MonoBehaviour{

    #region Public Variables

    [Header("Rigidbody and Basic Stuff for Unity")]
    [Tooltip("Rigidbody of the Object")]
    public Rigidbody2D rbody; //Rigidbody of the Object

    [Header("Controls the speed of the character")]
    [Tooltip("How fast it can go nyoom")]
    public int PlayerSpeed = 5;

    [Header("Combat Stuff")]
    [Tooltip("This is the hit box thing")]
    public GameObject hitbox;

    #endregion

    #region Private Varirables
    public bool CanMove = true;

    #endregion

    // Update is called once per frame
    void Update(){
        if(CanMove){
            PlayerMovement();
        } else {
            //Stops movement if you cannot move
            rbody.velocity = new Vector2(0,0);
        }

        CheckAttack();
    }

    /**
        Player movement allows the player to move
        Kinda straight forward I think
    **/
    private void PlayerMovement(){
        //First gets the movement vectors
        Vector2 MovementVector = 
        new Vector2((Input.GetAxis("Horizontal") * PlayerSpeed),
                    (Input.GetAxis("Vertical") * PlayerSpeed));

        //Sets velocity to the movement vector
        rbody.velocity = MovementVector;
    }

    /**
        Getters and Setters for Can Move
        Get CanMove checks for Can Move
        Set CanMove sets it to the desired value
    **/
    public bool GetCanMove(){
        return CanMove;
    }

    public void SetCanMove(bool val){
        CanMove = val;
    }

    public void CheckAttack(){
        Quaternion rotato = new Quaternion(0,0,0,0);
        Vector3 Offset = transform.position + new Vector3(1,0,0);
        if(Input.GetButtonDown("Use")){
            Object.Instantiate(hitbox, Offset, rotato);
        }
    }

}
