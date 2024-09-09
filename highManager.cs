using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highManager : MonoBehaviour
{
    //this script exists to facilitate the GameManager working.
    //The High Manager is an object that contains the currentLevel variable and it doesn't get destroyed between scenes
    //As such, it can be used to tell the GameManager which level it is every time the gameplay scene gets loaded

    public int currentLevel;
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
