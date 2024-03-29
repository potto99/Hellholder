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
    [SerializeField] public TMP_Text TimeOfTest;
    int testingTextCount = 1;
    char[] movementSequence = new char[] {'d','d','d','d','d','d','d','d','d','d','d','d','d','d','d',
        'd','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d','d',
        'd','d','d','d','d'};

    
    
    public double TestingTime = 0;
    public int move = 0;
    public int minMoves = 40;
    public int minTurnsToFinish = 40;
    public int minTurnsAllowedToFinish = 0;
    public int bestSolutionMoves = 40;
    public int bestSolutionTurns = 40;
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
    public bool finishedSearching = false;
    public bool levelIsSolvable = false;
    public bool solvingContonously = false;
    string solutionSequenceString = null;
    string bestSolutionSequenceString = null;
    char character = '0';

    void Start()
    {
        
        LevelTestGeneratorObject = GameObject.Find("LevelTestGeneratorObject");
        LevelTestGeneratorScript = LevelTestGeneratorObject.GetComponent<LevelTestGeneratorScript>();
        SeedToCopy.text = LevelTestGeneratorScript.seed;
        minTurnsAllowedToFinish = LevelTestGeneratorScript.MinMoves;
        minTurnsToFinish = LevelTestGeneratorScript.MaxMoves;

        TurnCounter = GameObject.Find("TurnCounter");
        TurnCounterScript = TurnCounter.GetComponent<TurnTestCounterScript>();

        Player = LevelTestGeneratorScript.players[0];
        PlayerTesting = Player.GetComponent<PlayerTesting>();

        GameObject ContinousTester = GameObject.Find("ObjectIndicatingContinousTests");
        if(ContinousTester != null)
        {
            if(ContinousTester.GetComponent<ContinousTests>().testContinous == true)
            {
                solvingContonously = true;
                TestingTime = ContinousTester.GetComponent<ContinousTests>().TestingTime + Time.deltaTime;
            }

        }
    }

    void FixedUpdate()
    {
        if(canGetNewMove && finishedSearching == false && Player!=null)
        {
            TestingTime += (double) Time.unscaledDeltaTime;
            TimeOfTest.text = TestingTime.ToString();

            character = movementSequence[move];
            if(character == 'd'){PlayerTesting.GoRight(); canGetNewMove = false;}
            else if(character == 'w'){PlayerTesting.GoUp(); canGetNewMove = false;}
            else if(character == 's'){PlayerTesting.GoDown(); canGetNewMove = false;}
            else if(character == 'a'){PlayerTesting.GoLeft(); canGetNewMove = false;}
            move++;
            if(move > minTurnsToFinish){ChangeSequence();}
        }
    }

    public void ChangeSequence()
    {
        ChangeTestingText(false);
        
        string movementSequenceString = new string(movementSequence);
 
        if(movementSequenceString == "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")
        {
            Debug.Log("Brak możliwości rozwiązania");
            finishedSearching = true;
            ChangeTestingText(true);
            NeedToContinueTesting(solvingContonously);
            return;
        }

        move = 0;
        
        for(int i = 0; i<40; i++)
        {
            if(movementSequence[i] == 'd')
            {
                movementSequence[i] = 'w';
                if(i > highestChanged){highestChanged = i;}
                break;
            }
            else if(movementSequence[i] == 'w')
            {
                movementSequence[i] = 's';
                if(i > highestChanged){highestChanged = i;}
                break;
            }
            else if(movementSequence[i] == 's')
            {
                movementSequence[i] = 'a';
                if(i > highestChanged){highestChanged = i;}
                break;
            }
            else if(movementSequence[i] == 'a')
            {
                movementSequence[i] = 'd'; 
            }
        }

        if(highestChanged >= minTurnsToFinish)
            {
                AllPossibilitiesChecked();
                canGetNewMove = false;
                return;
            }
        
        movementSequenceString = new string(movementSequence);
        Debug.Log("Check: "+movementSequenceString);
        //Resetowanie poziomu

        LevelTestGeneratorScript.Purge();
        TurnCounterScript.turnsLeft = 40;
        LevelTestGeneratorScript.InitializeLevelObjects();

        Player =  LevelTestGeneratorScript.players[0];
        PlayerTesting = Player.GetComponent<PlayerTesting>();

        canGetNewMove = true;
    }

    public void SolutionFound(int turnsToFinish)
    {
         if(turnsToFinish < minTurnsAllowedToFinish)
            {
                AllPossibilitiesChecked();
                canGetNewMove = false;
                return;
            }

        levelIsSolvable = true;
        minTurnsToFinish = turnsToFinish;
        minMoves = move;
        
        if(minTurnsToFinish <= bestSolutionTurns)
        {
            bestSolutionTurns = minTurnsToFinish;
            bestSolutionMoves = minMoves;
            TurnCounterScript.bestSolutionTurns = bestSolutionTurns;
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
    }

    public void AllPossibilitiesChecked()
    {
        finishedSearching = true;        
        ChangeTestingText(true);

        if(levelIsSolvable)
        {
            Camera.main.backgroundColor = Camera.main.backgroundColor = new Color32(29, 135, 13, 255);  
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
            Camera.main.backgroundColor = new Color32(207, 22, 19, 255);  
            Debug.Log("------------------------");
            Debug.Log("ALL POSIBILITIES CHECKED");
            Debug.Log("SEED:");
            Debug.Log(LevelTestGeneratorScript.seed);
            Debug.Log("No solution found within " + minTurnsAllowedToFinish + " to " + minTurnsToFinish + " turns");
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
                    ContinousTester.GetComponent<ContinousTests>().TestingTime = TestingTime;
                    ContinousTester.GetComponent<ContinousTests>().RetryforContinuity();
                }
            }
        }
    }
    
}
