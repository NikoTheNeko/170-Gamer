using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotScript : MonoBehaviour
{
    public float speed = 10f;

    bool isRotating = false;
    Vector2 movementVec;
    Quaternion startRotation;
    Vector3 startPos;

    private void Start()
    {
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            if (transform.up.y >= -0.25)
            {
                transform.Rotate(new Vector3(0, 0, 2) * Time.deltaTime * speed);
            }
        }
        else
        {
            if(transform.up.y != 1)
            {
                transform.Rotate(new Vector3(0, 0, 2) * Time.deltaTime * (-speed));
            }
        }
    }
}
