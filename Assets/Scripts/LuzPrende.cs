using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzPrende : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D luz;

    public OnOff onOff;
    // Start is called before the first frame update
    void Start()
    {
        luz = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (onOff.prendida == false)
        {
            luz.enabled = !luz.enabled;
        }
        else
        {
            luz.enabled = luz.enabled;
        }*/
        
    }
}
