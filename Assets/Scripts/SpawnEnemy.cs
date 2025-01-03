using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform[] spawnPaints;
    public GameObject[] prefabEnemy;
    private int random = 0;
    private int enemigos = 0;
    public bool can_sp = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InicioJuego()
    {
        
        StartCoroutine(TSpawn());
    }

    IEnumerator TSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4.0f, 14.0f));
            random = Random.Range(0, spawnPaints.Length);
            enemigos = Random.Range(0, prefabEnemy.Length);
            GameObject gameObject = Instantiate<GameObject>(prefabEnemy[enemigos], spawnPaints[random].position, Quaternion.identity);
        }
    }
}

