using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBodyWithLegs : MonoBehaviour
{
    public Transform[] legPositions;
    public float heightOffGround;

    // Update is called once per frame
    void LateUpdate()
    {
        if (legPositions.Length > 0)
        {
            float sumLegPosY = 0f;
            foreach (Transform legPos in legPositions)
            {
                sumLegPosY += legPos.position.y;
            }
            float avgLegPosY = sumLegPosY / legPositions.Length;
            
            transform.position = new Vector3(transform.position.x, avgLegPosY + heightOffGround, transform.position.z);
        }
    }
}
