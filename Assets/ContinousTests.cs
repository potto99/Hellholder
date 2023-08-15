using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinousTests : MonoBehaviour
{
    
    [SerializeField] GameObject ContinousPanel;
    RandomSeedGenerator RandomSeedGeneratorScript;

   
    public float TestingTime;
    public bool testContinous = false;

    void Start() 
    {
        if(GameObject.Find("KeepTesting") && GameObject.Find("KeepTesting") != this.gameObject)
        {
            testContinous = true;
            TestingTime = GameObject.Find("KeepTesting").GetComponent<ContinousTests>().TestingTime + Time.deltaTime; 
            Destroy(GameObject.Find("KeepTesting"));
            DontDestroyOnLoad(this.gameObject);
            Scene scene = SceneManager.GetActiveScene();
            if(scene.name == "GeneratingRandomSeedScene")
            {
                GameObject SeedGenerator = GameObject.Find("RandomSeedGeneratorObject");
                SeedGenerator.GetComponent<RandomSeedGenerator>().GenerateSeed();
                GameObject SeedHolder = GameObject.Find("SeedHolder");
                SeedHolder.GetComponent<SeedHolderScript>().testSeed();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            ContinousPanel.SetActive(false);
        }

        // if(SceneManager.GetActiveScene().name != "GeneratingRandomSeedScene"){Destroy(this.gameObject);} 
    }

    //  void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     Debug.Log(scene.name);
    //     if(this.gameObject.name == "KeepTesting" && scene.name != "GeneratingRandomSeedScene")
    //     {Destroy(this.gameObject);}
    //     if(this.gameObject.name == "ObjectIndicatingContinousTests" && scene.name != "SeedEnteringScene")
    //     {Destroy(this.gameObject);}
    // }

    public void Continous()
    {
        if(testContinous == false)
        {
            testContinous = true;
            ContinousPanel.SetActive(true);
        }
        else if(testContinous == true)
        {
            testContinous = false;
            ContinousPanel.SetActive(false);
        }
    }

    public void SeedAccepted()
    {
        if(testContinous == true)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void RetryforContinuity()
    {
        gameObject.name = "KeepTesting";
        SceneManager.LoadScene("GeneratingRandomSeedScene");

    }

}
