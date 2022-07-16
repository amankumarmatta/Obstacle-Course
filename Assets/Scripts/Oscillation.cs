using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 moveVector;
    float moveFactor;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; //Grows overtime

        const float tau = Mathf.PI * 2; //Constant Value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau);

        moveFactor = rawSinWave + 1f / 2f; //Recalculated to move from 0 to 1

        Vector3 offset = moveVector * moveFactor;
        transform.position = startingPosition + offset;
    }
}
