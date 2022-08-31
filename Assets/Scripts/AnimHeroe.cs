using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHeroe : MonoBehaviour
{
    private Animator animHeroe;
    public Linterna linterna;
    // Start is called before the first frame update
    void Start()
    {
        animHeroe = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animHeroe.SetFloat("MiraFloat", linterna.angulo);
    }
}
