using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class inventory : MonoBehaviour
{
    #region Public Variables
    [Header("In Scene References")]
    [Tooltip("Takes in a Sprite and a name associated with that Sprite. The name should be the same as key used in inventoryDict. Place in order of appearance in inventory menu.")]
    public List<IngredientImage> ingredientPictures = new List<IngredientImage>();
    [Tooltip("Texts and Images for displaying the inventory. ")]
    public List<Display> ingredientDisplays = new List<Display>();
    #endregion
    //dictionary of objects being stored
    //key is name of ingredient
    //value is number of ingredient in inventory
    private Dictionary<string, int> inventoryDict = new Dictionary<string, int>();
    private int OffsetX = 10; //amount of leeway in x axis
    private int OffsetY = 10; //amount of leeway in y axis
    

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        foreach(Display temp in ingredientDisplays){
            temp.Deactivate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        displayIngredients();
        checkForMouseOver();
    }

    //returns the amount of an ingredient
    //returns -1 if the ingredient has not been discovered
    public int getAmount(string ingredient){
        if(inventoryDict.ContainsKey(ingredient)){
            return inventoryDict[ingredient];
        }
        else{
            return -1;
        }
    }

    //subtracts modifier from amount in inventory linked to key
    //returns true if subtraction successful
    //returns false if there aren't enough ingredients or ingredient doesn't exist
    public bool subtract(string ingredient, int modifier){
        //check if ingredient has been found
        if(inventoryDict.ContainsKey(ingredient)){
            int temp = inventoryDict[ingredient];
            temp -= modifier;
            if(temp < 0){
                return false;
            }
            //can't subtract
            else{
                inventoryDict[ingredient] = temp;
                return true;
            }
        }
        else{
            return false;
        }
    }

    //adds modifier to amount in inventory linked to key
    //returns true if addition succesful
    //returns false if addition couldn't be done
    public bool add(string ingredient, int modifier){
        //check if ingredient has been found
        if(inventoryDict.ContainsKey(ingredient)){
            int temp = inventoryDict[ingredient];
            temp += modifier;
            
            //if addition results in a negative return false
            if(temp < 0){
                return false;
            }
            //else do addition
            else{
                inventoryDict[ingredient] = temp;
                return true;
            }
        }
        //adds ingredient to inventoryDict, discovering ingredient
        else{
            inventoryDict[ingredient] = modifier;
            return false;
        }
    }

    //Adds 1 to the amount of the ingredient indicated by key
    public void addOne(string ingredient){
        //check if ingredient has been found
        if(inventoryDict.ContainsKey(ingredient)){
            int temp = inventoryDict[ingredient];
            temp += 1;
            if(temp < 0){

            }
            else{
                inventoryDict[ingredient] = temp;
            }
        }
        else{
            inventoryDict[ingredient] = 1;
        }
    }

    //returns true if dictionary already has given key
    //returns false if key not found
    public bool discovered(string ingredient){
        return inventoryDict.ContainsKey(ingredient);
    }

    //Displays all discovered ingredients on given display objects
    //If ingredient in ingredientPictures hasn't been discovered, it will be passed over when displaying
    private void displayIngredients(){
        int ingredient = 0;
        int display = 0;
        while(ingredient < ingredientPictures.Count && display < ingredientDisplays.Count){

            //if the player has discovered an ingredient display it
            if(inventoryDict.ContainsKey(ingredientPictures[ingredient].name)){
                ingredientDisplays[display].Activate();
                ingredientDisplays[display].image.sprite = ingredientPictures[ingredient].picture;
                ingredientDisplays[display].name.text = ingredientPictures[ingredient].name;
                ingredientDisplays[display].amount.text = "x" +  inventoryDict[ingredientPictures[ingredient].name].ToString();
                display++;
            }
            ingredient++;
        }
    }

    private void checkForMouseOver(){
        foreach(Display target in ingredientDisplays){
            if(Input.mousePosition.x < target.image.gameObject.transform.position.x + OffsetX && 
            Input.mousePosition.x > target.image.gameObject.transform.position.x - OffsetX && 
            Input.mousePosition.y < target.image.gameObject.transform.position.y + OffsetY && 
            Input.mousePosition.y > target.image.gameObject.transform.position.y - OffsetY){
                //info should be displayed here
                Debug.Log("you moused over me!");
        }
        }
       
    }

}

[System.Serializable]
public class IngredientImage{
    public Sprite picture;
    public string name;
}

[System.Serializable]
public class Display{
    public Text name;
    public Text amount;
    public Image image;

    //Sets all members inactive
    public void Deactivate(){
        name.gameObject.SetActive(false);
        amount.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
    }

    //Sets all members active
    public void Activate(){
        name.gameObject.SetActive(true);
        amount.gameObject.SetActive(true);
        image.gameObject.SetActive(true);
    }
}
