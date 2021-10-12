using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float maxDistanxce;
    public Vector3 origin;

    void Update()
    {

        if(Mathf.Abs(Vector3.Magnitude(origin - transform.position)) > maxDistanxce){
            Destroy(gameObject);
        }
    }
}
