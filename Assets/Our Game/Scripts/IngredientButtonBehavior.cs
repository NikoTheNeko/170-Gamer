using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientButtonBehavior : MonoBehaviour
{
    bool move = false;
    int x = 0;
    public Button self;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = GetComponent<Transform>().position;
        if(move && pos.x < x){
            pos.x++;
            GetComponent<Transform>().position = pos;
        }
    }

    /*
    Increments x var of given Vector3, meant for moving buttons on/off screen
    moveOnScreen dictates direction object moves
        False = moving right
        True = moving left
    Need to reassign pos to target GameObject transform later
    */
    public void MoveButtonOffScreen(int temp){
        x = temp;
        move = true;
        self.interactable = false;
    }

}
