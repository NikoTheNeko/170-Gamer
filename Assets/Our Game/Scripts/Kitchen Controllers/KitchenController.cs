using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenController : MonoBehaviour{

    #region Public Variables

    [Header("Camera stuff")]
    [Tooltip("Minigame Camera to see the minigames")]
    public Transform MinigameCamera;
    [Tooltip("Smooth speed to make the minigames transition good")]
    public float SmoothSpeed = 0.05f;
    [Tooltip("A time for the timer to delay stuff idk it's straightforward")]
    public float delay = 5f;
    [Tooltip("The Player so I can make him fucking stop")]
    public GameObject player;

    [Header("Minigame 1")]
    [Tooltip("The trigger for the pan minigame")]
    public GameObject PanMinigameTrigger;
    [Tooltip("The Pan Minigame itself")]
    public GameObject PanMinigame;
    [Tooltip("Pan Minigame Show")]
    public Transform PanMinigameShow;
    [Tooltip("Pan Minigame Hide")]
    public Transform PanMinigameHide;

    [Header("Minigame 2")]
    [Tooltip("The trigger for the pot minigame")]
    public GameObject PotMinigameTrigger;
    [Tooltip("The Pot Minigame itself")]
    public GameObject PotMinigame;
    [Tooltip("Pot Minigame Show")]
    public Transform PotMinigameShow;
    [Tooltip("Pot Minigame Hide")]
    public Transform PotMinigameHide;

    [Header("Temp Ass Shit ass Fuck")]
    public Transform SpawnHereFood;
    public GameObject FoodToDisplay;

    #endregion

    #region Private Variables
    
    /**
        -1 - Completed wow amazing boss
        0 - Nothing, waiting for recipe
        1 - Pan minigame
        2 - Pot minigame
    **/
    private int MinigameNumber = 0;

    /**
        MinigameRecipe holds an array with
        all of the minigames you have to play in a
        certain order given.
        END WITH A -1 SO THAT WAY YOU KNOW IT DONE.
    **/
    private int[] MinigameRecipe = {0};

    /**
        Minigame step is how you traverse the array
        above. This number will be incremented through a function.
    **/
    private int MinigameStep = 0;

    //Minigame triggers
    private bool trigger = false;

    //Showing the minigame
    private bool showingMinigame = false;

    //Time remaining is for the timer itself.
    private float timeRemaining;

    //Transitions back peacefully
    //No war crimes here :)
    //Cough cough eric
    private bool TransitionBack = false;
    
    #endregion

    //Start function
    void Start(){
        timeRemaining = delay;
    }

    // Update is called once per frame
    void Update(){
        IONManager();
        MinigameController();
    }

    #region Kitchen Controller as a whole

    //Handles the IONs to show or not depending on whatever needs
    //to be shown
    private void IONManager(){
        switch(MinigameNumber){
            //Completion
            case -1:
                PanMinigameTrigger.SendMessage("ToggleDisplayOff");
                PotMinigameTrigger.SendMessage("ToggleDisplayOff");
                GameObject.Instantiate(FoodToDisplay, SpawnHereFood);
                MinigameNumber = -99;
            break;
            //Off
            case 0:
                PanMinigameTrigger.SendMessage("ToggleDisplayOff");
                PotMinigameTrigger.SendMessage("ToggleDisplayOff");
            break;
            //Pan minigame
            case 1:
                PanMinigameTrigger.SendMessage("ToggleDisplayOn");
                PotMinigameTrigger.SendMessage("ToggleDisplayOff");
            break;
            //Pot minigame
            case 2:
                PanMinigameTrigger.SendMessage("ToggleDisplayOff");
                PotMinigameTrigger.SendMessage("ToggleDisplayOn");
            break;
        }
    }

    private void MinigameController(){
        switch(MinigameNumber){

            case 1:
                runMinigame(PanMinigame, PanMinigameHide, PanMinigameShow);
            break;

            case 2:
                runMinigame(PotMinigame, PotMinigameHide, PotMinigameShow);
            break;    


        }
    }

    //Basis of running all the minigames
    private void runMinigame(GameObject minigame, Transform hidden, Transform show){
        //First deal with camera movements
        //If the minigame isn't being shown, then don't show it, if it is then show it
        //Pretty straight forward I think
        if(showingMinigame){
            //Moves the camera
            MoveCamera(show);
            //Stopst he player from moving
            player.SendMessage("SetCanMove", false);
            //When it's done transitioning it'll start the minigame and then reset the timer
            if(delayTimer()){
                minigame.SendMessage("TurnOnMinigame");
                timeRemaining = delay;
            }
        } else {
            MoveCamera(hidden);
        }

        //This does a check if the thing is pressed, if it is then show the minigame
        if(trigger){
            showingMinigame = true;
            trigger = false;
        }

        //After it transitions it will increment
        if(TransitionBack){
            TransitionsMinigameButSlowly();
        }
    }

    /**
        Used by other things to get called on by minigames
        This is so you can signal that the minigame is completed and it
        can move to the next one
        Sets showing minigame to false so it hides the minigame
        Then it increments minigame number (we'll probably have to change this later)
    **/
    public void MinigameCompleted(){
        TransitionBack = true;
        showingMinigame = false;
    }

    /**
        This basically fuckin uhh
        So it gets triggered from above and then lets it move back
        slowly so it goes UP and not like sideways ands hit
        that was an issue
        i miss timers
        i miss you bolt
    **/
    private void TransitionsMinigameButSlowly(){
        //delays the inevitable
        if(delayTimer()){
            //If you're not at the max at the minigame recipe
            //array, increment one then transition everything
            if(MinigameStep < MinigameRecipe.Length)
                MinigameStep += 1;
            //Sets the current cell of the minigame recipe array
            //To the current minigame number
            MinigameNumber = MinigameRecipe[MinigameStep];
            timeRemaining = delay;
            TransitionBack = false;
            player.SendMessage("SetCanMove", true);
        }
    }

    /**
        Runs a delay timer so that you can delay stuff
    **/
    private bool delayTimer(){
        if(timeRemaining > 0){
            timeRemaining -= Time.deltaTime;
            return false;
        } else {
            timeRemaining = delay;
            return true;
        }
    }


    #endregion

    #region Camera Movement

    /**
        Moves camera to the desired location
        This is basically lerp but like cleaner and specifically
        for the camera
        Clean ain't it?
        I know it's actually not that clean but more specific lerp
    **/
    private void MoveCamera(Transform desiredPos){
        Vector2 newPos = Vector2.Lerp(MinigameCamera.position, desiredPos.position,
                                        SmoothSpeed);
        MinigameCamera.position = newPos;
    }

    #endregion

    #region Interacting with other objects thing

    /**
    *   Okay so quick thing on this works. Basically
    *   The interactable object calls TriggerActivated
    *   If the minigame number matches the number sent in
    *   from that minigame, then it will turn trigger to true
    *   You can use TriggerMinigame to turn on the minigames by
    *   calling that function and the minigame name
    *   okay cool that's it jesus fucking christ I hate code
    *   I'm sorry if anyone has to read this, I'm making it as
    *   modular as I can okay look we have to have like 300
    *   fucking minigames on this god damn game okay I'm trying my
    *   fucking best.
    */

    //For the minigame triggers, this helps
    //if the player walks on the trigger and it works it do
    public void TriggerActivated(int triggerNumber){
        if(MinigameNumber == triggerNumber)
            trigger = true; 
    }

    /**
        This triggers the minigame based on the minigame based on
        which trigger is activated
    **/
    private void TriggerMinigame(GameObject minigame){
        //If it's triggered
        if(trigger){
            minigame.SendMessage("TurnOnMinigame");
            trigger = false;
        }
    }

    /**
        This basically starts the minigames. Call it
        with an array with the recipe list and then it'll
        start the cooking yes I do the cleaning
    **/
    public void StartCooking(int[] RecipeArray){
        if(MinigameNumber == 0){
            MinigameRecipe = RecipeArray;
            MinigameNumber = MinigameRecipe[0];
            MinigameStep = 0;
        }
    }

    #endregion



}
