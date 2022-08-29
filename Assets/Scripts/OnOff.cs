using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    public Collider2D collidosde;
    public float stamina = 100f;
    public float maxstamina = 100f;
    // Start is called before the first frame update
    void Start()
    {
        collidosde = GetComponent<PolygonCollider2D>();
        collidosde.enabled = !collidosde.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        Estamina();
    }

    void Estamina()
    {
        while (Input.GetKeyDown(KeyCode.Space))
        {
            collidosde.enabled = !collidosde.enabled;
            if (stamina > 0)
            {
                stamina -= 5f * Time.deltaTime;
            }

        }
        //stamina += 2f * Time.deltaTime;
    }
}
