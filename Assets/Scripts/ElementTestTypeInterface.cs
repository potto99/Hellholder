using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ElementTestTypeInterface : MonoBehaviour
{
    [SerializeField] public bool isBeginningField;
    [SerializeField] public bool isField;
    [SerializeField] public bool isWall;
    [SerializeField] public bool isRock;
    [SerializeField] public bool isSpike;
    [SerializeField] public bool isEnemy;
    [SerializeField] public bool isKey;
    [SerializeField] public bool isDoor;

    [SerializeField] public bool isChangableSpike;
    [SerializeField] public bool isTakenByRock;
    [SerializeField] public bool isTakenByEnemy;
    [SerializeField] public bool isSpikeActive;
    [SerializeField] public bool isHoldingKey;

    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] public Sprite inactiveSprite;
    [SerializeField] public Sprite activeSprite;

    [SerializeField] public bool needsToMove = false;

    Vector2 targetFieldPosition = new Vector2();


    [SerializeField] public GameObject LevelGenerator;
    LevelTestGeneratorScript LevelGeneratorScript;
    ElementCoordinates myElementCoordinates;

    void Start()
    {
        LevelGenerator = GameObject.Find("LevelTestGeneratorObject");
        LevelGeneratorScript = LevelGenerator.GetComponent<LevelTestGeneratorScript>(); 
        myElementCoordinates = GetComponent<ElementCoordinates>();
    }

    void Update()
    {
        
    }
    
    

    public void ChangeSpikeState()
    {
        
        if(isSpikeActive == true && isSpike == true && isChangableSpike)
        {
            DeactivateSpike();
        }
        else if(isSpikeActive == false && isSpike == true && isChangableSpike)
        {
            ActivateSpike();
        }
    }
    public void ActivateSpike()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        isSpikeActive = true;
        spriteRenderer.sprite = activeSprite;
        if(isTakenByEnemy)
        {

            List<GameObject> levelObjects = LevelGeneratorScript.levelObjects;
            foreach(GameObject levelObject in levelObjects)
            {
                if(levelObject != null)
                {
                    ElementTestTypeInterface objectElementTypeInterface = levelObject.GetComponent<ElementTestTypeInterface>();
                    if(objectElementTypeInterface.isEnemy == true)
                    {
                        ElementCoordinates enemyCoordinates = levelObject.GetComponent<ElementCoordinates>();
                        if(enemyCoordinates.TableNumberX == myElementCoordinates.TableNumberX && enemyCoordinates.TableNumberY == myElementCoordinates.TableNumberY)
                        {
                            Destroy(levelObject);
                            isTakenByEnemy = false;
                            break;
                        }
                    
                    }
                }
            }
        }

        
    }
    public void DeactivateSpike()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        isSpikeActive = false;
        spriteRenderer.sprite = inactiveSprite;

       
        
    }

    public void Push(string dir)
    {
        int TableNumberX_toCheck = myElementCoordinates.TableNumberX;
        int TableNumberY_toCheck = myElementCoordinates.TableNumberY;
        if(dir == "right"){TableNumberX_toCheck++;}
        else if(dir == "left"){TableNumberX_toCheck--;}
        else if(dir == "up"){TableNumberY_toCheck++;}
        else if(dir == "down"){TableNumberY_toCheck--;}

        List<GameObject> fields = LevelGeneratorScript.fields;
        foreach(GameObject field in fields)
        {
            ElementCoordinates fieldCoordinates = field.GetComponent<ElementCoordinates>();
            if(fieldCoordinates.TableNumberX == TableNumberX_toCheck && fieldCoordinates.TableNumberY == TableNumberY_toCheck)
            {
                ElementTestTypeInterface fieldElementTypeInterface = field.GetComponent<ElementTestTypeInterface>();
                if(fieldElementTypeInterface.isTakenByEnemy == false && fieldElementTypeInterface.isTakenByRock == false && fieldElementTypeInterface.isWall == false && fieldElementTypeInterface.isDoor == false)
                {
                    // W przyszłości trzeba tu też sprawdzać, czy spike jest aktywny
                    if(isEnemy == true){fieldElementTypeInterface.isTakenByEnemy = true;}
                    else if(isRock == true){fieldElementTypeInterface.isTakenByRock = true;}
                    ReleasePreviousField(myElementCoordinates.TableNumberX, myElementCoordinates.TableNumberY);
                    myElementCoordinates.TableNumberX = TableNumberX_toCheck;
                    myElementCoordinates.TableNumberY = TableNumberY_toCheck;
                    targetFieldPosition = new Vector2(fieldCoordinates.positionX, fieldCoordinates.positionY);
                    
        
                    transform.position = targetFieldPosition;
                    if(isEnemy)
                    {   
                        if((fieldElementTypeInterface.isChangableSpike == false && fieldElementTypeInterface.isSpikeActive == true) || (fieldElementTypeInterface.isChangableSpike == true && fieldElementTypeInterface.isSpikeActive == false))
                        {
                            ReleaseBeforeDestroying();
                            Destroy(gameObject);
                        }   
                        
                    }
           

                }
                else
                {
                    if(isEnemy)
                    {
                        ReleaseBeforeDestroying();
                        Destroy(gameObject);
                    }
                }
            }
        }

    }

    public void ReleasePreviousField(int X_toRelease, int Y_toRelease)
    {
        List<GameObject> fieldsToRelease = LevelGeneratorScript.fields;
        foreach(GameObject field in fieldsToRelease)
        {
            if(field!=null) //Warunek potrzebny, żeby silnik nie płakał błędami w trakcie wychodzenia z testu
            {
                ElementCoordinates fieldToReleaseElementCoordinates = field.GetComponent<ElementCoordinates>();
                if(fieldToReleaseElementCoordinates.TableNumberX == X_toRelease && fieldToReleaseElementCoordinates.TableNumberY == Y_toRelease)
                {
                    ElementTestTypeInterface fieldToReleaseElementTypeInterface = field.GetComponent<ElementTestTypeInterface>();
                    if(isEnemy == true){fieldToReleaseElementTypeInterface.isTakenByEnemy = false;}
                    else if(isRock == true){fieldToReleaseElementTypeInterface.isTakenByRock = false;}
                    
                }
            }
        }
    }


    
    void ReleaseBeforeDestroying()
    {
        {
        LevelGeneratorScript.levelObjects.Remove(this.gameObject);
        LevelGeneratorScript.levelObjects.TrimExcess();
        if(isEnemy)
        {

            ReleasePreviousField(myElementCoordinates.TableNumberX, myElementCoordinates.TableNumberY);
        }
        if(isKey)
        {
            List<GameObject> fields = LevelGeneratorScript.fields; 
            foreach(GameObject field in fields)
            {
                if(field != null) //Warunek potrzebny, żeby kompilator nie płakał błędami w trakcie wychodzenia z playtestu
                {
                    ElementCoordinates fieldCoordinates = field.GetComponent<ElementCoordinates>();
                    
                    if(fieldCoordinates.TableNumberX == myElementCoordinates.TableNumberX && fieldCoordinates.TableNumberY == myElementCoordinates.TableNumberY)
                    {
                        ElementTestTypeInterface fieldElementTypeInterface = field.GetComponent<ElementTestTypeInterface>();
                        fieldElementTypeInterface.isHoldingKey = false;
                    }
                }
            }
        }

    }   
    }
    
    // void OnDestroy()
    // {
    //     LevelGeneratorScript.levelObjects.Remove(this.gameObject);
    //     LevelGeneratorScript.levelObjects.TrimExcess();
    //     if(isEnemy)
    //     {
    //         // List<GameObject> fields = LevelGeneratorScript.fields; 
    //         // foreach(GameObject field in fields)
    //         // {
    //         //     if(field != null) //Warunek potrzebny, żeby kompilator nie płakał błędami w trakcie wychodzenia z playtestu
    //         //     {
    //         //         ElementCoordinates fieldCoordinates = field.GetComponent<ElementCoordinates>();
                    
    //         //         if(fieldCoordinates.TableNumberX == myElementCoordinates.TableNumberX && fieldCoordinates.TableNumberY == myElementCoordinates.TableNumberY)
    //         //         {
    //         //             ElementTestTypeInterface fieldElementTypeInterface = field.GetComponent<ElementTestTypeInterface>();
    //         //             fieldElementTypeInterface.isTakenByEnemy = false;
    //         //         }
    //         //     }
    //         // }

    //         ReleasePreviousField(myElementCoordinates.TableNumberX, myElementCoordinates.TableNumberY);
    //     }
    //     if(isKey)
    //     {
    //         List<GameObject> fields = LevelGeneratorScript.fields; 
    //         foreach(GameObject field in fields)
    //         {
    //             if(field != null) //Warunek potrzebny, żeby kompilator nie płakał błędami w trakcie wychodzenia z playtestu
    //             {
    //                 ElementCoordinates fieldCoordinates = field.GetComponent<ElementCoordinates>();
                    
    //                 if(fieldCoordinates.TableNumberX == myElementCoordinates.TableNumberX && fieldCoordinates.TableNumberY == myElementCoordinates.TableNumberY)
    //                 {
    //                     ElementTestTypeInterface fieldElementTypeInterface = field.GetComponent<ElementTestTypeInterface>();
    //                     fieldElementTypeInterface.isHoldingKey = false;
    //                 }
    //             }
    //         }
    //     }

    // }   
}
