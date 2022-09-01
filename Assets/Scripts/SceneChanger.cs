using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneChanger : MonoBehaviour
{
    public ContadorAmigos contadorAmigos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
    }

    public void GameScene()
    {
        SceneManager.LoadScene("Landscape");
    }

    public void GameOver()
    {
        if (contadorAmigos.contM==0)
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
