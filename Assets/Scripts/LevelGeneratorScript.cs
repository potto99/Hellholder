using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject beginningField;
    [SerializeField] GameObject field;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject rock;
    [SerializeField] GameObject spike;
    [SerializeField] GameObject enemy;
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
            if(character.ToString() == "f") //field
            {
                placingObject = Instantiate(field);
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            }
            else if(character.ToString() == "w") //wall
            {
                placingObject = Instantiate(wall);
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            }
            else if(character.ToString() == "r") //rock
            {
                placingObject = Instantiate(field); //field on which the rock will be standing
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTypeInterface>().isTakenByRock = true;

                placingObject = Instantiate(rock); 
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            }
            else if(character.ToString() == "a") //active spike
            {
                placingObject = Instantiate(spike);
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTypeInterface>().ActivateSpike();

            }
            else if(character.ToString() == "s") //spike
            {
                placingObject = Instantiate(spike);
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            }
            else if(character.ToString() == "b") // beginningField
            {
                placingObject = Instantiate(beginningField);
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);

                player = Instantiate(player);
                addCoordinates(placingObject, i);
            }
            else if(character.ToString() == "d") //door
            {
                placingObject = Instantiate(door);
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            } 
            else if(character.ToString() == "k") //key
            {
                placingObject = Instantiate(field); //field on which the key will be standing
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTypeInterface>().isHoldingKey = true;
                
                placingObject = Instantiate(key);
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            } 
            else 
            {
                Debug.Log("ERROR - NIEPOPRAWNY SYMBOL");
            }
        }

        for (int i = seed.Length; i < 100; i ++) // We are placing walls on top of level
        {
            placingObject = Instantiate(wall);
            levelObjects.Add(placingObject);
            addCoordinates(placingObject, i);
        }

        

        
        createWallFrame();




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

    void createWallFrame()
    {
        int borderX = 0;
        int borderY = 0;
        do
        {
            do
            {
                if(borderX == 0 || borderX == 11 || borderY == 0 || borderY == 11)
                {
                    placingObject = Instantiate(wall);
                    levelObjects.Add(placingObject);
                    placingObject.GetComponent<ElementCoordinates>().TableNumberX = borderX;
                    placingObject.GetComponent<ElementCoordinates>().TableNumberY = borderY;
                    placingObject.GetComponent<ElementCoordinates>().placeOnLevel();
                }
                borderY++;
            }while(borderY <= 11);
            borderX++;
            borderY = 0;
        }while(borderX <= 11);
    }

    
    
}
