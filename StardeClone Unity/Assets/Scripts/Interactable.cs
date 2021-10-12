using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    public float interactDistance = 5;

    // Start is called before the first frame update
    private void OnMouseDown() {
        float distance = Vector3.Magnitude(Player_Manager.player.transform.position - gameObject.transform.position);
        if(!Time_Manager.getInstance().pause &&  distance < interactDistance){
            Interact();
        }
    }

    public abstract void Interact();
}
