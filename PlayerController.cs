using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    //Additional component that should be used with this script
    public CharacterController controller;

    //Speed control
    private float playerSpeed = 3;

    //TODO: Make speed a set value, rather than having the player accelerate

    void Start()
    {
        
    }

    void Update()
    {
        //look for input every frame
        playerEngine();
    }

    void playerEngine()
    {
        //Look for WASD and Arrow Key Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Limit movement to one axis at a time
        if(z != 0)
        {
            x = 0;
        }

        //Apply Movements
        Vector3 move = transform.right * x + transform.forward * z;

        //Take set speed into account
        controller.Move(move * playerSpeed * Time.deltaTime);

    }
}
