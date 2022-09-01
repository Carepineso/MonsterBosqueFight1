using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class amigo_1 : MonoBehaviour
{
    public float patrolSpeed = 0f;
    public float canhgeTargetD = 0.1f;
    public bool flip = false;

    public Animator animAmigo;

    int currentTarget = 0;

    private void Start()
    {
        animAmigo = this.GetComponent<Animator>();
    }
    void Update()
    {
        if(MoveToTarget())
        {
            currentTarget = GetNextTarget();
            
        }
    }

    private void FixedUpdate()
    {
        animAmigo.SetBool("Flip", flip);
    }

    private bool MoveToTarget()
    {
        Vector3 distanceVector = Qteszcohatl.singleton.patrolPoints[currentTarget].position - transform.position;
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
            currentTarget = Random.Range(0, Qteszcohatl.singleton.patrolPoints.Length);
            Vector2 posicion = new Vector2(transform.position.x, transform.position.y);
            Vector2 direcion = (new Vector2(Qteszcohatl.singleton.patrolPoints[currentTarget].position.x, Qteszcohatl.singleton.patrolPoints[currentTarget].position.y)) - posicion;
            
            posicion = posicion + direcion.normalized * 1.5f;
            RaycastHit2D hit = Physics2D.Raycast(posicion, direcion, direcion.magnitude);
            Debug.DrawRay(posicion, direcion, Color.green);

            if (direcion.x < transform.position.x)
            {
                flip = false;
                print("izqui");
            }
            else if (direcion.x > transform.position.x)
            {
                flip = true;
                print("dere");
            }

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.DrawRay(posicion, direcion, Color.red);
                    repetir = true;
                }
                else
                {
                    Debug.DrawRay(posicion, direcion, Color.green);
                }
            }
        } while (repetir);

        return currentTarget;
        
    }

}
