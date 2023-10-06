using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnTestCounterScript : MonoBehaviour
{
    
    [SerializeField] public int turnsLeft;
    [SerializeField] public TMP_Text Turns;
    [SerializeField] GameObject LevelGeneratorObject;
    [SerializeField] LevelTestGeneratorScript LevelGeneratorScript;
    [SerializeField] public List<GameObject> fields;
    [SerializeField] GameObject LevelTestSupervisorObject;
    [SerializeField] public TestSupervisingScript TestSupervisingScript;

    public int bestSolutionTurns = 40;
    public void Start()
    {
        turnsLeft = 40;
        LevelGeneratorObject = GameObject.Find("LevelTestGeneratorObject");
        LevelGeneratorScript = LevelGeneratorObject.GetComponent<LevelTestGeneratorScript>();
        fields = LevelGeneratorScript.fields;
        
        LevelTestSupervisorObject = GameObject.Find("LevelTestSupervisorObject");
        if(LevelTestSupervisorObject != null)
        {
            TestSupervisingScript = LevelTestSupervisorObject.GetComponent<TestSupervisingScript>();
        }
    }

    public void TurnDown()
    {
        turnsLeft--;
        // Turns.text = movesLeft.ToString();
        fields.TrimExcess();
        foreach(GameObject field in fields)
        {
            ElementTestTypeInterface ElementTypeInterface = field.GetComponent<ElementTestTypeInterface>();
            if(ElementTypeInterface.isChangableSpike){ElementTypeInterface.ChangeSpikeState();}
        }

        if(40-turnsLeft > bestSolutionTurns)
        {
            if(LevelGeneratorObject != null)
            {
                TestSupervisingScript.ChangeSequence();
            }
        }

        if(turnsLeft < 0)
        {
            Debug.Log("All moves used");
        }
    }

    public void SpikeTurnLoss()
    {
        turnsLeft--;
        // Turns.text = movesLeft.ToString();
        if(turnsLeft < 0)
        {
            Debug.Log("All moves used");
        }
    }

    public int GetTurns()
    {
        return 40-turnsLeft;
    }

}
