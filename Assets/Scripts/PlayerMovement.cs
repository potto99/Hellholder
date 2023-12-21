using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] public int TableNumberX;
    [SerializeField] public int TableNumberY;
    [SerializeField] public int TableNumberX_toCheck;
    [SerializeField] public int TableNumberY_toCheck;
    [SerializeField] public List<GameObject> fields;
    [SerializeField] public List<GameObject> objects;
    [SerializeField] public GameObject LevelGenerator;
    public Vector2 targetFieldPosition;
    public string dir = null;
    public int heldKeys = 0;
    [SerializeField] bool needToMove = false;
    LevelGeneratorScript LevelGeneratorScript;
    ElementCoordinates ElementCoordinates;
    TurnCounterScript TurnCounterScript;
    void Start()
    {
        LevelGenerator = GameObject.Find("LevelGeneratorObject");
        LevelGeneratorScript = LevelGenerator.GetComponent<LevelGeneratorScript>();
        TurnCounterScript = LevelGenerator.GetComponent<TurnCounterScript>();
        ElementCoordinates = GetComponent<ElementCoordinates>();
        TableNumberX = ElementCoordinates.TableNumberX;
        TableNumberY = ElementCoordinates.TableNumberY;
        TableNumberX_toCheck = TableNumberX;
        TableNumberY_toCheck = TableNumberY;
       
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && needToMove == false)
        {
            TableNumberY_toCheck = TableNumberY + 1;
            dir ="up";
            checkThisField(TableNumberX_toCheck, TableNumberY_toCheck);
            dir = null;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) && needToMove == false)
        {   
            TableNumberY_toCheck = TableNumberY -1 ;
            dir ="down";
            checkThisField(TableNumberX_toCheck, TableNumberY_toCheck);
            dir = null;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) && needToMove == false)
        {
            TableNumberX_toCheck = TableNumberX + 1;
            dir ="right";
            checkThisField(TableNumberX_toCheck, TableNumberY_toCheck);
            dir = null;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && needToMove == false)
        {
            TableNumberX_toCheck = TableNumberX - 1;
            dir ="left";
            checkThisField(TableNumberX_toCheck, TableNumberY_toCheck);
            dir = null;
        }

        if(needToMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetFieldPosition, 2f);

            if(Math.Abs(transform.position.x - targetFieldPosition.x) < 1 && Math.Abs(transform.position.y - targetFieldPosition.y) < 1)
            {
                foreach(GameObject field in fields)
                {
                    ElementCoordinates fieldElementCoordinates = field.GetComponent<ElementCoordinates>();
                    if(fieldElementCoordinates.TableNumberX == TableNumberX && fieldElementCoordinates.TableNumberY == TableNumberY)
                    {
                        ElementTypeInterface fieldElementTypeInterface = field.GetComponent<ElementTypeInterface>();
                        if(fieldElementTypeInterface.isSpikeActive == true)
                        {
                            TurnCounterScript.SpikeTurnLoss();
                        }
                    }
                }
                needToMove = false;
            }
        }
       




        
				

    }


    public void checkThisField(int X, int Y)
    {
        fields = LevelGeneratorScript.fields;
        foreach(GameObject field in fields)
        {
            ElementCoordinates fieldElementCoordinates = field.GetComponent<ElementCoordinates>();
            if(fieldElementCoordinates.TableNumberX == X && fieldElementCoordinates.TableNumberY == Y)
            {
                ElementTypeInterface fieldElementTypeInterface = field.GetComponent<ElementTypeInterface>();
                if(fieldElementTypeInterface.isWall == true)
                {
                    TableNumberX_toCheck = TableNumberX;
                    TableNumberY_toCheck = TableNumberY;
                    return;
                }
                else if(fieldElementTypeInterface.isField == true)
                {
                    if(fieldElementTypeInterface.isTakenByEnemy)
                    {//Natrafiliśmy na przeciwnika
                        objects = LevelGeneratorScript.levelObjects;
                        foreach(GameObject levelObject in objects)
                        {
                            ElementCoordinates objectElementCoordinates = levelObject.GetComponent<ElementCoordinates>();
                            ElementTypeInterface objectElementTypeInterface = levelObject.GetComponent<ElementTypeInterface>();
                            if(objectElementTypeInterface.isEnemy == true && objectElementCoordinates.TableNumberX == TableNumberX_toCheck && objectElementCoordinates.TableNumberY == TableNumberY_toCheck)
                            {
                                // Destroy(levelObject);
                                objectElementTypeInterface.Push(dir);
                                TurnCounterScript.TurnDown();
                                TableNumberX_toCheck = TableNumberX;
                                TableNumberY_toCheck = TableNumberY;
                                CheckIfImStandingOnSpike(TableNumberX, TableNumberY);
                                break;

                            }
                        }
                    }
                    else if(fieldElementTypeInterface.isTakenByRock)
                    {//Natrafiliśmy na skałę
                        objects = LevelGeneratorScript.levelObjects;
                        foreach(GameObject levelObject in objects)
                        {
                            ElementCoordinates objectElementCoordinates = levelObject.GetComponent<ElementCoordinates>();
                            ElementTypeInterface objectElementTypeInterface = levelObject.GetComponent<ElementTypeInterface>();
                            if(objectElementTypeInterface.isRock == true && objectElementCoordinates.TableNumberX == TableNumberX_toCheck && objectElementCoordinates.TableNumberY == TableNumberY_toCheck)
                            {
                                // Destroy(levelObject);
                                objectElementTypeInterface.Push(dir);
                                TurnCounterScript.TurnDown();
                                TableNumberX_toCheck = TableNumberX;
                                TableNumberY_toCheck = TableNumberY;
                                CheckIfImStandingOnSpike(TableNumberX, TableNumberY);
                                break;
                            }
                        }
                    }
                    else if(fieldElementTypeInterface.isHoldingKey)
                    {//Natrafiliśmy na klucz
                        objects = LevelGeneratorScript.levelObjects;
                        foreach(GameObject levelObject in objects)
                        {
                            ElementCoordinates objectElementCoordinates = levelObject.GetComponent<ElementCoordinates>();
                            ElementTypeInterface objectElementTypeInterface = levelObject.GetComponent<ElementTypeInterface>();
                            if(objectElementTypeInterface.isKey == true && objectElementCoordinates.TableNumberX == TableNumberX_toCheck && objectElementCoordinates.TableNumberY == TableNumberY_toCheck)
                            {
                                Destroy(levelObject);
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
                    else
                    {//Natrafiliśmy na puste pole
                        TableNumberX = TableNumberX_toCheck;
                        TableNumberY = TableNumberY_toCheck;
                        targetFieldPosition = new Vector2(fieldElementCoordinates.positionX, fieldElementCoordinates.positionY);
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
                        needToMove = true;
                        TurnCounterScript.TurnDown();
                        return;
                    }
                    else
                    {
                        TurnCounterScript.TurnDown();
                        TableNumberX_toCheck = TableNumberX;
                        TableNumberY_toCheck = TableNumberY;
                        return;
                    }
                }
                else if(fieldElementTypeInterface.isGoal == true)
                {
                    TurnCounterScript.TurnDown();
                    TurnCounterScript.DeclareWin();
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
                ElementTypeInterface fieldElementTypeInterface = field.GetComponent<ElementTypeInterface>();
                if(fieldElementTypeInterface.isSpike)
                {
                    if(fieldElementTypeInterface.isSpikeActive == true){TurnCounterScript.SpikeTurnLoss();}
                }
            }
        }
    }
}
