using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelEntrance : MonoBehaviour
{
    //private Scene currentScene = SceneManager.GetActiveScene();
    [SerializeField] private string SceneToLoad = @"Scene name in /Scenes directory";
    [SerializeField] private string TransitionName = "Change me please";
    [SerializeField] IntoLevelDir direction;
    [SerializeField] GameObject StartPos;
    private void OnTriggerEnter2D(Collider2D collidoder)
    {
        SceneManager.LoadScene(SceneManager.GetSceneByPath(@"/Scenes"+SceneToLoad).buildIndex);
    }

}
enum IntoLevelDir
{
    Fall,
    Jump,
    ToRight,
    ToLeft
}
