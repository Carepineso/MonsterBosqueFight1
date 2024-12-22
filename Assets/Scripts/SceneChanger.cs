using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public ContadorAmigos contadorAmigos;
    
    // Update is called once per frame
    void Update()
    {
      if (contadorAmigos.contM==0)
        {
            SceneManager.LoadScene("Fin");
        }
    }

}
