using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SeedHolderScript : MonoBehaviour
{
    [SerializeField] public TMP_InputField SeedStringInput;
    public string seed;
    public bool ownsSeed = false;

    void Start()
    {
        GameObject preExistingSeedHolder = GameObject.Find("SeedHolderDontDestroy");
        

        if(preExistingSeedHolder != null)
        {
            SeedHolderScript SeedHolderToExtractSeedFrom = preExistingSeedHolder.GetComponent<SeedHolderScript>();
            seed = SeedHolderToExtractSeedFrom.seed;    
            Destroy(preExistingSeedHolder);
            SeedStringInput.transform.Find("Text Area").transform.Find("Placeholder").GetComponent<TMP_Text>().text = seed;
            ownsSeed = true;
        }
    }

    public void acceptSeed()
    {
        bool seedCorrect = true;
        if(seed.Length <= 100)
        {
            if(ownsSeed == false){seed = SeedStringInput.text;}
            char character;
            for (int i = 0; i < seed.Length; i ++)
            {
                character = seed[i];
                if(character.ToString() == "f" || character.ToString() == "w" || character.ToString() == "r" || character.ToString() == "a" || character.ToString() == "s" || character.ToString() == "c" || character.ToString() == "e" || character.ToString() == "b" || character.ToString() == "k" || character.ToString() == "d"){}
                else
                {
                    seedCorrect = false;
                    Debug.Log("Error on char " + i);
                }
            }
        }
        else
        {
            Debug.Log("Maksymalny rozmiar Seedu to 100 znaków");
        }

        if(seedCorrect == true)
        {
            DontDestroyOnLoad(this.gameObject);
            this.gameObject.name = "SeedHolderDontDestroy";
            SceneManager.LoadScene("GeneratedLevelScene");
        }


    }

    public void testSeed()
    {
        bool seedCorrect = true;
        if(seed.Length <= 100)
        {
            if(ownsSeed == false){seed = SeedStringInput.text;}
            char character;
            for (int i = 0; i < seed.Length; i ++)
            {
                character = seed[i];
                if(character.ToString() == "f" || character.ToString() == "w" || character.ToString() == "r" || character.ToString() == "a" || character.ToString() == "s" || character.ToString() == "c" || character.ToString() == "e" || character.ToString() == "b" || character.ToString() == "k" || character.ToString() == "d"){}
                else
                {
                    seedCorrect = false;
                    Debug.Log("Error on char " + i);
                }
            }
        }
        else
        {
            Debug.Log("Maksymalny rozmiar Seedu to 100 znaków");
        }

        if(seedCorrect == true)
        {
            DontDestroyOnLoad(this.gameObject);
            this.gameObject.name = "SeedHolderDontDestroy";
            SceneManager.LoadScene("SeedTestingScene");
        }


    }




}
