using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurgerAssemblyBehavior : MonoBehaviour{    
    
    #region Public Variables

    //Public References
    [Header("Rigidbody and Basic Stuff for Unity")]
    [Tooltip("Rigidbody of the Object")]
    public Rigidbody2D rbody; //Rigidbody of the Object
    [Tooltip("The AudioSource of the object")]
    public AudioSource audio;

    [Header("Text for UI Stuff")]
    [Tooltip("The game's instructions")]
    public Text Instructions; //The game's instructions

    [Header("Food Variables")]
    [Tooltip("The  of the mixing bowl Prefab")]
    public GameObject[] Foods;
    [Tooltip("Where the food will drop from")]
    public Transform DropLocation;
    [Tooltip("Timer for the food dropping")]
    public float DropTime = 1;

    [Header("Kitchen Controller stuff")]
    [Tooltip("The Kitchen Controller")]
    public GameObject KitchenController;
    [Tooltip("This is to delay the game when it's done so the player doesn't get INSTANTLY thrown back pretty much.")]
    public float delayTimer = 1.2f;
    List<string> ingredients = new List<string>(){};    //List of ingredients that need to be spawned

    public Button[] buttons; //Array of buttons needed to choose ingredients

    #endregion

    #region Private Variables
    
    /**
        Empty - The pan is empty first stage
        Bottom - Adding the bottom bun
        Meat - Adding the meat
        Lettuce - Adding the Lettuce
        Cheese - Adding the Cheese
        Top - Adding the top bun
        Finished - The pan is done
    **/
    private string BurgerState = "Empty";

    /**
        This goes through the array to add
        foods ingredients
    **/
    private int FoodsListIndex = 0;

    /**
        This variable checks if an ingredient is dropping
        If it is, then it'll start the delay timer and then will unlock when it's done
    **/
    private bool FoodDropping = false;

    /**
        Drop timer counter gets the value from drop time and then
        will basically reset itself when needed
    **/
    private float DropTimerCounter;

    /**
        The PlayingMinigame variable checks so that
        the minigame is being played, if it's not
        being played then it just can't do shit at all
    **/
    private bool PlayingMinigame = false;

    

    #endregion

    void Start(){
        //Sets Drop Timmer Counter to DropTime;
        DropTimerCounter = DropTime;
    }

    // Update is called once per frame
    void Update(){
        //If you're playing the minigame, then like play it
        if(PlayingMinigame){
            RunCookingMinigame();
        } else {
            //Text is empty when you're not doing anything
            Instructions.text = " ";
        }
    }


    //Runs the whole thing to actually play the minigame
    //I'm basically shoving every single function here
    private void RunCookingMinigame(){
        //Checks which state the pan is in and will transition from one to the other
        switch (BurgerState){
            //Empty, there's nothing in the plate
            case "Empty":
                AddFood(FoodsListIndex, "Bottom");
                Instructions.text = "Press Space to add a bun!";
            break;

            case "Bottom":
                AddFood(FoodsListIndex, "Meat");
                Instructions.text = "Press Space to add a patty!";
            break;

            case "Meat":
                AddFood(FoodsListIndex, "Lettuce");
                Instructions.text = "Press Space to add a lettuce!";
            break;

            case "Lettuce":
                AddFood(FoodsListIndex, "Cheese");
                Instructions.text = "Press Space to add a cheese!";
            break;

            case "Cheese":
                AddFood(FoodsListIndex, "Finished");
                Instructions.text = "Press Space to add a bun!";
            break;

            //Finished, it's done
            case "Finished":
                if(delayTimer > 0){
                    delayTimer -= Time.deltaTime;
                } else {
                    KitchenController.SendMessage("MinigameCompleted");
                    PlayingMinigame = false;
                }
                Instructions.text = "Shit is done! Amazing!";
            break;
        }
    }

    #region The Cooking Minigame stuff

    /**
        Adds the food for the specific number
        int FoodNumber - The Index of the food you're going to add
        string - ChangeState the state that will be changed
    **/
    private void AddFood(int FoodNumber, string ChangeState){
        //Creates a Quarternion for rotation stuff
        Quaternion Rotato = new Quaternion(0,0,0,0);

        //Checks for the use button being played
        if(Input.GetButtonDown("Use") && !FoodDropping){
                Object.Instantiate(Foods[FoodNumber],
                    DropLocation.position, Rotato);
                FoodDropping = true;
        }

        if(FoodDropping){
            changeState(ChangeState);
        }

    }

    /**
        This changes the state of the food with a delay and sets
        FoodDropping to false
        string - ChangeState the state that will be changed
    **/
    private void changeState(string ChangeState){
        //The Timer
        if(DropTimerCounter > 0){
                DropTimerCounter -= Time.deltaTime;
            } else {
                FoodDropping = false;
                DropTimerCounter = DropTime;

                if(FoodsListIndex < Foods.Length){
                    FoodsListIndex++;
                }

                BurgerState = ChangeState;
            }
    }

    #endregion

    #region Audio Stuff

    //If it's being stirred fast enough it'll make noises if not it'll shut up

    #endregion

    #region Interacting other things

    /**
        This gets called by another minigame 
    **/
    public void TurnOnMinigame(){
        PlayingMinigame = true;
    }

    /*
    Called by ingredient buttons
    Tells program what ingredients to add
    Removes input str if it's already been input
    */
    public void AddIngredient(string str){
        if(ingredients.Contains(str)){
            ingredients.Remove(str);
        }
        else{
            ingredients.Add(str);
        }
    }

    /*
    Unpauses game and changes BurgerState
    Used for signaling to minigame ingredients have been picked
    */
    public void IngredientsChosen(){
        BurgerState = "Empty";
        Time.timeScale = 1f;
        
    }

    /*
    Moves given UI buttons on screen
    Calls MoveButtonOnScreen function of script attached to given buttons
    ANY BUTTONS THAT ARE USED FOR UI NEED IngredientButtonBehavior.cs
    */
    void MoveButtons(){
        foreach(Button button in buttons){
            Debug.Log("moving a button");
            button.GetComponent<IngredientButtonBehavior>().MoveButtonOnScreen(850);
        }
    }

    #endregion

}
