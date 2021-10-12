using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad instance;
    void Awake()
    {
        if(instance == null){
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        } else{
            foreach (Transform child in gameObject.transform) {
                GameObject.Destroy(child.gameObject);
            }
            Destroy(gameObject);
        }
        
    }

}
