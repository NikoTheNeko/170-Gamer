using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixingBehavior : MonoBehaviour{
    #region Public Variables

    //Public References
    [Header("Rigidbody and Basic Stuff for Unity")]
    [Tooltip("Rigidbody of the Object")]
    public Rigidbody2D rbody; //Rigidbody of the Object
    [Tooltip("The AudioSource of the object")]
    public AudioSource audio;

    [Header("Spoon Stuff")]
    [Tooltip("Controls how fast you can stir")]
    public int SpoonSpeed = 10; //Controls how fast you can move the spoon
    [Tooltip("Rigidbody of the Object")]
    public Rigidbody2D spoonbody; //Rigidbody of the Object

    [Header("Text for UI Stuff")]
    [Tooltip("The game's instructions")]
    public Text Instructions; //The game's instructions

    [Header("Food Variables")]
    [Tooltip("The Broth of the Soup Prefab")]
    public GameObject SoupBroth; //Adds the broth
    [Tooltip("The food to be added")]
    public GameObject Food1; //The food to be added
    public GameObject Food2; //The other food to be added
    [Tooltip("How long you want it to cook")]
    public float TimeLength = 5f; //Length of time for cooking
    [Tooltip("The amount of food")]
    public int AmountOfFood = 10; //The amount of food

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
        PanState controls the state of the pan
        Empty - The pan is empty first stage
        Filled - The pan is Filled so it's all gweasy
        Cooking - The pan has food and is cooking
        Finished - The pan is done
    **/
    private string PotState = "Picking";

    /**
        Allows it so we can check the angular velocity
        of the soup later
    **/
    private Rigidbody2D SoupCheck;

    /**
        The PlayingMinigame variable checks so that
        the minigame is being played, if it's not
        being played then it just can't do shit at all
    **/
    private bool PlayingMinigame = false;

    

    #endregion

    // Update is called once per frame
    void Update(){
        //If you're playing the minigame, then like play it
        if(PlayingMinigame){
            RunCookingMinigame();
        } else {
            //Text is empty when you're not doing anything
            Instructions.text = " ";
            TurnDownVolume();
        }
    }


    //Runs the whole thing to actually play the minigame
    //I'm basically shoving every single function here
    private void RunCookingMinigame(){
        //Checks which state the pan is in and will transition from one to the other
        switch (PotState){
            //State for picking ingredients
            //Pauses time and moves UI buttons onto screen
            case "Picking":
                MoveButtons();
                Time.timeScale = 0f;
                Instructions.text = "Choose your ingredients";
            break;
            //Empty, there's nothing in the pan
            //Press space to add oil
            case "Empty":
                AddBroth();
                Instructions.text = "Press Space to Add Slime Broth";
            break;

            //Oiled, it's Filled
            //Press Space to add food
            case "Filled":
                AddFood();
                Instructions.text = "Press Space to Add Carrots";
            break;

            //Coking, it's Cooking
            //Press up and down to shake the pan
            case "Cooking":
                CookFood();
                Instructions.text = "Press The Arrow Keys to Stir!";
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
                TurnDownVolume();
            break;
        }
    }

    #region The Cooking Minigame stuff

    /**
        Lets player press space to add broth
    **/
    private void AddBroth(){
        //Creates a spawn position for the food
        //Basically puts it in the position then
        //Offsets it a bit
        Vector3 SpawnPosition = rbody.position;
        SpawnPosition += new Vector3(0,0,10);

        //Creates a Quarternion for rotation stuff
        Quaternion Rotato = new Quaternion(0,0,0,0);

        //If the player presses the use button
        if(Input.GetButtonDown("Use")){
            GameObject temp;
            temp = Object.Instantiate(SoupBroth, SpawnPosition, Rotato);
            //Assigns Soupcheck the rigidbody2D of the soup
            temp = GameObject.Find(temp.name);
            SoupCheck = temp.GetComponent<Rigidbody2D>();
            //Changes state
            PotState = "Filled";
        }
        
    }

    /**
        Adds the food to the pot
    **/
    private void AddFood(){
        //Gets input down
        if(Input.GetButtonDown("Use")){
            foreach(string ingredient in ingredients){
                //Adds 4 pieces of food
                for(int i = 0; i != AmountOfFood; i++){
                    //Creates a spawn position for the food
                    //Basically puts it in the position then
                    //Offsets it a bit
                    Vector3 SpawnPosition = rbody.position;
                    Vector3 Offset = new Vector3(0, (i/10), 10);
                    SpawnPosition = SpawnPosition + Offset;

                    //Creates a Quarternion for rotation stuff
                    Quaternion Rotato = new Quaternion(0,0,0,0);

                    //Creates the food onto the thing
                    switch(ingredient){
                        case "carrot":
                            Object.Instantiate(Food1, SpawnPosition, Rotato);
                        break;
                        case "niko":
                            Object.Instantiate(Food2, SpawnPosition, Rotato);
                        break;
                    }
                    
                }
            }
            

            PotState = "Cooking";
        }    
    }

    /**
    Cooks the food basically. This is when the pan is able to shake
    That's about it.
    **/
    private void CookFood(){
        //Just to keep things cleaner
        //I moved the y axis part to a separate part so it just looks nicer.
        //Code-wise at least
        //Yes I know this eats up more memory, do I care? No
        //First gets the movement vectors
        Vector2 MovementVector = 
        new Vector2((Input.GetAxis("Horizontal") * SpoonSpeed),
                    (Input.GetAxis("Vertical") * SpoonSpeed));

        //Sets velocity to the movement vector
        spoonbody.velocity = MovementVector;

        //Audio stuff
        MixingVolumeBehavior();

        //Checks the angular velocity of the slime
        if(Mathf.Abs(SoupCheck.angularVelocity) > 100)
            CookingTimer();

        if(TimeLength <= 0)
            PotState = "Finished";

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

    #endregion

    #region Audio Stuff

    //If it's being stirred fast enough it'll make noises if not it'll shut up
    private void MixingVolumeBehavior(){
        //
        if(Mathf.Abs(SoupCheck.angularVelocity) > 100)
            TurnUpVolume();
        else{
            TurnDownVolume();
        }    
    }

    private void TurnUpVolume(){
        //If the volume is quiet turn it up a bit
        //Using Time Delta Time
        if(audio.volume < 1){
            audio.volume += (0.2f * Time.deltaTime);
        }
    }

    private void TurnDownVolume(){
        //Mutes the audio
        if(audio.volume > 0){
            audio.volume -= (0.5f * Time.deltaTime);
        }
    }

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
    Unpauses game and changes PotState
    Used for signaling to minigame ingredients have been picked
    */
    public void IngredientsChosen(){
        PotState = "Empty";
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
