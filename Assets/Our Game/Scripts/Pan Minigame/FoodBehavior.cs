using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehavior : MonoBehaviour
{
    #region Public Variables

    [Header("Rigidbody and Basic Stuff for Unity")]
    [Tooltip("Rigidbody of the Object")]
    public Rigidbody2D rbody; //Rigidbody of the Object
    [Tooltip("Sprite Renderer of the Object")]
    public SpriteRenderer sRenderer; //Sprite Renderer of the Object
    [Tooltip("The Pan for the minigame")]
    public GameObject Pan;

    [Header("Food changing stuff")]
    [Tooltip("How long you want it to cook")]
    public float TimeLength = 5f; //Length of time for cooking
    [Tooltip("Sprite to change to")]
    public Sprite CookedSprite;

    #endregion

    #region Private Varirables
    //Checks if the cooking is done
    private bool CookingDone = false;

    #endregion

    // Update is called once per frame
    void Update(){
        //Has the timer running if it's velocity is more than 0
        if(rbody.velocity.magnitude > 5)
            CookingTimer();
        //If the timer finishes, change the state
        if(TimeLength <= 0){
            FinishCooking();
        }
        
    }

    /**
        The timer for cooking
        Yes I do the cooking
        Yes I do the cleaning
    **/
    private void CookingTimer(){
        //Counts down for the timer
        if(TimeLength > 0){
            TimeLength -= Time.deltaTime;    
        }
    }

    /**
        Finishes the cooking for the
        food. Basically changes the sprite
        and sets it to completed
    **/
    private void FinishCooking(){
        //Sets cooking done to true
        CookingDone = true;
        //Sets the sprite to a different sprite
        sRenderer.sprite = CookedSprite;

        //When the cooking is done it will send a message
        //To the pan and say "Hey run this method" It well then
        //Run said method and compelte it.
        var PanSwap = GameObject.Find(Pan.name);
        PanSwap.SendMessage("FinishCooking");
    }

}
