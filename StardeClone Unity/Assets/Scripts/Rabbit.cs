using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{

    public Item pelt;
    public Item meat;

    public GameObject rabbitHole;
    GameObject rabbitHoleInstance;

    double lastMove;

    float minMoveTime = .2f;
    float maxMoveTime = 1;


    float minRestTime = 1;
    float maxRestTime = 3;

    float moveTime;

    float walkSpeed = 5;
    float runSpeed = 8;

    float fleeDistance = 6;


    bool moving = false;
    bool fleeing = false;





    //Creates the rabbit hole and sets up movement variables
    private void Start() {
        rabbitHoleInstance = Instantiate(rabbitHole);
        rabbitHoleInstance.transform.position = gameObject.transform.position;
        lastMove = Time.time;
        moveTime = Random.Range(minMoveTime, maxMoveTime);
    }

    //Checks for elapsed time
    //If moving, stops and sets new time
    //If not moving, picks a random direction to move
    private void Update() {
        //Check if it needs to flee
        if(( Player_Manager.player.transform.position - gameObject.transform.position ).magnitude < fleeDistance){
            fleeing = true;
        } 
        if(fleeing){
            Vector2 velocity = rabbitHoleInstance.transform.position - gameObject.transform.position;
            velocity.Normalize();
            velocity *= runSpeed;
            gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
        }

        if(lastMove + moveTime < Time.time){
            lastMove = Time.time;
            if(moving){
                
                moving = false;
                moveTime = Random.Range(minRestTime, maxRestTime);
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            } else{
                moving = true;
                moveTime = Random.Range(minMoveTime, maxMoveTime);
                int emergency = 0;
                while(emergency < 1000){
                    emergency++;
                    float random = Random.Range(0f, 360f);
                    Vector2 velocity = new Vector2(Mathf.Cos(random), Mathf.Sin(random));
                    velocity.Normalize();
                    velocity *= walkSpeed;
                    RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, velocity, 6, LayerMask.GetMask("Default"));
                    if(hit.collider == null){
                        gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
                        break;
                    }
                }
            }
        }  
    }


    //If hit by arrow, die
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Weapon"){
            Inventory_Manager.instance.AddItem(pelt, 1);
            Inventory_Manager.instance.AddItem(meat, 1);
            Destroy(gameObject);
        } else if(other.gameObject == rabbitHoleInstance && fleeing){
            Destroy(gameObject);
        }
    }
}
