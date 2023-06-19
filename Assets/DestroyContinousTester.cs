using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyContinousTester : MonoBehaviour
{
    
    void Start()
    {
        GameObject ContinousTester;
        ContinousTester = GameObject.Find("ObjectIndicatingContinousTests");
        if(ContinousTester != null)
        {
            Destroy(ContinousTester);
        }
    }

   
}
