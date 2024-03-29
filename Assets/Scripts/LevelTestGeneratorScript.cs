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
    [SerializeField] GameObject goal;

    public string seed;
    public int MinMoves;
    public int MaxMoves;
    public List<GameObject> levelObjects;
    public List<GameObject> fields;
    public List<GameObject> players;
    public GameObject placingObject;
    public GameObject placingPlayer;
    [SerializeField] GameObject SeedHolder;

    public int keys = 0;
    
    SeedHolderScript SeedHolderScript;


    void Awake() 
    {
        InitializeLevelObjects();    
    }

    public void InitializeLevelObjects()
    {
        keys = 0;
        fields.Clear();
        levelObjects.Clear();
        players.Clear();
        SeedHolder = GameObject.Find("SeedHolderDontDestroy");
        SeedHolderScript = SeedHolder.GetComponent<SeedHolderScript>();
        seed = SeedHolderScript.seed;
        MinMoves = SeedHolderScript.MinMovesAllowed;
        MaxMoves = SeedHolderScript.MaxMovesAllowed;

        char character;
        for (int i = 0; i < seed.Length; i++)
        {
            character = seed[i];
            if(character.ToString() == "f") //field
            {
                placingObject = Instantiate(field);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
            }
            else if(character.ToString() == "w") //wall
            {
                placingObject = Instantiate(wall);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);

            }
            else if(character.ToString() == "r") //rock
            {
                placingObject = Instantiate(field); //field on which the rock will be standing
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTestTypeInterface>().isTakenByRock = true;

                placingObject = Instantiate(rock); 
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTestTypeInterface>().isRock = true;

            }
            else if(character.ToString() == "e") //enemy
            {
                placingObject = Instantiate(field); //field on which the enemy will be standing
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTestTypeInterface>().isTakenByEnemy = true;

                placingObject = Instantiate(enemy); 
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTestTypeInterface>().isEnemy = true;
            }
            else if(character.ToString() == "a") //active spike
            {
                placingObject = Instantiate(spike);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTestTypeInterface>().ActivateSpike();
                placingObject.GetComponent<ElementTestTypeInterface>().isChangableSpike = false;
            }
            else if(character.ToString() == "o") //active spike with rock
            {
                placingObject = Instantiate(spike);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTestTypeInterface>().ActivateSpike();
                placingObject.GetComponent<ElementTestTypeInterface>().isChangableSpike = false;
                placingObject.GetComponent<ElementTestTypeInterface>().isTakenByRock = true;

                placingObject = Instantiate(rock); 
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);

            }
            else if(character.ToString() == "s") //changable active spike
            {
                placingObject = Instantiate(spike);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTestTypeInterface>().isChangableSpike = true;
                placingObject.GetComponent<ElementTestTypeInterface>().ActivateSpike();

            }
            else if(character.ToString() == "m") //changable active spike with rock
            {
                placingObject = Instantiate(spike);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTestTypeInterface>().isChangableSpike = true;
                placingObject.GetComponent<ElementTestTypeInterface>().ActivateSpike();
                placingObject.GetComponent<ElementTestTypeInterface>().isTakenByRock = true;

                placingObject = Instantiate(rock); 
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            }
            else if(character.ToString() == "c") //changable inactive spike
            {
                placingObject = Instantiate(spike);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTestTypeInterface>().isChangableSpike = true;
                placingObject.GetComponent<ElementTestTypeInterface>().DeactivateSpike();
            }
             else if(character.ToString() == "n") //changable inactive spike with rock
            {
                placingObject = Instantiate(spike);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTestTypeInterface>().isChangableSpike = true;
                placingObject.GetComponent<ElementTestTypeInterface>().DeactivateSpike();
                placingObject.GetComponent<ElementTestTypeInterface>().isTakenByRock = true;

                placingObject = Instantiate(rock); 
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            }
            else if(character.ToString() == "b") // beginningField
            {
                placingObject = Instantiate(beginningField);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);

                placingPlayer = Instantiate(player);
                players.Add(placingPlayer);
                addCoordinates(placingPlayer, i);
            }
            else if(character.ToString() == "d") //door
            {
                placingObject = Instantiate(door);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
            } 
            else if(character.ToString() == "k") //key
            {
                placingObject = Instantiate(field); //field on which the key will be standing
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTestTypeInterface>().isHoldingKey = true;
                
                placingObject = Instantiate(key);
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTestTypeInterface>().isKey = true;
                keys++;
            } 
            else if(character.ToString() == "g") //goal
            {
                placingObject = Instantiate(goal);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
            }  
            else 
            {
                Debug.Log("ERROR - NIEPOPRAWNY SYMBOL");
                Debug.Log(i);
                Debug.Log(seed[i]);
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
