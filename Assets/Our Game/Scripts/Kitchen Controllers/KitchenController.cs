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

    #region Minigame 1
    [Header("Minigame 1")]
    [Tooltip("The trigger for the pan minigame")]
    public GameObject PanMinigameTrigger;
    [Tooltip("The Pan Minigame itself")]
    public GameObject PanMinigame;
    [Tooltip("Pan Minigame Show")]
    public Transform PanMinigameShow;
    [Tooltip("Pan Minigame Hide")]
    public Transform PanMinigameHide;
    #endregion

    #region Minigame 2
    [Header("Minigame 2")]
    [Tooltip("The trigger for the pot minigame")]
    public GameObject PotMinigameTrigger;
    [Tooltip("The Pot Minigame itself")]
    public GameObject PotMinigame;
    [Tooltip("Pot Minigame Show")]
    public Transform PotMinigameShow;
    [Tooltip("Pot Minigame Hide")]
    public Transform PotMinigameHide;
    #endregion    
    
    #region Minigame 3
    [Header("Minigame 3")]
    [Tooltip("The trigger for the pot minigame")]
    public GameObject MixingBowlMinigameTrigger;
    [Tooltip("The MixingBowl Minigame itself")]
    public GameObject MixingBowlMinigame;
    [Tooltip("MixingBowl Minigame Show")]
    public Transform MixingBowlMinigameShow;
    [Tooltip("Pot Minigame Hide")]
    public Transform MixingBowlMinigameHide;
    #endregion

    #region Minigame 5
    [Header("Minigame 5")]
    [Tooltip("The trigger for the pot minigame")]
    public GameObject BurgerMinigameTrigger;
    [Tooltip("The Burger Minigame itself")]
    public GameObject BurgerMinigame;
    [Tooltip("Burger Minigame Show")]
    public Transform BurgerMinigameShow;
    [Tooltip("Pot Minigame Hide")]
    public Transform BurgerMinigameHide;
    #endregion

    #region Minigame Selection
    [Header("Minigame Selection items")]
    [Tooltip("An array of all the foods you can display")]

    public GameObject[] FoodToDisplay;
    [Tooltip("This is the UI for the recipe selection")]
    public Transform RecipeUI;
    public Transform HideUI;
    public Transform ShowUI;

    #endregion

    [Header("Temp Ass Shit ass Fuck")]
    public Transform SpawnHereFood;

    #endregion

    #region Private Variables
    /** The recipe that will be shown at the end
        0 - nothing
        1 - Soup
        2 - Burger
        3 - Bread
    **/
    private int RecipeNumber = 0;

    /**This holds the recipes for the recipes above
        0 - empty
        1 - {2, -1}
        2 - {1, 5, -1}
        3 - {3, -1}
    **/
    private int[][] RecipeArrays = new int[][]{
        new int[] {0},
        new int[] {2, -1},
        new int[] {1, 5, -1},
        new int[] {3, -1}
    };

    /**
        Controls the Recipe Selection to show the thing
    **/
    private bool ShowRecipes = false;
    
    /**
        -1 - Completed wow amazing boss
        0 - Nothing, waiting for recipe
        1 - Pan minigame
        2 - Pot minigame
        3 - Mixing Bowl minigame
        4 - Knife Minigame
        5 - Assembly Minigame
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
        ShowRecipeUI();
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
                MixingBowlMinigameTrigger.SendMessage("ToggleDisplayOff");
                BurgerMinigameTrigger.SendMessage("ToggleDisplayOff");
                GameObject.Instantiate(FoodToDisplay[RecipeNumber], SpawnHereFood);
                MinigameNumber = -99;
            break;
            //Off
            case 0:
                PanMinigameTrigger.SendMessage("ToggleDisplayOff");
                PotMinigameTrigger.SendMessage("ToggleDisplayOff");
                MixingBowlMinigameTrigger.SendMessage("ToggleDisplayOff");
                BurgerMinigameTrigger.SendMessage("ToggleDisplayOff");
            break;
            //Pan minigame
            case 1:
                PanMinigameTrigger.SendMessage("ToggleDisplayOn");
                PotMinigameTrigger.SendMessage("ToggleDisplayOff");
                MixingBowlMinigameTrigger.SendMessage("ToggleDisplayOff");
                BurgerMinigameTrigger.SendMessage("ToggleDisplayOff");

            break;
            //Pot minigame
            case 2:
                PanMinigameTrigger.SendMessage("ToggleDisplayOff");
                PotMinigameTrigger.SendMessage("ToggleDisplayOn");
                MixingBowlMinigameTrigger.SendMessage("ToggleDisplayOff");
                BurgerMinigameTrigger.SendMessage("ToggleDisplayOff");
            break;
            //Mixing Bowl Minigame
            case 3:
                PanMinigameTrigger.SendMessage("ToggleDisplayOff");
                PotMinigameTrigger.SendMessage("ToggleDisplayOff");
                MixingBowlMinigameTrigger.SendMessage("ToggleDisplayOn");
                BurgerMinigameTrigger.SendMessage("ToggleDisplayOff");

            break;

            case 5:
                PanMinigameTrigger.SendMessage("ToggleDisplayOff");
                PotMinigameTrigger.SendMessage("ToggleDisplayOff");
                MixingBowlMinigameTrigger.SendMessage("ToggleDisplayOff");
                BurgerMinigameTrigger.SendMessage("ToggleDisplayOn");
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

            case 3:
                runMinigame(MixingBowlMinigame, MixingBowlMinigameHide, MixingBowlMinigameShow);
            break;

            case 5:
                runMinigame(BurgerMinigame, BurgerMinigameHide, BurgerMinigameShow);
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

    #region Recipe Selection

    /**
        This basically starts the minigames. Call it
        with the corresponding recipe number and it'll
        handle the rest
    **/
    public void StartCooking(int Recipe){
        if(ShowRecipes){
            RecipeNumber = Recipe;
            player.SendMessage("SetCanMove", true);
            if(MinigameNumber == 0){
                MinigameRecipe = RecipeArrays[Recipe];
                MinigameNumber = MinigameRecipe[0];
                MinigameStep = 0;
            }
            ShowRecipes = false;
        }
    }

    /**
        Stops player from moving and sets show recipes to true
    **/
    public void SelectRecipe(){
        //Stopst he player from moving
        player.SendMessage("SetCanMove", false);
        ShowRecipes = true;
    }

    /**
        This shows the Ui layer on screen
    **/
    private void ShowRecipeUI(){
        //If show recipes is true, show the recipes
        //Else, then hide
        //Does a basic LERP
        if(ShowRecipes){
            Vector3 newPos = Vector3.Lerp(RecipeUI.position, ShowUI.position, SmoothSpeed);
            RecipeUI.position = newPos;
        } else {
            Vector3 newPos = Vector3.Lerp(RecipeUI.position, HideUI.position, SmoothSpeed);
            RecipeUI.position = newPos; 
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

    #endregion

}