using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class SeedHolderScript : MonoBehaviour
{
    [SerializeField] public TMP_InputField SeedStringInput;
    
    [SerializeField] public TMP_InputField MinMovesAllowedInput;
    [SerializeField] public TMP_InputField MaxMovesAllowedInput;
    public string seed;
    public int MinMovesAllowed;
    public int MaxMovesAllowed;
    // public bool ownsSeed = false;

    void Start()
    {
        GameObject preExistingSeedHolder = GameObject.Find("SeedHolderDontDestroy");
        

        if(preExistingSeedHolder != null)
        {
            SeedHolderScript SeedHolderToExtractSeedFrom = preExistingSeedHolder.GetComponent<SeedHolderScript>();
            seed = SeedHolderToExtractSeedFrom.seed;
            MinMovesAllowed = SeedHolderToExtractSeedFrom.MinMovesAllowed;
            MaxMovesAllowed = SeedHolderToExtractSeedFrom.MaxMovesAllowed;  
            Destroy(preExistingSeedHolder);
            if(SeedStringInput != null)
            {
                SeedStringInput.transform.Find("Text Area").transform.Find("Placeholder").GetComponent<TMP_Text>().text = seed;
            }
            if(MinMovesAllowedInput != null)
            {
                MinMovesAllowedInput.transform.Find("Text Area").transform.Find("Placeholder").GetComponent<TMP_Text>().text = MinMovesAllowed.ToString();
            }
            if(MaxMovesAllowedInput != null)
            {
                MaxMovesAllowedInput.transform.Find("Text Area").transform.Find("Placeholder").GetComponent<TMP_Text>().text = MaxMovesAllowed.ToString();
            }
            
           
            // ownsSeed = true;
        }
    }

    public void acceptSeed()
    {
        bool seedCorrect = true;
        if(SeedStringInput.text.Length != 0){seed = SeedStringInput.text;}
        if(seed.Length <= 100)
        {
            char character;
            for (int i = 0; i < seed.Length; i ++)
            {
                character = seed[i];
                if(character.ToString() == "f" || character.ToString() == "w" || character.ToString() == "r" || character.ToString() == "a" || character.ToString() == "o" || character.ToString() == "s" || character.ToString() == "m" || character.ToString() == "c" || character.ToString() == "n"|| character.ToString() == "e" || character.ToString() == "b" || character.ToString() == "k" || character.ToString() == "d" || character.ToString() == "g"){}
                else
                {
                    seedCorrect = false;
                    Debug.Log("Error on char " + i);
                }
            }

            if(String.IsNullOrEmpty(MinMovesAllowedInput.text) == false)
            {
                try
                {
                    MinMovesAllowed = Int32.Parse(MinMovesAllowedInput.text.ToString());
                }
                catch (FormatException)
                {
                    Debug.Log("Minimalna liczba ruchów musi być liczbą");
                    seedCorrect = false;
                }
                
            }
 

            if(String.IsNullOrEmpty(MaxMovesAllowedInput.text) == false)
            {
                try
                {
                    MaxMovesAllowed = Int32.Parse(MaxMovesAllowedInput.text.ToString());
                }
                catch (FormatException)
                {
                    Debug.Log("Maksymalna liczba ruchów musi być liczbą");
                    seedCorrect = false;
                }
                
            }
            else
            {
                if(MaxMovesAllowed == 0)
                {
                    MaxMovesAllowed = 10;
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
        if(SeedStringInput != null){if(SeedStringInput.text.Length != 0){seed = SeedStringInput.text;}}
        if(seed.Length <= 100)
        {
            char character;
            for (int i = 0; i < seed.Length; i ++)
            {
                character = seed[i];
                if(character.ToString() == "f" || character.ToString() == "w" || character.ToString() == "r" || character.ToString() == "a" || character.ToString() == "o" || character.ToString() == "s" || character.ToString() == "m" || character.ToString() == "c" || character.ToString() == "n"|| character.ToString() == "e" || character.ToString() == "b" || character.ToString() == "k" || character.ToString() == "d" || character.ToString() == "g"){}
                else
                {
                    seedCorrect = false;
                    Debug.Log("Error on char " + i);
                }
            }

            if(String.IsNullOrEmpty(MinMovesAllowedInput.text) == false)
            {
                try
                {
                    MinMovesAllowed = Int32.Parse(MinMovesAllowedInput.text.ToString());
                }
                catch (FormatException)
                {
                    Debug.Log("Minimalna liczba ruchów musi być liczbą");
                    seedCorrect = false;
                }
            }
       
            if(String.IsNullOrEmpty(MaxMovesAllowedInput.text) == false)
            {
                try
                {
                    MaxMovesAllowed = Int32.Parse(MaxMovesAllowedInput.text.ToString());
                }
                catch (FormatException)
                {
                    Debug.Log("Maksymalna liczba ruchów musi być liczbą");
                    seedCorrect = false;
                }
            }
            else
            {
                if(MaxMovesAllowed == 0)
                {
                    MaxMovesAllowed = 10;
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
