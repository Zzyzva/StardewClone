using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = .5f;
    public static LevelLoader instance;


    private void Start() {
        if(SceneManager.GetActiveScene().name != "New Day"){
            Time_Manager.getInstance().Unpause();
        }
            
        if(instance){
            Destroy(this);
        } else{
            instance = this;
        }
    }

    

    public void LoadLevel(string newScene, Vector2 newPosition, bool outside){
        StartCoroutine(LoadLevelRoutine(newScene, newPosition, outside));
    }


    IEnumerator LoadLevelRoutine(string name, Vector2 newPosition, bool outside){
    Time_Manager.getInstance().ShortPause();
       transition.SetTrigger("Start");
       yield return new WaitForSeconds(transitionTime);
       
       Player_Manager.player.transform.position = newPosition;
            if(!outside){
                Time_Manager.getInstance().sun.enabled = false;
            } else{
               Time_Manager.getInstance().sun.enabled = true; 
            }
        SceneManager.LoadScene(name);
        if(name != "New Day"){
            Time_Manager.getInstance().HUD.alpha = 1f;
        }
        

   }
}
