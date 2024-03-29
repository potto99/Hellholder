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
    [SerializeField] GameObject goal;

    
    public string seed;
    public List<GameObject> levelObjects;
    public List<GameObject> fields;
    public List<GameObject> players;
    public GameObject placingObject;
    [SerializeField] GameObject SeedHolder;

    public int keys = 0;
    
    SeedHolderScript SeedHolderScript;
    void Start()
    {
        SeedHolder = GameObject.Find("SeedHolderDontDestroy");
        SeedHolderScript = SeedHolder.GetComponent<SeedHolderScript>();
        seed = SeedHolderScript.seed;

        char character;
        for (int i = 0; i < seed.Length; i ++)
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
                placingObject.GetComponent<ElementTypeInterface>().isTakenByRock = true;

                placingObject = Instantiate(rock); 
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            }
            else if(character.ToString() == "e") //enemy
            {
                placingObject = Instantiate(field); //field on which the enemy will be standing
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTypeInterface>().isTakenByEnemy = true;

                placingObject = Instantiate(enemy); 
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            }
            else if(character.ToString() == "a") //active spike
            {
                placingObject = Instantiate(spike);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTypeInterface>().ActivateSpike();
                placingObject.GetComponent<ElementTypeInterface>().isChangableSpike = false;

            }
            else if(character.ToString() == "o") //active spike with rock
            {
                placingObject = Instantiate(spike);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTypeInterface>().ActivateSpike();
                placingObject.GetComponent<ElementTypeInterface>().isChangableSpike = false;
                placingObject.GetComponent<ElementTypeInterface>().isTakenByRock = true;

                placingObject = Instantiate(rock); 
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);

            }
            else if(character.ToString() == "s") //changable active spike
            {
                placingObject = Instantiate(spike);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTypeInterface>().isChangableSpike = true;
                placingObject.GetComponent<ElementTypeInterface>().ActivateSpike();
            }
            else if(character.ToString() == "m") //changable active spike with rock
            {
                placingObject = Instantiate(spike);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTypeInterface>().isChangableSpike = true;
                placingObject.GetComponent<ElementTypeInterface>().ActivateSpike();
                placingObject.GetComponent<ElementTypeInterface>().isTakenByRock = true;

                placingObject = Instantiate(rock); 
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            }
            else if(character.ToString() == "c") //changable inactive spike
            {
                placingObject = Instantiate(spike);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTypeInterface>().isChangableSpike = true;
                placingObject.GetComponent<ElementTypeInterface>().DeactivateSpike();
            }
            else if(character.ToString() == "n") //changable inactive spike with rock
            {
                placingObject = Instantiate(spike);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);
                placingObject.GetComponent<ElementTypeInterface>().isChangableSpike = true;
                placingObject.GetComponent<ElementTypeInterface>().DeactivateSpike();
                placingObject.GetComponent<ElementTypeInterface>().isTakenByRock = true;

                placingObject = Instantiate(rock); 
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
            }
            else if(character.ToString() == "b") // beginningField
            {
                placingObject = Instantiate(beginningField);
                fields.Add(placingObject);
                addCoordinates(placingObject, i);

                player = Instantiate(player);
                players.Add(player);
                addCoordinates(player, i);
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
                placingObject.GetComponent<ElementTypeInterface>().isHoldingKey = true;
                
                placingObject = Instantiate(key);
                levelObjects.Add(placingObject);
                addCoordinates(placingObject, i);
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

    
    
}
