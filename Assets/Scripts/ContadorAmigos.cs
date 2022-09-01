using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorAmigos : MonoBehaviour
{
    public int cont = 0;
    public int contM = 3;
    public TMP_Text pp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string sCont = cont.ToString();
        pp.text = sCont;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("parcero"))
        {
            cont++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("parcero"))
        {
            contM--;
        }
    }
}
