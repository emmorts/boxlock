//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;

public class MainCharacterMovementController
{
    private GameObject gameObject;
    private float xSpeed = 0;
    private float ySpeed = 0;

    public MainCharacterMovementController(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }

    public void Update(float speed, float timeUntilMaxSpeed)
    {
        timeUntilMaxSpeed *= Time.deltaTime;
        speed *= Time.deltaTime;

        bool up = false;
        bool right = false;
        bool down = false;
        bool left = false;

        if (Input.GetKey(KeyCode.W)) { up = true; }
        if (Input.GetKey(KeyCode.S)) { down = true; }
        if (Input.GetKey(KeyCode.A)) { left = true; }
        if (Input.GetKey(KeyCode.D)) { right = true; }

        float velChange = (1f / timeUntilMaxSpeed) * Time.deltaTime;

        if (up)
        {
            if (ySpeed < 0) ySpeed = 0;
            ySpeed += velChange;
        }
        if (down)
        {
            if (ySpeed > 0) ySpeed = 0;
            ySpeed -= velChange;
        }
        if (left)
        {
            if (xSpeed > 0) xSpeed = 0;
            xSpeed -= velChange;
        }
        if (right)
        {
            if (xSpeed < 0) xSpeed = 0;
            xSpeed += velChange;
        }

        if (!up && !down)
        {
            ySpeed = 0f;
        }
        if (!left && !right)
        {
            xSpeed = 0f;
        }

        if (ySpeed < -1) ySpeed = -1;
        if (ySpeed > 1) ySpeed = 1;
        if (xSpeed < -1) xSpeed = -1;
        if (xSpeed > 1) xSpeed = 1;

        Vector3 dirVector = new Vector3(xSpeed, 0, ySpeed);

        gameObject.transform.Translate(dirVector * speed, Space.World);
    }
}
