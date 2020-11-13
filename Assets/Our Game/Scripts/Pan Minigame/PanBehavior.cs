using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanBehavior : MonoBehaviour
{
    #region Public Variables

    //Public References
    [Header("Rigidbody and Basic Stuff for Unity")]
    [Tooltip("Rigidbody of the Object")]
    public Rigidbody2D rbody; //Rigidbody of the Object
    [Tooltip("Sprite Renderer of the Object")]
    public SpriteRenderer sRenderer; //Sprite Renderer of the Object

    [Header("Speed of the Pan")]
    [Tooltip("Controls how fast you can shake the pan")]
    public int PanShakeSpeed = 10; //Controls how fast you can shake the pan

    [Header("Swapped Sprite for Pan")]
    [Tooltip("Adds the oiled pan for the sprite")]
    public Sprite OiledPan; //Adds the oiled pan for the sprite

    [Header("Text for UI Stuff")]
    [Tooltip("The game's instructions")]
    public Text Instructions; //The game's instructions

    [Header("Food Variables")]
    [Tooltip("The food to be added")]
    public GameObject Food; //The food to be added
    [Tooltip("The amount of food")]
    public int AmountOfFood = 4; //The amount of food

    #endregion

    #region Private Variables
    
    /**
        PanState controls the state of the pan
        Empty - The pan is empty first stage
        Oiled - The pan is oiled so it's all gweasy
        Cooking - The pan has food and is cooking
        Finished - The pan is done
    **/
    private string PanState = "Empty";

    /**
        The PlayingMinigame variable checks so that
        the minigame is being played, if it's not
        being played then it just can't do shit at all
    **/
    public bool PlayingMinigame = false;

    /**
        Checks if the food is completed, if it is
        then neat! Just like, it done fam.
    **/
    private bool Completed = false;

    #endregion

    // Update is called once per frame
    void Update(){
        //If you're playing the minigame, then like play it
        if(PlayingMinigame){
            RunCookingMinigame();
        } else {
            //Text is empty when you're not doing anything
            Instructions.text = "";
        }

    }

    //Runs the whole thing to actually play the minigame
    //I'm basically shoving every single function here
    void RunCookingMinigame(){
        //Checks which state the pan is in and will transition from one to the other
        switch (PanState){
            //Empty, there's nothing in the pan
            //Press space to add oil
            case "Empty":
                Instructions.text = "Press Space to Add Oil";
                AddOil();
            break;

            //Oiled, it's oiled
            //Press Space to add food
            case "Oiled":
                Instructions.text = "Press Space to Add Food";
                AddFood();
            break;

            //Coking, it's Cooking
            //Press up and down to shake the pan
            case "Cooking":
                Instructions.text = "Press up and down to shake the pan";
                CookFood();
            break;

            //Finished, it's done
            case "Finished":
                Instructions.text = "Shit is done! Amazing!";
                Completed = true;
            break;

        }

    }

    #region The Cooking Minigame stuff

    /**
        Changes the sprite to the oiled pan
        so that way it can just vibe
        that's about it really
    **/
    private void AddOil(){
        //If you press down it will change sprites
        //Also updates the state
        if(Input.GetButtonDown("Use")){
            sRenderer.sprite = OiledPan;
            PanState = "Oiled";
        }            
    }

    /**
        Adds the food to the pan
    **/
    private void AddFood(){
        //Gets input down
        if(Input.GetButtonDown("Use")){
            //Adds 4 pieces of food
            for(int i = 0; i != AmountOfFood; i++){
                //Creates a spawn position for the food
                //Basically puts it in the position then
                //Offsets it a bit
                Vector2 SpawnPosition = rbody.position;
                Vector2 Offset = new Vector2(0, (i/10));
                SpawnPosition = SpawnPosition + Offset;

                //Creates a Quarternion for rotation stuff
                Quaternion Rotato = new Quaternion(0,0,0,0);

                //Creates the food onto the thing
                Object.Instantiate(Food, SpawnPosition, Rotato);
            }

            PanState = "Cooking";
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
        float PanShakeDirection = PanShakeSpeed * Input.GetAxis("Vertical");
        //Gets the panshake vector so that way it can move by basically just 
        //Getting the axis shit and the multipling by speed
        //Typical stuff ya kno
        Vector2 PanShakeVector = new Vector2(0, PanShakeDirection);
        rbody.velocity = PanShakeVector;
    }

    /**
        Method to be called on by the food
        to signal that it's done cooking
    **/
    public void FinishCooking(){
        //Stops movement
        rbody.velocity = new Vector2(0,0);
        //Changes it to be finished
        PanState = "Finished";
    }

    /**
        Getter method for Completed
    **/
    public bool GetCompleted(){
        return Completed;
    }

    #endregion



}
