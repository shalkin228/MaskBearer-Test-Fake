using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelEntrance : MonoBehaviour
{
    //private Scene currentScene = SceneManager.GetActiveScene();
    [SerializeField] private string SceneToLoad = @"Scene name in /Scenes directory";
    [SerializeField] public IntoLevelDir direction;
    public Scene curr;
    [SerializeField] GameObject StartPos;
    private void OnTriggerEnter2D(Collider2D collidoder)
    {
        curr = SceneManager.GetActiveScene();
        if (collidoder.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            LevelTransitionManager.LoadLvl(SceneManager.GetActiveScene().path,SceneToLoad, this.gameObject);
        }
    }

}
public enum IntoLevelDir
{
    Fall,
    JumpLeft,
    JumpRight,
    ToRight,
    ToLeft
}
