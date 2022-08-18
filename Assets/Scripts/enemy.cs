using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Estados estado;
    public float patrolSpeed = 0f;
    public float canhgeTargetD = 0.1f;
    public Transform[] enemyPoints;
    private bool mover = true;
    public float radio = 3f;
    public LayerMask capaParceros;
    public Transform objetivoSeguir;

    int currentTarget = 0;

    void Update()
    {
        ControlEstados();
    }

    public void ControlEstados()
    {
        switch (estado)
        {
            case Estados.agro:
                EstadoAgro();
                break;
            case Estados.patrol:
                EstadoPatrol();
                break;
            case Estados.muerto:
                EstadoMuerto();
                break;
            default:
                break;
        }
    }

    void CambiarEstado(Estados ne)
    {
        estado = ne;
    }

    void EstadoPatrol()
    {
       if (MoveToTarget())
        {
            currentTarget = GetNextTarget();
        }
    }

    void EstadoAgro()
    {
        Vector3 distanceVector = objetivoSeguir.position - transform.position;
        if (distanceVector.magnitude < canhgeTargetD)
        {
            //mato al objetivo
            print("MATÉ A UN OBJETIVO");
            CambiarEstado(Estados.patrol);
            Destroy(objetivoSeguir.gameObject);
        }
        else
        {
            Vector3 velocityVector = distanceVector.normalized;
            transform.position += velocityVector * patrolSpeed * Time.deltaTime;
        }
    }

    void EstadoMuerto()
    {

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

    private void FixedUpdate()
    {
        DetectarParceros();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }

    IEnumerator Quieto()
    {
        while (!mover)
        {
            yield return new WaitForSeconds(3.0f);
            mover = true;
        }
    }

    void DetectarParceros()
    {
        Vector2 posicion = new Vector2(transform.position.x, transform.position.y);
        Collider2D col = Physics2D.OverlapCircle(posicion, radio, capaParceros);
        if (col!=null && col.CompareTag("parcero"))
        {
            objetivoSeguir = col.transform;
            CambiarEstado(Estados.agro);
        }
    }
}

[System.Serializable]
public enum Estados
{
    agro,
    patrol,
    muerto
}