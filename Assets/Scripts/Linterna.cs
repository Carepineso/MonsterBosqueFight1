using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    public Vector3 posMouse;
    public float angulo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        posMouse = Input.mousePosition - new Vector3(Screen.width/2,Screen.height/2);
        angulo = Mathf.Atan(posMouse.y/posMouse.x)*Mathf.Rad2Deg;
        if (posMouse.x<0)
        {
            angulo+=180;
        }
        transform.eulerAngles = Vector3.forward * angulo;
    }
}
