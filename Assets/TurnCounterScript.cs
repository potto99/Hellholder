using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnCounterScript : MonoBehaviour
{
    [SerializeField] int movesLeft;
    [SerializeField] public TMP_Text Turns;
    [SerializeField] GameObject LevelGeneratorObject;
    [SerializeField] LevelGeneratorScript LevelGeneratorScript;
    [SerializeField] public List<GameObject> fields;
    void Start()
    {
        movesLeft = 40;
        LevelGeneratorObject = GameObject.Find("LevelGeneratorObject");
        LevelGeneratorScript = LevelGeneratorObject.GetComponent<LevelGeneratorScript>();
        fields = LevelGeneratorScript.fields;

    }

    public void TurnDown()
    {
        movesLeft--;
        Turns.text = movesLeft.ToString();
        fields.TrimExcess();
        foreach(GameObject field in fields)
        {
            ElementTypeInterface ElementTypeInterface = field.GetComponent<ElementTypeInterface>();
            if(ElementTypeInterface.isChangableSpike){ElementTypeInterface.ChangeSpikeState();}
        }
    }

    public void SpikeTurnLoss()
    {
        movesLeft--;
        Turns.text = movesLeft.ToString();
    }

}
