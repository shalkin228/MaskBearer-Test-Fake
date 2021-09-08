using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class LevelTransitionManager : object
{
    public static void LoadLvl (string thisScene, string loadScene, GameObject thisEntrance)
    {
        SceneManager.LoadScene(@"Assets/Scenes/" + loadScene);
        SceneManager.SetActiveScene(SceneManager.GetSceneByPath(@"Assets/Scenes/" + loadScene));
        SceneManager.UnloadScene(@"Assets/Scenes" + thisEntrance.GetComponent<LevelEntrance>().curr);
        GameObject nextEntrance = GameObject.Find(thisEntrance.name);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = nextEntrance.transform.position; //PLEASE FIX IT FOR ME IDK WHY IT DONT WORK, SEND ME PULL REQUEST WITH FIX
        //playr.transform.position = nextEntrance.gameObject.transform.position;
        /*if (thisEntrance.GetComponent<LevelEntrance>().direction == IntoLevelDir.JumpLeft)
        {
            
        }*/
    }
}
