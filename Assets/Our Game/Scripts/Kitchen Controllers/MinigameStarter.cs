using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameStarter : MonoBehaviour{

    #region Public Variables
    [Header("Basic rigidbody stuff")]
    [Tooltip("Box Collider for the thing as a trigger")]
    public BoxCollider2D trigger;

    [Tooltip("Player Collider")]
    public Collider2D playerCollider;

    [Header("Kitchen Controller")]
    [Tooltip("This is the kitchen controller")]
    public GameObject KitchenController;

    #endregion

    // Update is called once per frame
    void Update(){
        MeDoCookNow();
    }

    /**Just Temp stuff but this will send a message to the
    Kitchen COntroller basically saying "Hey start the minigame"
    "Fucker" and then send the minigame array.
    **/
    private void MeDoCookNow(){
        if(trigger.IsTouching(playerCollider)){
            if(Input.GetButtonDown("Use")){
                KitchenController.SendMessage("SelectRecipe");
            }
        }
    }


}
