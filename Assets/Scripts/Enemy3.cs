using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public Estados3 estado;
    public float patrolSpeed = 0f;
    public float canhgeTargetD = 0.1f;
    private bool mover = true;
    private bool canDie = false;
    public bool flip = false;
    public float radio = 3f;
    public int contE2 = 0;
    public int iContE2 = 1;
    public LayerMask capaParceros;
    public Transform objetivoSeguir;
    private Animator animE3;
    private SoundManager soundManager;
    int currentTarget = 0;

    private void Start()
    {
        animE3 = this.GetComponent<Animator>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Update()
    {
        ControlEstados();
    }


    private void ControlEstados()
    {
        switch (estado)
        {
            case Estados3.agro:
                EstadoAgro();
                break;
            case Estados3.patrol:
                EstadoPatrol();
                break;
            case Estados3.muerto:
                EstadoMuerto();
                break;
            default:
                break;
        }
    }

    private void CambiarEstado(Estados3 ne)
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
            CambiarEstado(Estados3.patrol);
            Destroy(objetivoSeguir.gameObject);
        }
        else if (canDie == true)
        {
            CambiarEstado(Estados3.muerto);
        }
        else
        {
            Vector3 velocityVector = distanceVector.normalized;
            transform.position += velocityVector * patrolSpeed * Time.deltaTime;
        }


    }

    private void EstadoMuerto()
    {
        mover = false;
        animE3.SetTrigger("Muerto");
        canDie = true;
        soundManager.SeleccionAudio(3);
        Destroy(this.gameObject, 2.0f);
        CambiarEstado(Estados3.patrol);
        StartCoroutine(VolverdeMuerto());

    }
    IEnumerator VolverdeMuerto()
    {
        if (!mover && canDie)
        {
            yield return new WaitForSeconds(2.0f);
            mover = true;
            canDie = false;
        }
    }



    private bool MoveToTarget()
    {
        Vector3 distanceVector = Qteszcohatl.singleton.enemyPoints[currentTarget].position - transform.position;
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
            currentTarget = Random.Range(0, Qteszcohatl.singleton.enemyPoints.Length);
            Vector2 posicion = new Vector2(transform.position.x, transform.position.y);
            Vector2 direcion = (new Vector2(Qteszcohatl.singleton.enemyPoints[currentTarget].position.x, Qteszcohatl.singleton.enemyPoints[currentTarget].position.y)) - posicion;
            posicion = posicion + direcion.normalized * 1.2f;
            RaycastHit2D hit = Physics2D.Raycast(posicion, direcion, direcion.magnitude);

            if (direcion.x < transform.position.x)
            {
                flip = true;
                print("izqui");
            }
            else if (direcion.x > transform.position.x)
            {
                flip = false;
                print("dere");
            }
            
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
        animE3.SetBool("Flip", flip);
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
            yield return new WaitForSeconds(0f);
            mover = true;
        }
    }

    void DetectarParceros()
    {
        Vector2 posicion = new Vector2(transform.position.x, transform.position.y);
        Collider2D col = Physics2D.OverlapCircle(posicion, radio, capaParceros);
        if (col != null && col.CompareTag("parcero"))
        {
            objetivoSeguir = col.transform;
            CambiarEstado(Estados3.agro);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Linterna"))
        {
            CambiarEstado(Estados3.muerto);
            
        }
    }

}

[System.Serializable]
public enum Estados3
{
    agro,
    patrol,
    muerto
}

