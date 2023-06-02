using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCoordinates : MonoBehaviour
{
    [SerializeField] public int TableNumberX;
    [SerializeField] public int TableNumberY;

    [SerializeField] public int positionX;
    [SerializeField] public int positionY;
    
    public void placeOnLevel()
    {
        positionX = TableNumberX * 8;
        positionY = TableNumberY * 8;
        this.gameObject.transform.position = new Vector2(positionX, positionY);
    }
}
