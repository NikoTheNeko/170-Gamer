using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public Vector3 basePosition = new Vector3(3, -1, 0);
    public GameObject cutLine;
    public Sliceable target;
    public float errorOffset = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        cutLine.transform.position = target.GetNextSlicePos();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 knifePosition = new Vector3(mousePosition.x, basePosition.y, basePosition.z);
        transform.position = Vector2.Lerp(transform.position, knifePosition, moveSpeed);

        if(Input.GetMouseButtonDown(0))
        {
            if(knifePosition.x <= cutLine.transform.position.x + errorOffset && knifePosition.x >= cutLine.transform.position.x - errorOffset)
            {
                if(target.Slice())
                {
                    Debug.Log("sliced tha fucccccc up");
                    cutLine.GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    cutLine.transform.position = target.GetNextSlicePos();
                }
            }
        }
    }

    
}
