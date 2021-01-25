using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientButtonBehavior : MonoBehaviour
{
    bool move = false;
    bool moveleft = true;
    bool grayed = false;
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

        //Script for moving the attached button
        if(move){
            if(!moveleft && pos.x < x){
                Debug.Log("Adding");
                pos.x++;
                GetComponent<Transform>().position = pos;
            }
            else if(moveleft && pos.x > x){
                Debug.Log("subtracting");
                pos.x--;
                GetComponent<Transform>().position = pos;
            }
        }
    }

    /*
    Tells script to move button off screen
    I'm assuming left is off screen right now    
    */
    public void MoveButtonOffScreen(int temp){
        x = temp;
        move = true;
        self.interactable = false;
        moveleft = false;
    }

    /*
    Moves buttons on screen
    Also assuming right is on screen
    */
    public void MoveButtonOnScreen(int temp){
        Debug.Log("called button move on screen " + temp);
        x = temp;
        move = true;
        moveleft = true;
    }

    /*
    Changes color of button from white to grey, or vice versa
    Indicates to player what ingredients have been added
    */
    public void ColorChange(){
        if(grayed){
            GetComponent<Image>().color = new Color32(255,255,255,255);
            grayed = false;
        }
        else{
            GetComponent<Image>().color = new Color32(118,118,118,255);
            grayed = true;
        }
    }

}
