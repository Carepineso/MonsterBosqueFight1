using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Estados estado;
    public float patrolSpeed = 0f;
    public float canhgeTargetD = 0.1f;
    private bool mover = true;
    private bool canDie = false;
    public bool invensible = false;
    public bool flip = false;
    public float radio = 3f;
    public int contE1 = 0;
    public int iContE1 = 1;
    public LayerMask capaParceros;
    public Transform objetivoSeguir;
    private Animator animA;
    public GameObject puffAmi;
    public GameObject puffEne;
    private SoundManager soundManager;

    int currentTarget = 0;

    private void Start()
    {
        animA = this.GetComponent<Animator>();
        soundManager = FindObjectOfType<SoundManager>();
        GetNextTarget();

    }

    void Update()
    {
        ControlEstados();
    }

    private void ControlEstados()
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

    private void CambiarEstado(Estados ne)
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
            CambiarEstado(Estados.patrol);
            
            Destroy(objetivoSeguir.gameObject);
            Instantiate(puffAmi, transform.position, transform.rotation);
        }
        else if (canDie == true)
        {
            CambiarEstado(Estados.muerto);
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
        canDie = true;
        animA.SetTrigger("AMuerto");
        soundManager.SeleccionAudio(1);
        Instantiate(puffEne, transform.position, transform.rotation);
        soundManager.SeleccionAudio(4);
        Destroy(this.gameObject, 2.0f);
        CambiarEstado(Estados.patrol);
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
            StartCoroutine(ADespertarse());
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
        animA.SetBool("AFlip", flip);
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
            invensible = true;
            animA.SetTrigger("ADormir");
            yield return new WaitForSeconds(5.0f);
            mover = true;
            invensible = false;
        }
    }

    IEnumerator ADespertarse()
    {
        yield return new WaitForSeconds(3.0f);
        animA.SetTrigger("ADespertarse");
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Linterna")&& !invensible)
        {
            CambiarEstado(Estados.muerto);
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

