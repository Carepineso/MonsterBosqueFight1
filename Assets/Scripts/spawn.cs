using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public Transform[] spawnPaints;
    public GameObject prefabParcero;
    private int random= 0;
    public bool can_sp = true;
    // Start is called before the first frame update
    void Start()
    {
         StartCoroutine(TSpawn());
    }

    IEnumerator TSpawn()
    {

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4.0f, 14.0f));
            random = Random.Range(0, spawnPaints.Length);
            GameObject gameObject = Instantiate<GameObject>(prefabParcero, spawnPaints[random].position, Quaternion.identity);
        }
    }
}
