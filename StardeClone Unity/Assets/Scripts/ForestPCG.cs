using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestPCG : MonoBehaviour
{

    string[,] map;

    public GameObject tree;
    public GameObject berryBush;
    public GameObject rabbitHole;



    int minTrees = 8;
    int maxTrees = 14;

    int minBush = 4;
    int maxBush = 8;

    int minRabbits = 1;
    int maxRabbits = 3;

    int rabbitWarrenChance = 10;

    int minWarrenRabbits = 8;
    int maxWarrebRabbits = 12;



    void Start()
    {
        map = new string[80,80];

        int emergency = 0;

        int x = 0;
        int y = 0;


        //Generate trees
        int numTrees = Random.Range(minTrees, maxTrees + 1);
        for(int i = 0; i < numTrees; i++){
            while(emergency < 1000){
                emergency++;
                x = Random.Range(4, 76);
                y = Random.Range(4, 76);
                if(map[x,y] != "Null"){
                    map[x,y] = "tree";
                    map[x + 1, y - 1] = "N";
                    map[x + 1, y] = "N";
                    map[x + 1, y + 1] = "N";
                    map[x, y - 1] = "N";
                    map[x, y + 1] = "N";
                    map[x - 1, y - 1] = "N";
                    map[x - 1, y] = "N";
                    map[x - 1, y + 1] = "N";
                    break;
                }
            }     
        }

        //Generate Berries
        int numBushes = Random.Range(minBush, maxBush + 1);
        x = Random.Range(8, 72);
        y = Random.Range(8, 72);
        int tempx = x;
        int tempy = y;
        int direction = Random.Range(0,4);

        for(int i = 0; i < numBushes; i++){
            while(emergency < 1000){
                emergency++;
                
                //Move the 'brush' over small amount to make patch of berries
                direction += Random.Range(1,4);
                direction %= 4;
                int jump = Random.Range(1,3);
                if(direction == 0){
                    x += jump;
                } else if(direction == 1){
                    x -= jump;
                } else if(direction == 2){
                    y += jump;
                } else{
                    y -= jump;
                }
                if( x > 78 || x < 2 ){
                    x = tempx;
                }
                if( y > 78 || y < 2 ){
                    y = tempy;
                }
                if(map[x,y] != "Null"){
                    map[x,y] = "bush";
                    break;
                }
            }
        }

        //Generate Rabbits
        int warrenRand = Random.Range(0, 100);
        int numRabbits;
        if(warrenRand < rabbitWarrenChance){
            numRabbits = Random.Range(minWarrenRabbits, maxWarrebRabbits + 1);
        } else {
            numRabbits = Random.Range(minRabbits, maxRabbits + 1);
        }
        for(int i = 0; i < numRabbits; i++){
            while(emergency < 1000){
                emergency++;
                x = Random.Range(4, 76);
                y = Random.Range(4, 76);
                if(map[x,y] != "Null"){
                    map[x,y] = "rabbit";
                    map[x + 1, y - 1] = "N";
                    map[x + 1, y] = "N";
                    map[x + 1, y + 1] = "N";
                    map[x, y - 1] = "N";
                    map[x, y + 1] = "N";
                    map[x - 1, y - 1] = "N";
                    map[x - 1, y] = "N";
                    map[x - 1, y + 1] = "N";
                    break;
                }
            }     
        }

        




        //Load Objects
        for(x = 0; x < 79; x++){
            for(y = 0; y < 79; y++){
                string pos = map[x,y];
                GameObject temp = null;
                if(pos == "tree"){
                    temp = Instantiate(tree);
                } else if(pos == "bush"){
                    temp = Instantiate(berryBush);
                } else if(pos == "rabbit"){
                    temp = Instantiate(rabbitHole);
                }

                
                if(temp){
                    temp.transform.position = new Vector3(x, y, -1);
                }
                
            }
        }

    }


}
