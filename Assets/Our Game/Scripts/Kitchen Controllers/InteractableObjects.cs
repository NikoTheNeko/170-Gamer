using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour{

    #region Public Variables

    [Header("Rigidbody and Other Basic Stuff")]
    [Tooltip("The Box Collider for the object")]
    public BoxCollider2D bcol;
    [Tooltip("The Collider for the player")]
    public Collider2D playerCollider;
    [Tooltip("Kitchen Controller")]
    public GameObject KitchenController;
    [Tooltip("Minigame Number")]
    public int MinigameNumber = 0;

    [Header("ION Positions and Speed")]
    [Tooltip("InteractableObjectNotification")]
    public Transform ION;
    [Tooltip("This is when the Interactable Object Notification is hidden")]
    public Transform HiddenION;
    [Tooltip("This is when the Interactable Object Notification is shown")]
    public Transform ShownION;

    [Tooltip("How fast the ION will move")]
    public float SmoothSpeed = 0.125f;

    #endregion

    #region Private Variables
    
    //Controls if you can see the notification
    public bool DisplayION = false;
    
    #endregion

    // Update is called once per frame
    void Update(){
        //Controlers the ION to show or hide it
        IONController();
        //Sees if the player is interacting with this
        CheckTrigger();
    }

    #region Interactable Notification Stuff

    /**
        Shows or hides the
        Interactable Object Notification
        I'm sorry that's such a long name
        ION sounds cool as fuck though
    **/
    private void IONController(){
        //Checks if it should display the ION or not
        if(DisplayION){
            //If display is true then show
            Vector2 NewPos = 
                Vector2.Lerp(ION.position,
                            ShownION.position,
                            SmoothSpeed);
            ION.position = NewPos;
        } else {
            //If it ain't true then hide
            Vector2 NewPos = 
                Vector2.Lerp(ION.position,
                            HiddenION.position,
                            SmoothSpeed);
            ION.position = NewPos;
        }
    }

    /**
        Displays if the ION is turning on or off
    **/
    public void ToggleDisplayOn(){
        DisplayION = true;
    }

    public void ToggleDisplayOff(){
        DisplayION = false;
    }

    #endregion

    #region Interacting

    /**
        This will check if the playre is in the box collider
        if they are it will check if they pressed "Use"
        Then it should trigger the game
        I hope.
    **/
    private void CheckTrigger(){
        //Checks the box collider
        if(bcol.IsTouching(playerCollider)){
            //Checks if player is pressing "Use"
            if(Input.GetButtonDown("Use")){
                KitchenController.SendMessage("TriggerActivated", MinigameNumber);
            }
        }
    }    
    
    #endregion

}
