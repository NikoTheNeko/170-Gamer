using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public static bool isOpen = false;

    public GameObject QuestList;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            OpenQuests();
        }
    }

    public void OpenQuests()
    {
        Debug.Log("uh oh stinky");
        Animator animator = QuestList.GetComponent<Animator>();
        if (animator != null)
        {
            Debug.Log("fuckshitpoopassssssssssssss");
            isOpen = animator.GetBool("open");

            animator.SetBool("open", !isOpen);
        }
    }
}
