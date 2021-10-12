using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : MonoBehaviour
{
    public string newScene;
    public Vector3 newPosition;
    public bool outside;
    

   void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "Player"){
            LevelLoader.instance.LoadLevel(newScene, newPosition, outside);
        
            
       }
   }

   

}
