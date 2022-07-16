using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
     MeshRenderer render;
     Rigidbody rb;
    [SerializeField] float timeToWait = 5f;
    // Start is called before the first frame update
    void Start()
    {

        render = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();

        render.enabled = false;
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeToWait)
        {
            render.enabled = true;
            rb.useGravity = true;
        }
    }
}
