using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qteszcohatl : MonoBehaviour
{
    public Transform[] enemyPoints;
    public Transform[] patrolPoints;
    public static Qteszcohatl singleton;

    void Awake()
    {
        singleton = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
