using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    #region Public Variables

    [Header("Target for the camera")]
    [Tooltip("Who the camera focuses on")]
    public Transform Target;

    [Header("Camera Smooth Speed")]
    [Tooltip("Camera can go slurp but slowly")]
    public float SmoothSpeed = 0.125f;
    
    [Header("Camera Offset")]
    [Tooltip("Offsets the camera just a bit")]
    public Vector3 Offset = new Vector3(0,0,-1);

    #endregion

    // Update is called once per frame
    void FixedUpdate(){
        MoveCamera();
    }

    private void MoveCamera(){
        //Gets the target's position and adds the offet
        Vector3 DesiredPos = Target.position + Offset;
        //Gets the position it should be at for the camera
        Vector3 MovementPosition = 
            Vector3.Lerp(transform.position, DesiredPos, SmoothSpeed);
        //Now assigns the camera's position to the movement position
        transform.position = MovementPosition;

    }
}
