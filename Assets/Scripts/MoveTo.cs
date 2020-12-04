using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public Transform target;
    public float maxDistanceToTarget = 2f;
    public float speed = 4f;
    public float legRaiseAmount = 3f;

    IEnumerator currentMoveCoroutine;
    float initialDistanceToTarget = -1f;

    Vector3 currentDestination;

    void FixedUpdate()
    {
        if ((target.position - transform.position).sqrMagnitude > maxDistanceToTarget * maxDistanceToTarget)
        {
            if (initialDistanceToTarget == -1f)
            {
                initialDistanceToTarget = (target.position - transform.position).sqrMagnitude;
                currentDestination = target.position;
            }

            if (currentMoveCoroutine != null)
            {
                StopCoroutine(currentMoveCoroutine);
            }

            currentMoveCoroutine = MoveToTarget(currentDestination);
            
            StartCoroutine(currentMoveCoroutine);
        }
    }

    IEnumerator MoveToTarget(Vector3 destination)
    {
        while(transform.position != destination)
        {
            float currentDistanceToDestination = (destination - transform.position).sqrMagnitude;
            float stepCompletion = currentDistanceToDestination / initialDistanceToTarget;
            Vector3 legRaiseVector = new Vector3(0, stepCompletion/2 * legRaiseAmount, 0);
            
            transform.position = Vector3.MoveTowards(transform.position, destination + legRaiseVector, speed * Time.deltaTime);

            if (transform.position == destination)
            {
                initialDistanceToTarget = -1f;
            }

            yield return null;
        }
    }
}
