using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameFish : MonoBehaviour

{
    Vector3 goal;
    Vector3 origin;
    public float width;
    public float height;
    public int speed;
    public bool hover = false;
    public Fish.FishType type;


    //Dash fish
    bool left = true;
    float pauseTime = .5f;
    float lastPause;
    bool pausing = false;



    // Start is called before the first frame update
    void Start()
    {
        origin = Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, 10));
        float x = 0;
        float y = 0;
        if(type == Fish.FishType.random){
            x = Random.Range(origin.x - width, origin.x + width);
            y = Random.Range(origin.y - height, origin.y + height);
        } else if(type == Fish.FishType.dash){
            x = Random.Range(origin.x - width / 2, origin.x - width);
            y = Random.Range(origin.y - height, origin.y + height);
        }
        
        goal = new Vector3(x,y,-2);
    }

    
    void FixedUpdate()
    {
        //Move the fish to a random spot
        if(type == Fish.FishType.random){
            if(Vector3.Magnitude(goal - transform.position) < .1  ){
                float x = Random.Range(origin.x - width, origin.x + width);
                float y = Random.Range(origin.y - height, origin.y + height);
                goal = new Vector3(x,y,-2);
            }
            Vector3 velocity = goal - gameObject.transform.position;
            transform.position += speed * Vector3.Normalize(velocity) * Time.deltaTime;


        //Dash across the field and pause
        } else if(type == Fish.FishType.dash){
            if(!pausing){
                if(Vector3.Magnitude(goal - transform.position) < .1  ){
                    float x;
                    float y;
                    if(left){
                        x = Random.Range(origin.x + width / 2, origin.x + width);
                        y = Random.Range(origin.y - height, origin.y + height);
                    } else{
                        x = Random.Range(origin.x - width / 2, origin.x - width);
                        y = Random.Range(origin.y - height, origin.y + height);
                    }
                    goal = new Vector3(x,y,-2);
                    left = !left;
                    pausing = true;
                    lastPause = Time.time;
                }
                Vector3 velocity = goal - gameObject.transform.position;
                transform.position += speed * Vector3.Normalize(velocity) * Time.deltaTime;
            } else{
                if(pauseTime + lastPause <= Time.time){
                    pausing = false;
                }
            }
            
        }
        
    }

    void Update() {
        //Checks for succesful reel
        if(Input.GetKeyDown("space")){
            if(hover)
            {
                Fishing_Manager.instance.Reel();
            }
            
        }
    }

    //Check if mouse is over object
    private void OnMouseEnter() {
        hover = true;
    }
    private void OnMouseExit() {
        hover = false;
    }
}
