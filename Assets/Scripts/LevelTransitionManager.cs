using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class LevelTransitionManager : object
{
    public static void LoadLvl(string thisScene, string loadScene, GameObject thisEntrance)
    {
        string thisEntranceName = thisEntrance.name;
        AsyncOperation async = SceneManager.LoadSceneAsync(@"Assets/Scenes/" + loadScene);
        async.completed += AsyncOperation => OnSceneLoaded(thisEntranceName);

        //PLEASE FIX IT FOR ME IDK WHY IT DONT WORK, SEND ME PULL REQUEST WITH FIX
        //playr.transform.position = nextEntrance.gameObject.transform.position;
        /*if (thisEntrance.GetComponent<LevelEntrance>().direction == IntoLevelDir.JumpLeft)
        {
            
        }*/
    }
    //all manipulations with loaded scene should be here
    public static void OnSceneLoaded(string thisEntranceName)
    {
        GameObject nextEntrance = GameObject.Find(thisEntranceName);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(nextEntrance.transform.position.x,
            nextEntrance.transform.position.y
            , GameObject.FindObjectOfType<SpriteRenderer>().transform.position.z);
    }
}
