using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnCounterScript : MonoBehaviour
{
    [SerializeField] public int movesLeft;
    [SerializeField] public int movesMade = 0;
    [SerializeField] public int turnsMade = 0;
    [SerializeField] public TMP_Text Turns;
    [SerializeField] public TMP_Text movesMadeText;
    [SerializeField] public TMP_Text turnsMadeText;

    [SerializeField] public TMP_Text declaredWinMoves;
    [SerializeField] public TMP_Text declaredWinTurns;


    [SerializeField] GameObject LevelGeneratorObject;
    [SerializeField] LevelGeneratorScript LevelGeneratorScript;
    [SerializeField] public List<GameObject> fields;
    void Start()
    {
        movesLeft = 40;
        movesMade = 0;
        turnsMade = 0;
        LevelGeneratorObject = GameObject.Find("LevelGeneratorObject");
        LevelGeneratorScript = LevelGeneratorObject.GetComponent<LevelGeneratorScript>();
        fields = LevelGeneratorScript.fields;

    }

    public void TurnDown()
    {
        movesLeft--;
        movesMade++;
        turnsMade++;
        Turns.text = movesLeft.ToString();
        movesMadeText.text = movesMade.ToString();
        turnsMadeText.text = turnsMade.ToString();
        fields.TrimExcess();
        foreach(GameObject field in fields)
        {
            ElementTypeInterface ElementTypeInterface = field.GetComponent<ElementTypeInterface>();
            if(ElementTypeInterface.isChangableSpike){ElementTypeInterface.ChangeSpikeState();}
        }

        if(movesLeft < 0)
        {
            Debug.Log("Przegrałeś");
        }
    }

    public void SpikeTurnLoss()
    {
        movesLeft--;
        turnsMade++;
        turnsMadeText.text = turnsMade.ToString();
        Turns.text = movesLeft.ToString();
        if(movesLeft < 0)
        {
            Debug.Log("Przegrałeś");
        }
    }

    public void DeclareWin()
    {
        declaredWinMoves.text = "Ukończono poziom w " + movesMade + " ruchach";
        declaredWinTurns.text = "Ukończono poziom w " + turnsMade + " turach";
    }

    

}
