using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevolvedorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Regresar()
    {
        SceneManager.LoadScene("Landscape");
    }
    public void Salir()
    {
        SceneManager.LoadScene("Interfaz");
    }
}
