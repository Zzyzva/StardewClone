using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{

    public static GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        if(player){
            Destroy(gameObject);
        }else{
            player = gameObject;
        }

    }


    public static void CanMove(bool canMove){
        player.GetComponent<Player_Movement>().canMove = canMove;
        player.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
    }

}
