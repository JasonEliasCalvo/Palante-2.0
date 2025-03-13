using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MovableObject
{
    protected override void Move()
    {
        if (platformOrder && nextPlatform + 1 >= movePoints.Length)
            platformOrder = false;
        if (!platformOrder && nextPlatform <= 0)
            platformOrder = true;

        if (Vector3.Distance(transform.position, movePoints[nextPlatform].position) < 0.001f)
        {
            nextPlatform = platformOrder ? nextPlatform + 1 : nextPlatform - 1;
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoints[nextPlatform].position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
