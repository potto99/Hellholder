using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTesting : MonoBehaviour
{
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
        LevelTestSupervisorObject = GameObject.Find("LevelTestSupervisorObject");
        TestSupervisingScript  = LevelTestSupervisorObject.GetComponent<TestSupervisingScript>();
        ElementCoordinates = GetComponent<ElementCoordinates>();
        
        TableNumberX = ElementCoordinates.TableNumberX;
        TableNumberY = ElementCoordinates.TableNumberY;
        TableNumberX_toCheck = TableNumberX;
        TableNumberY_toCheck = TableNumberY;
    }

    void FixedUpdate()
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
        fields = new List<GameObject>(LevelGeneratorScript.fields);
        foreach(GameObject field in fields)
        {
            ElementCoordinates fieldElementCoordinates = field.GetComponent<ElementCoordinates>();
            if(fieldElementCoordinates.TableNumberX == X && fieldElementCoordinates.TableNumberY == Y)
            {
                ElementTestTypeInterface fieldElementTypeInterface = field.GetComponent<ElementTestTypeInterface>();
                if(fieldElementTypeInterface.isWall == true)
                {
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
                                ElementCoordinates objectElementCoordinates = levelObject.GetComponent<ElementCoordinates>();
                                ElementTestTypeInterface objectElementTypeInterface = levelObject.GetComponent<ElementTestTypeInterface>();
                                if(objectElementTypeInterface.isEnemy == true && objectElementCoordinates.TableNumberX == TableNumberX_toCheck && objectElementCoordinates.TableNumberY == TableNumberY_toCheck)
                                {
                                    // Destroy(levelObject);
                                    objectElementTypeInterface.Push(dir);
                                    TurnCounterScript.TurnDown();
                                    TableNumberX_toCheck = TableNumberX;
                                    TableNumberY_toCheck = TableNumberY;
                                    CheckIfImStandingOnSpike(TableNumberX, TableNumberY);
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
                                ElementCoordinates objectElementCoordinates = levelObject.GetComponent<ElementCoordinates>();
                                ElementTestTypeInterface objectElementTypeInterface = levelObject.GetComponent<ElementTestTypeInterface>();
                                if(objectElementTypeInterface.isRock == true && objectElementCoordinates.TableNumberX == TableNumberX_toCheck && objectElementCoordinates.TableNumberY == TableNumberY_toCheck)
                                {
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
                                ElementCoordinates objectElementCoordinates = levelObject.GetComponent<ElementCoordinates>();
                                ElementTestTypeInterface objectElementTypeInterface = levelObject.GetComponent<ElementTestTypeInterface>();
                                if(objectElementTypeInterface.isKey == true && objectElementCoordinates.TableNumberX == TableNumberX_toCheck && objectElementCoordinates.TableNumberY == TableNumberY_toCheck)
                                {
                                    Destroy(levelObject);
                                    fieldElementTypeInterface.isHoldingKey = false;
                                    heldKeys++;
                                    TableNumberX = TableNumberX_toCheck;
                                    TableNumberY = TableNumberY_toCheck;
                                    targetFieldPosition = new Vector2(fieldElementCoordinates.positionX, fieldElementCoordinates.positionY);
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
                        targetFieldPosition = new Vector2(fieldElementCoordinates.positionX, fieldElementCoordinates.positionY);
                        needToMove = true;
                        TurnCounterScript.TurnDown();
                        if(fieldElementTypeInterface.isSpikeActive == true){TurnCounterScript.SpikeTurnLoss();}
                        return;
                    }
                    else
                    {//Natrafiliśmy na puste pole
                        TableNumberX = TableNumberX_toCheck;
                        TableNumberY = TableNumberY_toCheck;
                        targetFieldPosition = new Vector2(fieldElementCoordinates.positionX, fieldElementCoordinates.positionY);
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
                        TableNumberX = TableNumberX_toCheck;
                        TableNumberY = TableNumberY_toCheck;
                        targetFieldPosition = new Vector2(fieldElementCoordinates.positionX, fieldElementCoordinates.positionY);
                        transform.position = targetFieldPosition;
                        needToMove = true;
                        TurnCounterScript.TurnDown();
                        return;
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
                else if(fieldElementTypeInterface.isGoal == true)
                {
                    Debug.Log("Poziom ukończony");
                    TurnCounterScript.TurnDown();
                    int turnsToFinish = TurnCounterScript.GetTurns();
                    TestSupervisingScript.SolutionFound(turnsToFinish);
                    Debug.Log("Znalazłem solucję w " + turnsToFinish + " tur");
                }
            }
        }
    }

    public void CheckIfImStandingOnSpike(int X, int Y)
    {
        foreach(GameObject field in fields)
        {
            ElementCoordinates fieldElementCoordinates = field.GetComponent<ElementCoordinates>();
            if(fieldElementCoordinates.TableNumberX == X && fieldElementCoordinates.TableNumberY == Y)
            {
                ElementTestTypeInterface fieldElementTypeInterface = field.GetComponent<ElementTestTypeInterface>();
                if(fieldElementTypeInterface.isSpike)
                {
                    if(fieldElementTypeInterface.isSpikeActive == true){TurnCounterScript.SpikeTurnLoss();}
                }
            }
        }
    }
    
}
