using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * this class handles all the input the game needs.
 * it's basically a wrapper for input. 
 */
public class InputManager : MonoBehaviour
{
    /* 
     * functions that return values for the wasd buttons
     */
    public bool Up() { return Input.GetKey(KeyCode.W); }
    public bool Down() { return Input.GetKey(KeyCode.S); }
    public bool Left() { return Input.GetKey(KeyCode.A); }
    public bool Right() { return Input.GetKey(KeyCode.D); }

    /* 
        * functions that return values for the mouse position
        */
    public float GetXRot() { return Input.GetAxis(Strings.Movement.MOUSE_X); }
    public float GetYRot() { return -Input.GetAxis(Strings.Movement.MOUSE_Y); }

    /*
        * other functions
        */
    public bool Crouch() { return Input.GetKey("left ctrl"); }
    public bool Shoot() { return Input.GetMouseButton(0); }
    public bool ShootSemi() { return Input.GetMouseButtonDown(0); }
    public bool Reload() { return Input.GetKeyDown(KeyCode.R); }
    public bool Aim() { return Input.GetMouseButton(1); }
    public bool Run() { return Input.GetKey(KeyCode.LeftShift); }
}
