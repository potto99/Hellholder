using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTesting : MonoBehaviour
{
    // [SerializeField] public int StartingX;
    // [SerializeField] public int StartingY;
    [SerializeField] public int TableNumberX;
    [SerializeField] public int TableNumberY;
    [SerializeField] public int TableNumberX_toCheck;
    [SerializeField] public int TableNumberY_toCheck;
    [SerializeField] public List<GameObject> fields;
    [SerializeField] public List<GameObject> objects;
    [SerializeField] public GameObject LevelGenerator;
    [SerializeField] public GameObject LevelTestSupervisorObject;
    [SerializeField] public GameObject TurnCounter;

    public int turn = 0;
    
    public Vector2 targetFieldPosition;
    public string dir = null;
    public int heldKeys = 0;
    [SerializeField] bool needToMove = false;
    LevelTestGeneratorScript LevelGeneratorScript;
    ElementCoordinates ElementCoordinates;
    TurnTestCounterScript TurnCounterScript;
    TestSupervisingScript TestSupervisingScript;
    void Start()
    {
        LevelGenerator = GameObject.Find("LevelTestGeneratorObject");
        LevelGeneratorScript = LevelGenerator.GetComponent<LevelTestGeneratorScript>();
        TurnCounter = GameObject.Find("TurnCounter");
        TurnCounterScript = TurnCounter.GetComponent<TurnTestCounterScript>();
        // TurnCounterScript = LevelGenerator.GetComponent<TurnCounterScript>(); ZAMIEŃ TEN OBIEKT 
        LevelTestSupervisorObject = GameObject.Find("LevelTestSupervisorObject");
        TestSupervisingScript  = LevelTestSupervisorObject.GetComponent<TestSupervisingScript>();
        ElementCoordinates = GetComponent<ElementCoordinates>();
        
        

        TableNumberX = ElementCoordinates.TableNumberX;
        TableNumberY = ElementCoordinates.TableNumberY;
        TableNumberX_toCheck = TableNumberX;
        TableNumberY_toCheck = TableNumberY;
       
    }

    void Update()
    {
        if(dir == "up" && needToMove == false)
        {
            TableNumberY_toCheck = TableNumberY + 1;
            checkThisField(TableNumberX_toCheck, TableNumberY_toCheck);
            dir = null;
        }
        else if(dir =="down" && needToMove == false)
        {   
            TableNumberY_toCheck = TableNumberY - 1 ;
            checkThisField(TableNumberX_toCheck, TableNumberY_toCheck);
            dir = null;
        }
        else if(dir =="right" && needToMove == false)
        {
            TableNumberX_toCheck = TableNumberX + 1;
            checkThisField(TableNumberX_toCheck, TableNumberY_toCheck);
            dir = null;
        }
        else if(dir =="left" && needToMove == false)
        {
            TableNumberX_toCheck = TableNumberX - 1;
            checkThisField(TableNumberX_toCheck, TableNumberY_toCheck);
            dir = null;
        }

        if(needToMove)
        {
            
            needToMove = false;
            TestSupervisingScript.canGetNewMove = true;
        
        }
    }

    public void GoUp()
    {
        dir = "up";
    }
    public void GoDown()
    {
        dir = "down";
    }
    public void GoRight()
    {
        dir = "right";
        
    }
    public void GoLeft()
    {
        dir = "left";
    }



    public void checkThisField(int X, int Y)
    {
        // Debug.Log("checkcheck");
        // Debug.Log("X: " + X + "   Y: " + Y);
        // Debug.Log("checkcheck");
        fields = new List<GameObject>(LevelGeneratorScript.fields);
        foreach(GameObject field in fields)
        {
            ElementCoordinates fieldElementCoorinates = field.GetComponent<ElementCoordinates>();
            // Debug.Log("Field"+fieldElementCoorinates.TableNumberX+""+fieldElementCoorinates.TableNumberY);
            if(fieldElementCoorinates.TableNumberX == X && fieldElementCoorinates.TableNumberY == Y)
            {
                // Debug.Log("znaleziony");
                ElementTestTypeInterface fieldElementTypeInterface = field.GetComponent<ElementTestTypeInterface>();
                if(fieldElementTypeInterface.isWall == true)
                {
                    // Debug.Log("ściana");
                    // TurnCounterScript.TurnDown();
                    // TableNumberX_toCheck = TableNumberX;
                    // TableNumberY_toCheck = TableNumberY;
                    // TestSupervisingScript.canGetNewMove = true;

                    TestSupervisingScript.ChangeSequence();

                    return;
                }
                else if(fieldElementTypeInterface.isField == true)
                {
                    if(fieldElementTypeInterface.isTakenByEnemy)
                    {//Natrafiliśmy na przeciwnika
                        objects = LevelGeneratorScript.levelObjects;
                        foreach(GameObject levelObject in objects)
                        {
                            if(levelObject != null)
                            {
                                ElementCoordinates objectElementCoorinates = levelObject.GetComponent<ElementCoordinates>();
                                ElementTestTypeInterface objectElementTypeInterface = levelObject.GetComponent<ElementTestTypeInterface>();
                                if(objectElementTypeInterface.isEnemy == true && objectElementCoorinates.TableNumberX == TableNumberX_toCheck && objectElementCoorinates.TableNumberY == TableNumberY_toCheck)
                                {
                                    // Destroy(levelObject);
                                    objectElementTypeInterface.Push(dir);
                                    TurnCounterScript.TurnDown();
                                    TableNumberX_toCheck = TableNumberX;
                                    TableNumberY_toCheck = TableNumberY;
                                    TestSupervisingScript.canGetNewMove = true;
                                    break;

                                }
                            }
                        }
                    }
                    else if(fieldElementTypeInterface.isTakenByRock)
                    {//Natrafiliśmy na skałę
                        objects = LevelGeneratorScript.levelObjects;
                        foreach(GameObject levelObject in objects)
                        {
                            if(levelObject != null)
                            {
                                ElementCoordinates objectElementCoorinates = levelObject.GetComponent<ElementCoordinates>();
                                ElementTestTypeInterface objectElementTypeInterface = levelObject.GetComponent<ElementTestTypeInterface>();
                                if(objectElementTypeInterface.isRock == true && objectElementCoorinates.TableNumberX == TableNumberX_toCheck && objectElementCoorinates.TableNumberY == TableNumberY_toCheck)
                                {
                                    // Destroy(levelObject);
                                    objectElementTypeInterface.Push(dir);
                                    TurnCounterScript.TurnDown();
                                    TableNumberX_toCheck = TableNumberX;
                                    TableNumberY_toCheck = TableNumberY;
                                    TestSupervisingScript.canGetNewMove = true;
                                    break;
                                }
                            }
                        }
                    }
                    else if(fieldElementTypeInterface.isHoldingKey)
                    {//Natrafiliśmy na klucz
                        objects = LevelGeneratorScript.levelObjects;
                        foreach(GameObject levelObject in objects)
                        {
                            if(levelObject != null)
                            {
                                ElementCoordinates objectElementCoorinates = levelObject.GetComponent<ElementCoordinates>();
                                ElementTestTypeInterface objectElementTypeInterface = levelObject.GetComponent<ElementTestTypeInterface>();
                                if(objectElementTypeInterface.isKey == true && objectElementCoorinates.TableNumberX == TableNumberX_toCheck && objectElementCoorinates.TableNumberY == TableNumberY_toCheck)
                                {
                                    Destroy(levelObject);
                                    heldKeys++;
                                    TableNumberX = TableNumberX_toCheck;
                                    TableNumberY = TableNumberY_toCheck;
                                    targetFieldPosition = new Vector2(fieldElementCoorinates.positionX, fieldElementCoorinates.positionY);
                                    needToMove = true;
                                    TurnCounterScript.TurnDown();
                                    break;
                                }
                            }
                        }
                    }
                    else if(fieldElementTypeInterface.isSpike)
                    {
                        TableNumberX = TableNumberX_toCheck;
                        TableNumberY = TableNumberY_toCheck;
                        targetFieldPosition = new Vector2(fieldElementCoorinates.positionX, fieldElementCoorinates.positionY);
                        needToMove = true;
                        TurnCounterScript.TurnDown();
                        if(fieldElementTypeInterface.isSpikeActive == true){TurnCounterScript.SpikeTurnLoss();}
                        return;
                    }
                    else
                    {//Natrafiliśmy na puste pole
                        TableNumberX = TableNumberX_toCheck;
                        TableNumberY = TableNumberY_toCheck;
                        targetFieldPosition = new Vector2(fieldElementCoorinates.positionX, fieldElementCoorinates.positionY);
                        transform.position = targetFieldPosition;
                        needToMove = true;
                        TurnCounterScript.TurnDown();
                        return;
                    }
                }
                else if(fieldElementTypeInterface.isDoor == true)
                {
                    if(heldKeys == LevelGeneratorScript.keys)
                    {
                        Debug.Log("Poziom ukończony");
                        TurnCounterScript.TurnDown();
                        int turnsToFinish = TurnCounterScript.GetTurns();
                        TestSupervisingScript.SolutionFound(turnsToFinish);
                        Debug.Log("Znalazłem solucję w " + turnsToFinish + " tur");
                        
                    }
                    else
                    {
                        TurnCounterScript.TurnDown();
                        TableNumberX_toCheck = TableNumberX;
                        TableNumberY_toCheck = TableNumberY;
                        TestSupervisingScript.canGetNewMove = true;
                        return;
                    }
                }
            }
        }
    }
}
