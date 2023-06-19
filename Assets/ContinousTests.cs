using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinousTests : MonoBehaviour
{
    
    [SerializeField] GameObject ContinousPanel;
    RandomSeedGenerator RandomSeedGeneratorScript;

   
    
    public bool testContinous = false;

    void Start() 
    {
        ContinousPanel.SetActive(false);
        if(testContinous == true)
        {
            DontDestroyOnLoad(this.gameObject);
            //Kiedyś będzie trzeba to poprawić, żeby ten obiekt przestawał istnieć, jeżeli już nie chcemy robić ciągłych testów.
            Scene scene = SceneManager.GetActiveScene();
            if(scene.name == "GeneratingRandomSeedScene")
            {
                GameObject SeedGenerator = GameObject.Find("RandomSeedGeneratorObject");
                SeedGenerator.GetComponent<RandomSeedGenerator>().GenerateSeed();
                GameObject SeedHolder = GameObject.Find("SeedHolder");
                SeedHolder.GetComponent<SeedHolderScript>().testSeed();
            }
        }
    }
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

    public void retryforContinuity()
    {
        SceneManager.LoadScene("GeneratingRandomSeedScene");

    }

}
