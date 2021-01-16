using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatTesting : MonoBehaviour{

    #region Public Variables

    [Header("Rigidbody and Basic Stuff for Unity")]
    [Tooltip("Rigidbody of the Object")]
    public Rigidbody2D rbody; //Rigidbody of the Object

    [Header("Controls the speed of the character")]
    [Tooltip("How fast it can go nyoom")]
    public int PlayerSpeed = 5;

    [Header("Combat Stuff")]
    [Tooltip("crosshair")]
    public GameObject crosshair;
    [Tooltip("This is the hit box thing")]
    public GameObject hitbox;
    [Tooltip("Air bullet")]
    public GameObject airBullet;
    [Tooltip("Air slash")]
    public GameObject airSlash;

    #endregion

    #region Private Varirables
    public bool CanMove = true;
    private List<GameObject> shotList;

    #endregion
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update(){
        if(CanMove){
            PlayerMovement();
        } else {
            //Stops movement if you cannot move
            rbody.velocity = new Vector2(0,0);
        }

        CheckAttack();
        if (Input.GetButtonDown("Fire1"))
        {
            weaponOnePrimaryFire();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            weaponOneSecondaryFire();
        }
    }

    private void LateUpdate()
    {
        updateCrosshair();
    }

    void updateCrosshair()
    {
        if (crosshair.transform.localPosition.magnitude >= 5)
        {
            crosshair.transform.localPosition = crosshair.transform.localPosition.normalized * 5;
            crosshair.GetComponent<Rigidbody2D>().velocity = transform.gameObject.GetComponent<Rigidbody2D>().velocity;
        }
        Vector2 crosshairVel = transform.gameObject.GetComponent<Rigidbody2D>().velocity + new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        crosshairVel.Normalize();
        /* (Physics2D.Raycast(transform.position, Vector3.up, 0.1f))
        {
            if(crosshairVel.y > 0)
            {
                crosshairVel.y = 0;
            }
        }
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            if (crosshairVel.y < 0)
            {
                crosshairVel.y = 0;
            }
        }
        if (Physics2D.Raycast(transform.position, Vector3.right, 0.1f))
        {
            if (crosshairVel.x > 0)
            {
                crosshairVel.x = 0;
            }
        }
        if (Physics2D.Raycast(transform.position, Vector3.left, 0.1f))
        {
            if (crosshairVel.x < 0)
            {
                crosshairVel.x = 0;
            }
        }*/
        crosshair.GetComponent<Rigidbody2D>().velocity = crosshairVel * 10;
    }
    void weaponOnePrimaryFire()
    {
        GameObject newShot = Instantiate(airSlash, transform.forward, Quaternion.identity);
        newShot.name = (string.Format("Shot [0])", shotList.Count));
        shotList.Add(newShot);
    }

    void weaponOneSecondaryFire()
    {
        GameObject newShot = Instantiate(airBullet, transform.forward, Quaternion.identity);
        newShot.name = (string.Format("Shot [0])", shotList.Count));
        shotList.Add(newShot);
    }

    /**
        Player movement allows the player to move
        Kinda straight forward I think
    **/
    private void PlayerMovement(){
        //First gets the movement vectors
        Vector2 MovementVector = 
        new Vector2((Input.GetAxis("Horizontal") * PlayerSpeed),
                    (Input.GetAxis("Vertical") * PlayerSpeed));

        //Sets velocity to the movement vector
        rbody.velocity = MovementVector;
    }

    /**
        Getters and Setters for Can Move
        Get CanMove checks for Can Move
        Set CanMove sets it to the desired value
    **/
    public bool GetCanMove(){
        return CanMove;
    }

    public void SetCanMove(bool val){
        CanMove = val;
    }

    public void CheckAttack(){
        Quaternion rotato = new Quaternion(0,0,0,0);
        Vector3 Offset = transform.position + new Vector3(1,0,0);
        if(Input.GetButtonDown("Use")){
            Object.Instantiate(hitbox, Offset, rotato);
        }
    }

}
