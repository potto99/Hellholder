using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestSupervisingScript : MonoBehaviour
{
    [SerializeField] public TMP_Text TestingStatus;
    [SerializeField] public TMP_InputField SeedToCopy;
    [SerializeField] public TMP_Text RequiredMoves;
    [SerializeField] public TMP_Text RequiredTurns;
    [SerializeField] public TMP_InputField SolutionToCopy;
    int testingTextCount = 1;
    // string movementSequence = "dddddddddddddddddddddddddddddddddddddddd";
    char[] movementSequence = new char[] {'d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d'};

    
    

    public int move = 0;
    public int minMoves = 40;
    public int minTurnsToFinish = 40;
    public int bestSolutionMoves = 40;
    public int bestSolutionTurns = 40;
    public int highestChanged = 0;
    public int currentlyVariablePlace = 0;
    public int iteraionOfVariable = 1;
    GameObject LevelTestGenerator;
    [SerializeField] GameObject LevelTestGeneratorObject;
    [SerializeField] LevelTestGeneratorScript LevelTestGeneratorScript;
    [SerializeField] GameObject TurnCounter;
    [SerializeField] TurnTestCounterScript TurnCounterScript;
    [SerializeField] GameObject Player;
    [SerializeField] PlayerTesting PlayerTesting;
    public bool timeForNextmove = false;
    public bool timeForNextCheck = false;
    public bool canGetNewMove = true;
    public bool finishedSearching = false;
    public bool levelIsSolvable = false;
    public bool solvingContonously = false;
    string solutionSequenceString = null;
    string bestSolutionSequenceString = null;
    char character = '0';

    void Start()
    {
        
        LevelTestGeneratorObject = GameObject.Find("LevelTestGeneratorObject");
        // LevelTestGenerator = Instantiate(LevelTestGeneratorObject);
        LevelTestGeneratorScript = LevelTestGeneratorObject.GetComponent<LevelTestGeneratorScript>();
        SeedToCopy.text = LevelTestGeneratorScript.seed;

        TurnCounter = GameObject.Find("TurnCounter");
        TurnCounterScript = TurnCounter.GetComponent<TurnTestCounterScript>();

        Player = LevelTestGeneratorScript.players[0];
        PlayerTesting = Player.GetComponent<PlayerTesting>();

        GameObject ContinousTester = GameObject.Find("ObjectIndicatingContinousTests");
        if(ContinousTester != null)
        {
            Debug.Log("Contonous Tester");
            if(ContinousTester.GetComponent<ContinousTests>().testContinous == true)
            {
                Debug.Log("Testing Continously");
                solvingContonously = true;
            }

        }
    }
    void Update()
    {
        
       
        
        if(canGetNewMove && finishedSearching == false && Player!=null)
        {
            
            if(highestChanged >= minTurnsToFinish)
            {
                AllPossibilitiesChecked();
                canGetNewMove = false;
                return;
            }
            // Debug.Log(turn);
            
            character = movementSequence[move];
            
            
            if(character == 'd'){PlayerTesting.GoRight(); canGetNewMove = false;}
            else if(character == 'w'){PlayerTesting.GoUp(); canGetNewMove = false;}
            else if(character == 's'){PlayerTesting.GoDown(); canGetNewMove = false;}
            else if(character == 'a'){PlayerTesting.GoLeft(); canGetNewMove = false;}
            move++;
            // Debug.Log(turn);
            if(move >= minTurnsToFinish){ChangeSequence();}
            
        }
    }

    void ChangeSequence()
    {
        ChangeTestingText(false);
        
        string movementSequenceString = new string(movementSequence);
        
        //---------możesz to wykorzystać do testów continous tests------------
        // char[] tmpCheck = new char[3];
        // tmpCheck[0] = movementSequence[0];
        // tmpCheck[1] = movementSequence[1];
        // tmpCheck[2] = movementSequence[2];
        // string tmpCheckString = new string(tmpCheck);
        // if(tmpCheckString == "aaa")
        if(movementSequenceString == "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")
        {
            Debug.Log("Brak możliwości rozwiązania");
            finishedSearching = true;
            ChangeTestingText(true);
            NeedToContinueTesting(solvingContonously);
            return;
        }
        // bool nextOneToChange = false;
        // char i;
        move = 0;
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
        // Debug.Log("Sprawdzam dla: " + movementSequenceString);
        Debug.Log("Check: "+movementSequenceString);
        //Resetowanie poziomu

        LevelTestGeneratorScript.Purge();
        TurnCounterScript.turnsLeft = 40;
        // LevelTestGeneratorScript.Start();
        LevelTestGeneratorScript.InitializeLevelObjects();
        // Destroy(LevelTestGenerator);
        // LevelTestGenerator = Instantiate(LevelTestGeneratorObject);
        // TurnCounterScript.Start();
        // LevelTestGeneratorScript = LevelTestGenerator.GetComponent<LevelTestGeneratorScript>();

    
        Player =  LevelTestGeneratorScript.players[0];
        PlayerTesting = Player.GetComponent<PlayerTesting>();

        
        canGetNewMove = true;


    }

    public void SolutionFound(int turnsToFinish)
    {
        levelIsSolvable = true;
        minTurnsToFinish = turnsToFinish;
        minMoves = move;

        if(minTurnsToFinish <= bestSolutionTurns)
        {
            bestSolutionTurns = minTurnsToFinish;
            bestSolutionMoves = minMoves;
            char[] bestSolutionSequence = new char[minMoves];
            for(int s = 0; s < minMoves; s++ )
            {
                bestSolutionSequence[s] = movementSequence[s];
            }
            bestSolutionSequenceString = new string(bestSolutionSequence);
        } 

        
        Debug.Log("Turns: " + turnsToFinish);
        Debug.Log("minMoves: " + minMoves);
        char[] solutionSequence = new char[minMoves];
        for(int s = 0; s < minMoves; s++ )
        {
            solutionSequence[s] = movementSequence[s];
        }

        solutionSequenceString = new string(solutionSequence);
        Debug.Log("Nowa solucja: " + solutionSequenceString);
        
        ChangeSequence();
        // Debug.Log(movementSequenceString);
    }

    public void AllPossibilitiesChecked()
    {
        finishedSearching = true;        
        ChangeTestingText(true);

        if(levelIsSolvable)
        {
            Debug.Log("------------------------");
            Debug.Log("ALL POSIBILITIES CHECKED");
            Debug.Log("SEED:");
            Debug.Log(LevelTestGeneratorScript.seed);
            Debug.Log("First unchanged character: " + highestChanged);
            Debug.Log("Minimum number of moves to solve: " + minMoves);
            Debug.Log("BEST SOLUTION:");
            Debug.Log(bestSolutionSequenceString);
            RequiredTurns.text = minTurnsToFinish.ToString();
            RequiredMoves.text = minMoves.ToString();
            SolutionToCopy.text = solutionSequenceString;
        }
        else
        {
            Debug.Log("------------------------");
            Debug.Log("ALL POSIBILITIES CHECKED");
            Debug.Log("SEED:");
            Debug.Log(LevelTestGeneratorScript.seed);
            Debug.Log("No solution found within 2 turns");
            NeedToContinueTesting(solvingContonously);
        }
        
    }

    public void ChangeTestingText(bool done)
    {
        if(done == false)
        {
            testingTextCount++;
            if(testingTextCount == 4)
            {
                testingTextCount = 1;
            }
            if(testingTextCount == 1)
            {
                TestingStatus.text = "IN PROGRESS.";
            }
            else if(testingTextCount == 2)
            {
                TestingStatus.text = "IN PROGRESS..";
            }
            else if(testingTextCount == 3)
            {
                TestingStatus.text = "IN PROGRESS...";
            }
        }

        else if(done == true)
        {
            TestingStatus.text = "DONE";
        }
    }

    public void NeedToContinueTesting(bool continueTests)
    {
        if(continueTests)
        {
            GameObject ContinousTester = GameObject.Find("ObjectIndicatingContinousTests");
            if(ContinousTester != null)
            {
                if(ContinousTester.GetComponent<ContinousTests>().testContinous == true)
                {
                    ContinousTester.GetComponent<ContinousTests>().RetryforContinuity();
                }
            }
        }
    }
    
}
