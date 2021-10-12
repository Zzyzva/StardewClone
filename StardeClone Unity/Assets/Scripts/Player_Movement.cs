using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public int speed;
    public bool canMove = true;

    void Update()
    {
        if(!Time_Manager.getInstance().pause && canMove){
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(inputX, inputY);
            movement.Normalize();
            movement *= speed;


            GetComponent<Rigidbody2D>().velocity = movement;
        } else{
            Vector2 movement = new Vector3(0, 0);

            GetComponent<Rigidbody2D>().velocity = movement;
        }
    }
}
