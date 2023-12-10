using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomSeedGenerator : MonoBehaviour
{
    char[] seed = new char[] {'f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f','f'};
    [SerializeField] TMP_Text seedText;

    public GameObject SeedHolder;
    public SeedHolderScript SeedHolderScript;
    public void GenerateSeed()
    {
        int randomPlayerPlacement = 0;
        int randomDoorPlacement = 0;
        for (int i=0; i<seed.Length; i++) //Losowo stawiamy puste pola, kamienie, ściany, spike'i różnych typów i przeciwników. Wszystkie z tym samym prawdopodobieństwiem
        {
            int randomElement = Random.Range(1, 8);
            if(randomElement == 1){seed[i] = 'f';}
            else if(randomElement == 2){seed[i] = 'w';}
            else if(randomElement == 3){seed[i] = 'r';}
            else if(randomElement == 4){seed[i] = 'e';}
            else if(randomElement == 5){seed[i] = 'a';}
            else if(randomElement == 6){seed[i] = 's';}
            else if(randomElement == 7){seed[i] = 'c';}
        }

        int setKey = Random.Range(1,3); //50% szans, że poziom będzie posiadał klucz 
        if(setKey == 2)
        {
            int randomKeyPLacement = Random.Range(0, 100);
            seed[randomKeyPLacement] = 'k';
        }

        do
        { 
            randomPlayerPlacement = Random.Range(0, 100);
            randomDoorPlacement = Random.Range(0, 100);
        }while (randomPlayerPlacement == randomDoorPlacement);

        seed[randomPlayerPlacement] = 'b';
        seed[randomDoorPlacement] = 'd';


        string seedString = new string(seed);
        seedText.text = seedString;

        SeedHolder = GameObject.Find("SeedHolder");
        SeedHolderScript = SeedHolder.GetComponent<SeedHolderScript>();
        SeedHolderScript.seed = seedString;
        // SeedHolderScript.ownsSeed = true;


    }
}
