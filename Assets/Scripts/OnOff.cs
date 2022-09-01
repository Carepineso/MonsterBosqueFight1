using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnOff : MonoBehaviour
{
    public Collider2D collidosde;
    public Slider slider;
    public UnityEngine.Rendering.Universal.Light2D luz;
    public float stamina = 100f;
    public float maxstamina = 100f;
    public bool prendida = true;
    private SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        collidosde = GetComponent<PolygonCollider2D>();
        luz = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        soundManager=FindObjectOfType<SoundManager>();
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
            luz.enabled = !luz.enabled;
            StartCoroutine(Prender());
        } 
    }

    public void OnOff2()
    {
        soundManager.SeleccionAudio(0);
        collidosde.enabled = !collidosde.enabled;
        luz.enabled = !luz.enabled;
    }

    IEnumerator Prender()
    {
        yield return new WaitForSeconds(5F);
        prendida=true;
    }

}
