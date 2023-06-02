using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorScript : MonoBehaviour
{

    [SerializeField] GameObject beginningField;
    [SerializeField] GameObject field;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject rock;
    [SerializeField] GameObject spike;
    [SerializeField] GameObject key;
    [SerializeField] GameObject door;


    public string seed;
    public List<GameObject> levelObjects;
    public GameObject placingObject;
    [SerializeField] GameObject SeedHolder;
    
    SeedHolderScript SeedHolderScript;
    void Start()
    {
        SeedHolder = GameObject.Find("SeedHolder");
        SeedHolderScript = SeedHolder.GetComponent<SeedHolderScript>();
        Debug.Log(SeedHolderScript.seed);
        seed = SeedHolderScript.seed;

        char character;
        for (int i = 0; i < seed.Length; i ++)
        {
            character = seed[i];
            if(character.ToString() == "f")
            {
                placingObject = field;
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            }
            else if(character.ToString() == "w")
            {
                placingObject = wall;
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            }
            else if(character.ToString() == "r")
            {
                placingObject = rock;
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            }
            else if(character.ToString() == "s")
            {
                placingObject = spike;
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            }
            else if(character.ToString() == "b")
            {
                placingObject = beginningField;
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            }
            else if(character.ToString() == "b")
            {
                placingObject = door;
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            } 
            else if(character.ToString() == "k")
            {
                placingObject = key;
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            } 
            else 
            {
                Debug.Log("ERROR - NIEPOPRAWNY SYMBOL");
            }




        }




    }



    void addCoordinates(GameObject element, int ID)
    {
        Debug.Log(element);
        Debug.Log(ID);
        int x;
        int y;
        x = (ID%10)+1;
        y = (ID/10)+1;
        element.GetComponent<ElementCoordinates>().TableNumberX = x;
        element.GetComponent<ElementCoordinates>().TableNumberY = y;

        element.GetComponent<ElementCoordinates>().placeOnLevel();
    }

    
    
}
