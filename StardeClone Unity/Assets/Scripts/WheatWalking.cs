using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatWalking : MonoBehaviour
{
    public int lines;
    public float increment;
    static bool first = true;

    // Update is called once per frame
    void Update()
    {
        if(Player_Manager.player.transform.position.y > transform.position.y){
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        } else{
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }
}
