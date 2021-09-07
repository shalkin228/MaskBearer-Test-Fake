using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitch : MonoBehaviour
{
    [SerializeField] string SceneToLoad = "Select Room";
    [SerializeField] int exitType = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public enum EnterDir
{
    Up, Down, Left, Right
}