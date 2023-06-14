using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnTestCounterScript : MonoBehaviour
{
    [SerializeField] public int movesLeft;
    [SerializeField] public TMP_Text Turns;
    [SerializeField] GameObject LevelGeneratorObject;
    [SerializeField] LevelTestGeneratorScript LevelGeneratorScript;
    [SerializeField] public List<GameObject> fields;
    void Start()
    {
        movesLeft = 40;
        LevelGeneratorObject = GameObject.Find("LevelTestGeneratorObject");
        LevelGeneratorScript = LevelGeneratorObject.GetComponent<LevelTestGeneratorScript>();
        fields = LevelGeneratorScript.fields;

    }

    public void TurnDown()
    {
        movesLeft--;
        // Turns.text = movesLeft.ToString();
        fields.TrimExcess();
        foreach(GameObject field in fields)
        {
            ElementTestTypeInterface ElementTypeInterface = field.GetComponent<ElementTestTypeInterface>();
            if(ElementTypeInterface.isChangableSpike){ElementTypeInterface.ChangeSpikeState();}
        }
    }

    public void SpikeTurnLoss()
    {
        movesLeft--;
        // Turns.text = movesLeft.ToString();
        if(movesLeft < 0)
        {
            Debug.Log("Przegrałeś");
        }
    }

}
