using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //By Seth Ruiz
    //I am still learning how to code, so this will be sloppy.
    //But at least there are comments.

    //Bring in the controller component
    public CharacterController controller;

    //How fast do we want to go
    private float playerSpeed = 3;

    //To eliminate acceleration
    //We need to keep track of what the player was inputting last frame
    private float xLog = 0;
    private float zLog = 0;
    //And interpret the correct int to round to based on that and their current input
    private float xRounded = 0;
    private float zRounded = 0;

    void Start()
    {
        
    }

    void Update()
    {
        playerEngine();
    }

    void playerEngine()
    {
        //Look for WASD and Arrow Key Input. 
        //x and z will always be some decimal between -1 and 1
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //To get rid of acceleration we want to round to an int.
        //We have to use multiple rules so that we can interpret
        //player intent and keep the game feeling responsive

        //When moving right and speeding up
        if (x > 0 && x > xLog)
        {
            //Round up
            xRounded = Mathf.CeilToInt(x);
        }
        //Make the player stop immediately when they let go
        else if(x > 0 && x < xLog)
        {
            xRounded = 0;
        }

        //Now for the left
        else if (x < 0 && x < xLog)
        {
            xRounded = Mathf.FloorToInt(x);
        }
        else if (x < 0 && x > xLog)
        {
            xRounded = 0;
        }

        //And this keeps edge cases snappy
        else if (x == 0)
        {
            xRounded = 0;
        }

        //repeat above for vertical movement
        if (z > 0 && z > zLog)
        {
            zRounded = Mathf.CeilToInt(z);
        }
        else if (z > 0 && z < zLog)
        {
            zRounded = 0;
        }
        else if (z < 0 && z < zLog)
        {
            zRounded = Mathf.FloorToInt(z);
        }
        else if (z < 0 && z > zLog)
        {
            zRounded = 0;
        }
        else if (z == 0)
        {
            zRounded = 0;
        }

        //Limit movement to one axis at a time
        //Unfortunately this creates a situation where vertical input takes
        //priority over horizontal movement. Maybe this can be fixed in the future.
        while (z != 0)
        {
            x = 0;
        }

        //Create a vector3 (0, 0, 0) called "move"
        //transform.right is the first part of the vector3; it is modified by the player's input
        //transform.forward is the last part of the vector3; it is also modified by the player's input
        Vector3 move = transform.right * xRounded + transform.forward * zRounded;

        //Tell the controller to move the player based on their input modified by our set speed
        controller.Move(move * playerSpeed * Time.deltaTime);

        //write down what x and z were for this frame
        xLog = x;
        zLog = z;

        Debug.Log(xRounded);
    }
}