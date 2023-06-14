using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSupervisingScript : MonoBehaviour
{
    // string movementSequence = "dddddddddddddddddddddddddddddddddddddddd";
    char[] movementSequence = new char[] {'d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d'};

    public int turn = 0;
    public int minMoves = 40;
    public int highestChanged = 0;
    public int currentlyVariablePlace = 0;
    public int iteraionOfVariable = 1;
    [SerializeField] GameObject LevelTestGeneratorObject;
    [SerializeField] LevelTestGeneratorScript LevelTestGeneratorScript;
    [SerializeField] GameObject TurnCounter;
    [SerializeField] TurnTestCounterScript TurnCounterScript;
    [SerializeField] GameObject Player;
    [SerializeField] PlayerTesting PlayerTesting;
    public bool timeForNextmove = false;
    public bool timeForNextCheck = false;
    public bool canGetNewMove = true;
    char character = '0';

    void Start()
    {
        LevelTestGeneratorObject = GameObject.Find("LevelTestGeneratorObject");
        LevelTestGeneratorScript = LevelTestGeneratorObject.GetComponent<LevelTestGeneratorScript>();

        TurnCounter = GameObject.Find("TurnCounter");
        TurnCounterScript = TurnCounter.GetComponent<TurnTestCounterScript>();

        Player =  LevelTestGeneratorScript.players[0];
        PlayerTesting = Player.GetComponent<PlayerTesting>();
    }
    void Update()
    {
        
       
        
        if(canGetNewMove)
        {
            
            if(highestChanged > minMoves)
            {
                AllPossibilitiesChecked();
                canGetNewMove = false;
                return;
            }

            character = movementSequence[turn];
            if(character == 'd'){PlayerTesting.GoRight(); canGetNewMove = false;}
            else if(character == 'w'){PlayerTesting.GoUp(); canGetNewMove = false;}
            else if(character == 's'){PlayerTesting.GoDown(); canGetNewMove = false;}
            else if(character == 'a'){PlayerTesting.GoLeft(); canGetNewMove = false;}
            turn++;
            // Debug.Log(turn);
            if(turn > minMoves){ChangeSequence();}
            
        }
    }

    void ChangeSequence()
    {
        
        
        string movementSequenceString = new string(movementSequence);
        if(movementSequenceString == "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")
        {
            Debug.Log("Brak możliwości rozwiązania");
            AllPossibilitiesChecked();
            return;
        }
        // bool nextOneToChange = false;
        // char i;
        turn = 0;
        // i = movementSequence[currentlyVariablePlace];
        
       
        
        for(int i = 0; i<40; i++)
        {
            
           

            if(movementSequence[i] == 'd')
            {
                movementSequence[i] = 'w';
                if(i > highestChanged){highestChanged = i;}
                break;
                // if(nextOneToChange == true){break;}
            }
            else if(movementSequence[i] == 'w')
            {
                movementSequence[i] = 's';
                if(i > highestChanged){highestChanged = i;}
                break;
                // if(nextOneToChange == true){break;}
            }
            else if(movementSequence[i] == 's')
            {
                movementSequence[i] = 'a';
                if(i > highestChanged){highestChanged = i;}
                break;
                // if(nextOneToChange == true){break;}
            }
            else if(movementSequence[i] == 'a')
            {
                movementSequence[i] = 'd'; 
                // nextOneToChange = true;
            }
        }


        

        
        movementSequenceString = new string(movementSequence);
        Debug.Log("Sprawdzam dla: " + movementSequenceString);

        //Resetowanie poziomu

        LevelTestGeneratorScript.Purge();
        TurnCounterScript.movesLeft = 40;
        LevelTestGeneratorScript.Start();

        Player =  LevelTestGeneratorScript.players[0];
        PlayerTesting = Player.GetComponent<PlayerTesting>();

        canGetNewMove = true;


    }

    public void SolutionFound()
    {
        minMoves = turn-1;
        string movementSequenceString = new string(movementSequence);
        Debug.Log("Nowa solucja: " + movementSequenceString);

        ChangeSequence();
        // Debug.Log(movementSequenceString);
    }

    public void AllPossibilitiesChecked()
    {
        Debug.Log("Wszystkie opcje sprawdzone");
        Debug.Log("HighestChanged: " + highestChanged);
        Debug.Log("MinMoves: " + minMoves);
    }

    
}
