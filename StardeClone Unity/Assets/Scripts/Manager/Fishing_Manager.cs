using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fishing_Manager : MonoBehaviour
{
    public bool fishing = false;
    public bool canCatch = false;
    public bool minigameActive = false;

    int fishTime = 0;
    int fishGoal = 8; //Time to hook fish
    int catchTime = 0;
    int catchGoal = 2; //Time while fish is hooked
    public static Fishing_Manager instance;

    public GameObject hookSymbol;
    GameObject hookSymbolObject;

    public GameObject minigame;
    GameObject minigameObject;

    public float minigameGoal = 100;
    public float minigameDownTick = 2;
    float minigameScore = 20;
    Transform fishingBar;

    float energyCost = 0;



    public Fish[] fishTable;
    Fish hookedFish;

    void Awake()
    {
        if(instance){
            Destroy(this);
        } else{
            instance = this;
        }
    }


    //Counts time until fish bite and time while fish on line
    public void UpdateFishing()
    {
        if(fishing){
            //Increment by random amount
            int rand = Random.Range(0,5);
            fishTime += rand;
            //If we have reached goal
            if(fishTime >= fishGoal){
                fishing = false;
                canCatch = true;
                hookSymbolObject = Instantiate(hookSymbol);
                Vector3 symbolPos = Player_Manager.player.transform.position;
                symbolPos.y += 1;
                hookSymbolObject.transform.position = symbolPos;
                
            }
        } else if(canCatch){
            catchTime++;
            if(catchTime >= catchGoal){
                canCatch = false;
                fishing = true;
                Destroy(hookSymbolObject);
                catchTime = 0;
                fishTime = 0;
            }
        }
    }

    //Called when the player hooks a fish
    public void AttemptCatch(float energyCost, Water.Type waterType){
        if(canCatch){
            this.energyCost = energyCost;
            minigameActive = true;
            minigameObject = Instantiate(minigame);
            minigameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, 10));
            fishingBar = minigameObject.transform.GetChild(0).GetChild(1);


            //Pick fish
            int total = 0;
            foreach(Fish temp in fishTable){
                if(temp.waterTypes.Contains(waterType)){
                    total += temp.probability;
                }  
            }
            int rand = Random.Range(0, total);
            foreach(Fish temp in fishTable){
                if(temp.waterTypes.Contains(waterType)){
                    if(rand < temp.probability){
                        hookedFish = temp;
                        break;
                    } else{
                        rand -= temp.probability;
                    }
                }
            }


            //Set fish stats
            MinigameFish fish = minigameObject.transform.GetChild(1).gameObject.GetComponent<MinigameFish>();
            fish.type = hookedFish.type;
            fish.speed = hookedFish.speed;
            minigameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = hookedFish.icon;
            minigameObject.transform.GetChild(1).localScale = Vector3.one * hookedFish.size;
            



            
        } else{
            Player_Manager.player.GetComponent<Player_Movement>().canMove = true;
        }
        catchTime = 0;
        fishTime = 0;
        fishing = false;
        canCatch = false;
        if(hookSymbolObject){
            Destroy(hookSymbolObject);
        }
        
    }

    //Called when the player succesfully catches a fish
    public void EndMinigame(bool success){
        Inventory_Manager.instance.energy -= energyCost;
        minigameActive = false;
        minigameScore = 20;
        if(success){
            Inventory_Manager.instance.AddItem(hookedFish, 1);
        }
        
        if(minigameObject){
            Destroy(minigameObject);
        }
        Player_Manager.CanMove(true);
       
    }


    public void Reel(){
        minigameScore += 10;
        
        if(minigameScore >= minigameGoal){
            EndMinigame(true);
            
        }

    }

    void Update(){
        if(minigameActive){
            minigameScore -= minigameDownTick * Time.deltaTime;
            if(minigameScore < 0){
                EndMinigame(false);
            }
            fishingBar.localScale = new Vector3(minigameScore / minigameGoal, 1, 1);
        }
    }
}
