using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapCutsceneManager : MonoBehaviour
{
    //the levelCoordinates array has a list of vectors that represent the different regions of the map. The player travels between them after each level.
    public GameObject highManager;
    public GameObject worldMap;
    public int level;
    public Vector3[] levelCoordinates;
    public Sprite[] mapSprites;
    private bool blinking = false;

    // Start is called before the first frame update
    void Start()
    {
        highManager = GameObject.Find("HighManager");
        level = highManager.GetComponent<highManager>().currentLevel;
        transform.position = levelCoordinates[level];
        worldMap.GetComponent<SpriteRenderer>().sprite = mapSprites[level];
    }

    // Update is called once per frame
    void Update()
    {
        if(blinking == false)
        {
            if (transform.position != levelCoordinates[level+1])
            {
                transform.position = Vector3.MoveTowards(transform.position, levelCoordinates[level+1], 1.5f * Time.deltaTime);
            }
            else
            {
                blinking = true;
                StartCoroutine(Blink());
            }
        }
    }

    IEnumerator Blink()
    {
        //the player turns visible and invisible a few times before the next level starts
        yield return new WaitForSecondsRealtime(.5f);
        for (int b = 0; b < 4; b++)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSecondsRealtime(.5f);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSecondsRealtime(.5f);
        }
        StopCoroutine(Blink());
        if(level>10)
        {
            SceneManager.LoadScene("EndingScene", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
    }

}
