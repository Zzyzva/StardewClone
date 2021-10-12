using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDayButton : MonoBehaviour
{
    public void ButtonPress(){
        Time_Manager.getInstance().WakeUp();
    }

}
