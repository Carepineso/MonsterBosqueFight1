using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
     public Transform[] spawnPaints;
    public GameObject prefabParcero;
    private int random= 0;
    // Start is called before the first frame update
    void Start()
    {
        SpawnParceros();
    }

    // Update is called once per frame
    void SpawnParceros(){
        random= Random.Range(0, spawnPaints.Length);
        GameObject gameObject= Instantiate<GameObject>(prefabParcero, spawnPaints[random].position, Quaternion.identity);
        
    }
}
