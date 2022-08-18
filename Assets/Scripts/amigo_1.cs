using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amigo_1 : MonoBehaviour
{
    public float patrolSpeed = 0f;
    public float canhgeTargetD = 0.1f;
    public Transform[] patrolPoints;

    int currentTarget = 0;

    void Update()
    {
        if(MoveToTarget())
        {
            currentTarget = GetNextTarget();
        }
    }

    private bool MoveToTarget()
    {
        Vector3 distanceVector =  patrolPoints[currentTarget].position - transform.position;
        if(distanceVector.magnitude < canhgeTargetD)
        {
            return true;
        }

        Vector3 velocityVector = distanceVector.normalized;
        transform.position += velocityVector * patrolSpeed * Time.deltaTime;

        return false;
    }
    private int GetNextTarget()
    {

        bool repetir = false;
        do
        {
            repetir = false;
            currentTarget = Random.Range(0,patrolPoints.Length);
            Vector2 posicion = new Vector2(transform.position.x, transform.position.y);
            Vector2 direcion = (new Vector2(patrolPoints[currentTarget].position.x, patrolPoints[currentTarget].position.y)) - posicion;
            
            posicion = posicion + direcion.normalized * 1.2f;
            RaycastHit2D hit = Physics2D.Raycast(posicion, direcion, direcion.magnitude);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    repetir = true;
                }
            }
        } while (repetir);

        return currentTarget;
        
    }
}
