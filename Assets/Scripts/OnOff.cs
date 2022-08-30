using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnOff : MonoBehaviour
{
    public Collider2D collidosde;
    public Slider slider;
    public float stamina = 100f;
    public float maxstamina = 100f;
    public bool prendida= true;
    // Start is called before the first frame update
    void Start()
    {
        collidosde = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Estamina();

        slider.value = stamina;
    }

    void Estamina()
    {
        if (Input.GetButtonDown("Jump") && prendida) 
        {
            OnOff2();
        }

        if (collidosde.enabled && stamina>0)
        {
            stamina -= 5.0f*Time.deltaTime;
        }
        else if (!collidosde.enabled && stamina<maxstamina)
        {
            stamina += 3.0f * Time.deltaTime;
        } else if (stamina<=0f && collidosde.enabled)
        {
            prendida=false;
            collidosde.enabled = !collidosde.enabled;
            StartCoroutine(Prender());
        } 
    }

    public void OnOff2()
    {
        collidosde.enabled = !collidosde.enabled;
    }

    IEnumerator Prender()
    {
        yield return new WaitForSeconds(5F);
        prendida=true;
    }

}
