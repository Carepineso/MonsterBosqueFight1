using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float patrolSpeed = 0f;
    public float canhgeTargetD = 0.1f;
    public Transform[] enemyPoints;
    private bool mover = true;

    int currentTarget = 0;

    void Update()
    {
        if (MoveToTarget())
        {
            currentTarget = GetNextTarget();
        }
    }

    private bool MoveToTarget()
    {
        Vector3 distanceVector = enemyPoints[currentTarget].position - transform.position;
        if (distanceVector.magnitude < canhgeTargetD)
        {
            return true;
        }
        if (mover)
        {
            Vector3 velocityVector = distanceVector.normalized;
            transform.position += velocityVector * patrolSpeed * Time.deltaTime;
        }

        

        return false;
    }
    private int GetNextTarget()
    {

        bool repetir = false;
        do
        {
            mover = false;
            StartCoroutine(Quieto());
            repetir = false;
            currentTarget = Random.Range(0, enemyPoints.Length);
            Vector2 posicion = new Vector2(transform.position.x, transform.position.y);
            Vector2 direcion = (new Vector2(enemyPoints[currentTarget].position.x, enemyPoints[currentTarget].position.y)) - posicion;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direcion, direcion.magnitude);

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

    IEnumerator Quieto()
    {
        while (!mover)
        {
            yield return new WaitForSeconds(3.0f);
            mover = true;
        }
    }
}
