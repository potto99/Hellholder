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
    public GameObject placingPlayer;
    [SerializeField] GameObject SeedHolder;

    public int keys = 0;
    public int levelObjectCounter = 0;
    
    SeedHolderScript SeedHolderScript;


    public void Start()
    {
        fields.Clear();
        levelObjects.Clear();
        SeedHolder = GameObject.Find("SeedHolder");
        SeedHolderScript = SeedHolder.GetComponent<SeedHolderScript>();
        seed = SeedHolderScript.seed;

        char character;
        for (int i = 0; i < seed.Length; i ++)
        {
            character = seed[i];
            if(character.ToString() == "f") //field
            {
                // fields.Add(field);
                // addCoordinates(fields[i], i);

                placingObject = Instantiate(field);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);

            }
            else if(character.ToString() == "w") //wall
            {
                // fields.Add(wall);
                // addCoordinates(fields[i], i);

                placingObject = Instantiate(wall);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);

            }
            else if(character.ToString() == "r") //rock
            {
                // fields.Add(field);
                // addCoordinates(fields[i], i);
                // fields[i].GetComponent<ElementTestTypeInterface>().isTakenByRock = true;

                placingObject = Instantiate(field); //field on which the rock will be standing
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTypeInterface>().isTakenByRock = true;


                // levelObjects.Add(rock);
                // addCoordinates(levelObjects[levelObjectCounter], i);
                // levelObjectCounter++;

                placingObject = Instantiate(rock); 
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);

            }
            else if(character.ToString() == "e") //enemy
            {
                // fields.Add(field);
                // addCoordinates(fields[i], i);
                // fields[i].GetComponent<ElementTestTypeInterface>().isTakenByEnemy = true;

                placingObject = Instantiate(field); //field on which the enemy will be standing
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTypeInterface>().isTakenByEnemy = true;

                // levelObjects.Add(enemy);
                // addCoordinates(levelObjects[levelObjectCounter], i);
                // levelObjectCounter++;

                placingObject = Instantiate(enemy); 
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);

            }
            else if(character.ToString() == "a") //active spike
            {
                // fields.Add(spike);
                // addCoordinates(fields[i], i);
                // fields[i].GetComponent<ElementTestTypeInterface>().ActivateSpike();

                placingObject = Instantiate(spike);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTypeInterface>().ActivateSpike();

            }
            else if(character.ToString() == "s") //changable active spike
            {
                // fields.Add(spike);
                // addCoordinates(fields[i], i);
                // fields[i].GetComponent<ElementTestTypeInterface>().isChangableSpike = true;
                // fields[i].GetComponent<ElementTestTypeInterface>().ActivateSpike();

                placingObject = Instantiate(spike);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTypeInterface>().isChangableSpike = true;
                placingObject.GetComponent<ElementTypeInterface>().ActivateSpike();

            }
            else if(character.ToString() == "c") //changable inactive spike
            {
                // fields.Add(spike);
                // addCoordinates(fields[i], i);
                // fields[i].GetComponent<ElementTestTypeInterface>().isChangableSpike = true;
                // fields[i].GetComponent<ElementTestTypeInterface>().DeactivateSpike();

                placingObject = Instantiate(spike);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTypeInterface>().isChangableSpike = true;
                placingObject.GetComponent<ElementTypeInterface>().DeactivateSpike();

            }
            else if(character.ToString() == "b") // beginningField
            {
                // fields.Add(beginningField);
                // addCoordinates(fields[i], i);

                placingObject = Instantiate(beginningField);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);

                // players.Add(player);
                // addCoordinates(players[0], i);

                placingPlayer = Instantiate(player);
                players.Add(placingPlayer);
                addCoordinates(player, i);
            }
            else if(character.ToString() == "d") //door
            {
                // fields.Add(door);
                // addCoordinates(fields[i], i);

                placingObject = Instantiate(door);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
            } 
            else if(character.ToString() == "k") //key
            {
                // fields.Add(field);
                // addCoordinates(fields[i], i);
                // fields[i].GetComponent<ElementTestTypeInterface>().isHoldingKey = true;

                placingObject = Instantiate(field); //field on which the key will be standing
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTypeInterface>().isHoldingKey = true;
                
                // levelObjects.Add(key);
                // addCoordinates(levelObjects[levelObjectCounter], i);
                // levelObjectCounter++;
                // keys++;

                placingObject = Instantiate(key);
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
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
            fields.Add(placingObject);
            addCoordinates(placingObject, i);
        }

        

        
        createWallFrame();


    }


    void addCoordinates(GameObject element, int ID)
    {
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

    public void Purge()
    {
        foreach(GameObject levelObject in levelObjects)
        {
            Destroy(levelObject);
        }
        levelObjects.Clear();
        
        foreach(GameObject field in fields)
        {
            Destroy(field);
        }
        fields.Clear();

        foreach(GameObject player in players)
        {
            Destroy(player);
        }

        players.Clear();

    }
}
