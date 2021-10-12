using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time_Manager : MonoBehaviour
{
    
    public Text timeText;
    public Text dateText;
    public Light sun;
    public List<Schedule> schedules;
    public List<Script> scripts;
    public CanvasGroup HUD;


    private int minutes;
    private int hours;
    private string meridiem;
    private string day;
    private int date;
    private string month;



    private float lastTick;

    public bool pause;
    private float pauseTime;
    private float unpauseTime;
    private bool justUnpaused;
    private static Time_Manager instance;
    void Awake()
    {
        if(instance == null){
            minutes = 0;
            hours = 7;
            meridiem = "am";
            day = "Mon";
            date = 1;
            month = "Spring";
            lastTick = Time.time;
            pause = false;
            instance = this;
            schedules = new List<Schedule>();
            scripts = new List<Script>();
        } else{
            Destroy(gameObject);
        }
        
    }
    public static Time_Manager getInstance(){
        if(instance == null){
            return new Time_Manager();
        }
        return instance;
    }

    // Update is called once per frame
    void Update()
    {
        //Update Time
        if(!pause){
            if(Time.time - lastTick > 1){
                minutes += (int) (5 * (Time.time - lastTick));
                lastTick = Time.time;
                 
                //Check valid time
                if(minutes > 59){
                    minutes -= 60;
                    hours++;
                    if(hours == 12){
                        if(meridiem.Equals("am")){
                            meridiem = "pm";
                        } else {
                            meridiem = "am";
                            date++;
                            changeDay();
                        }
                    }
                }
                if(hours > 12){
                    hours -= 12;
                }
                

                //Print Time
                if(minutes > 9){
                    timeText.text = hours + ":" + minutes + " " + meridiem;
                } else {
                    timeText.text = hours + ":0" + minutes + " " + meridiem;
                }

                dateText.text = day + " " + date;

                //Alert Schedules of time change
                foreach(Schedule s in schedules){
                    s.UpdateSchedule(hours, minutes, meridiem);
                }
                Fishing_Manager.instance.UpdateFishing();

                //Change sun brightness
                if(hours > 3 && hours < 8 && meridiem == "pm" ){
                    sun.intensity -= .01f;
                }
                if(hours > 3 && hours < 8 && meridiem == "am"){
                    sun.intensity += .01f;
                }
            }

            if(hours == 2 && minutes == 0 && meridiem == "am"){
                Sleep();
            }
        }    
    }

    //returns the time as one in in miltary format
    //12:30 pm would be 1230
    //2:45 pm would be 1445
    public int getTime(){
        int ret = 0;
        ret += minutes;
        ret += hours * 100;
        if(meridiem == "pm" && hours != 12){
            ret += 1200;
        }
        if(meridiem == "am" && hours == 12){
            ret -= 1200;
        }
        return ret;  
    }



    //Returns the time after a given interval
    //interval - the number of MINUTES to be add, less than a day
    //return - the time in miltary format

    public int getTimeAfterInterval(int interval){
        //Convert interval
        int tempMinutes = interval % 60;
        int tempHours = interval / 60;
        string tempMeridiem = meridiem;

        tempMinutes += minutes;
        tempHours += hours;

        //Check valid time
        if(tempMinutes > 59){
            tempMinutes -= 60;
            tempHours++;
        }
                
        //Check for meridiem
        if(tempHours >= 12 && hours < 12){
            if(tempMeridiem.Equals("am")){
                    tempMeridiem = "pm";
            } else {
                tempMeridiem = "am";
            }
        }
            
        //check hours
        if(tempHours > 12){
            tempHours -= 12;
        }

        //Convert time to military
        int ret = 0;
        ret += tempMinutes;
        ret += tempHours * 100;
        if(tempMeridiem == "pm" && tempHours != 12){
            ret += 1200;
        }
        if(tempMeridiem == "am" && tempHours == 12){
            ret -= 1200;
        }
        return ret;

    }



    //Advances the the day. Probably should have been an enum
    void changeDay(){
        if(day == "Mon"){
            day = "Tue";
        } else if(day == "Tue"){
            day = "Wed";
        } else if(day == "Wed"){
            day = "Thu";
        } else if(day == "Thu"){
            day = "Fri";
        } else if(day == "Fri"){
            day = "Sat";
        }else if(day == "Sat"){
            day = "Sun";
        } else if(day == "Sun"){
            day = "Mon";
        }
    }


    //Pauses the game
    public void Pause(){
        Time.timeScale = 0;
        pause = true;
        pauseTime = Time.time;
    }


        //Pauses the game without pausing time
        //Used for transitions
        public void ShortPause(){
        pause = true;
        pauseTime = Time.time;
    }


    //Unpasues the game
    public void Unpause(){
        Time.timeScale = 1;
        pause = false;
        unpauseTime = Time.time;
        lastTick += (unpauseTime - pauseTime);
    }

    //Loads the new day scene and hides UI
    public void Sleep(){
        LevelLoader.instance.LoadLevel("New Day", new Vector2(0,0), false);
        ShortPause();
        //Hide UI
        HUD.alpha = 0f;
        Player_Manager.player.gameObject.SetActive(false);


    }

    //Updates the time and NPC locations for a new day
    public void WakeUp(){
        Time_Manager.getInstance().Unpause();
        LevelLoader.instance.LoadLevel("Inn", new Vector2(0,0), false);
        

        //Chnage time
        if(!(hours < 3 && meridiem == "am")){
            changeDay();
            date++;
        }

        hours = 6;
        minutes = 55;
        meridiem = "am";
        //Print Time
                if(minutes > 9){
                    timeText.text = hours + ":" + minutes + " " + meridiem;
                } else {
                    timeText.text = hours + ":0" + minutes + " " + meridiem;
                }
                dateText.text = day + " " + date;

        

        //Move npc
        foreach(Schedule s in schedules){
            s.NewDay();
        }
        foreach(Script s in scripts){
            s.NewDay();
        }

        //Update Farming
        Farming_Manager.instance.NewDay();

        //Get gold
        foreach(Item item in Inventory_Manager.instance.toSell){
            Inventory_Manager.instance.gold += item.count * item.value;
        }
        Inventory_Manager.instance.toSell.Clear();
        GameObject.Find("Shipping Slot").GetComponent<Inventory_Slot>().ClearSlot();

        //Refill Energy
        Inventory_Manager.instance.energy = Inventory_Manager.instance.maxEnergy;

        //Reset Sun
        sun.intensity = .6f;

        Player_Manager.player.gameObject.SetActive(true);

        
    }

}
