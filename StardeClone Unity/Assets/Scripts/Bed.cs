using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    private void OnMouseDown() {
        if(!Time_Manager.getInstance().pause){
            Time_Manager.getInstance().Sleep();
        }
    }
}
