using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTestGeneratorScript : MonoBehaviour
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
    public List<GameObject> fields;
    public List<GameObject> players;
    public GameObject placingObject;
    [SerializeField] GameObject SeedHolder;

    public int keys = 0;
    public int levelObjectCounter = 0;
    
    SeedHolderScript SeedHolderScript;


    void Start()
    {
        fields.Clear();
        levelObjects.Clear();
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
                fields.Add(field);
                addCoordinates(fields[i], i);
            }
            else if(character.ToString() == "w") //wall
            {
                fields.Add(wall);
                addCoordinates(fields[i], i);
            }
            else if(character.ToString() == "r") //rock
            {
                fields.Add(field);
                addCoordinates(fields[i], i);
                fields[i].GetComponent<ElementTestTypeInterface>().isTakenByRock = true;

                levelObjects.Add(rock);
                addCoordinates(levelObjects[levelObjectCounter], i);
                levelObjectCounter++;
            }
            else if(character.ToString() == "e") //enemy
            {
                fields.Add(field);
                addCoordinates(fields[i], i);
                fields[i].GetComponent<ElementTestTypeInterface>().isTakenByEnemy = true;

                levelObjects.Add(enemy);
                addCoordinates(levelObjects[levelObjectCounter], i);
                levelObjectCounter++;
            }
            else if(character.ToString() == "a") //active spike
            {
                fields.Add(spike);
                addCoordinates(fields[i], i);
                fields[i].GetComponent<ElementTestTypeInterface>().ActivateSpike();

            }
            else if(character.ToString() == "s") //changable active spike
            {
                fields.Add(spike);
                addCoordinates(fields[i], i);
                fields[i].GetComponent<ElementTestTypeInterface>().isChangableSpike = true;
                fields[i].GetComponent<ElementTestTypeInterface>().ActivateSpike();
            }
            else if(character.ToString() == "c") //changable inactive spike
            {
                fields.Add(spike);
                addCoordinates(fields[i], i);
                fields[i].GetComponent<ElementTestTypeInterface>().isChangableSpike = true;
                fields[i].GetComponent<ElementTestTypeInterface>().DeactivateSpike();
            }
            else if(character.ToString() == "b") // beginningField
            {
                fields.Add(beginningField);
                addCoordinates(fields[i], i);

                players.Add(player);
                addCoordinates(players[0], i);
            }
            else if(character.ToString() == "d") //door
            {
                fields.Add(door);
                addCoordinates(fields[i], i);
            } 
            else if(character.ToString() == "k") //key
            {
                fields.Add(field);
                addCoordinates(fields[i], i);
                fields[i].GetComponent<ElementTestTypeInterface>().isHoldingKey = true;
                
                levelObjects.Add(key);
                addCoordinates(levelObjects[levelObjectCounter], i);
                levelObjectCounter++;
                keys++;
            } 
            else 
            {
                Debug.Log("ERROR - NIEPOPRAWNY SYMBOL");
            }
        }

        for (int i = seed.Length; i < 100; i ++) // We are placing walls on top of level
        {
            placingObject = Instantiate(wall);
            fields.Add(wall);
            addCoordinates(fields[i], i);
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
                    fields.Add(placingObject);
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
