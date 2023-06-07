using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTestingScript : MonoBehaviour
{
    LevelTestGeneratorScript LevelTestGeneratorScript;
    TurnCounterScript TurnCounterScript;
    ElementCoordinates ElementCoordinates;
    int TableNumberX;
    int TableNumberY;
    int TableNumberX_toCheck;
    int TableNumberY_toCheck;


    void Start()
    {
        LevelTestGeneratorScript = GetComponent<LevelTestGeneratorScript>();
        TurnCounterScript = GetComponent<TurnCounterScript>();
        // ElementCoordinates = GetComponent<ElementCoordinates>();
        // TableNumberX = ElementCoordinates.TableNumberX;
        // TableNumberY = ElementCoordinates.TableNumberY;
        TableNumberX_toCheck = TableNumberX;
        TableNumberY_toCheck = TableNumberY; 
    }

    

    
    void Update()
    {
        
    }
}
