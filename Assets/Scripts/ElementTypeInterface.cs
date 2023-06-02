using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementTypeInterface : MonoBehaviour
{
    [SerializeField] public bool isBeginningField;
    [SerializeField] public bool isField;
    [SerializeField] public bool isWall;
    [SerializeField] public bool isRock;
    [SerializeField] public bool isSpike;
    [SerializeField] public bool isEnemy;
    [SerializeField] public bool isKey;
    [SerializeField] public bool isDoor;


    [SerializeField] public bool isTakenByRock;
    [SerializeField] public bool isTakenByEnemy;
    [SerializeField] public bool isSpikeActive;
    [SerializeField] public bool isHoldingKey;



    public void ChangeSpikeState()
    {
        if(isSpikeActive == true)
        {
            DeactivateSpike();
        }
        else if(isSpikeActive == false)
        {
            ActivateSpike();
        }
    }
    public void ActivateSpike()
    {
        isSpikeActive = true;
        // this.gameObject.transform.rotation.z  = new Vector3(0, 0, 90f);
        //Jak się robiło te głupie obracanie?
        //Ugh

        transform.Rotate(0, 0, 45, Space.World);
    }
    public void DeactivateSpike()
    {
        isSpikeActive = false;
        transform.Rotate(0, 0, -45, Space.World);
        // this.gameObject.transform.rotation = new Vector3(0, 0, 0);
    }
}
