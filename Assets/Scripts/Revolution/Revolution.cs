using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolution : MonoBehaviour
{
    [SerializeField] private GameObject sun;
    [SerializeField] private GameObject[] earths;
    [SerializeField] private float revolutionSpeed = 10;
    [SerializeField] private Rigidbody rb;
    Vector3 angularVel;

    private void Awake()
    {
        angularVel = new Vector3(0, revolutionSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(sun.transform.position, Vector3.up, revolutionSpeed * Time.deltaTime);
        //rb.AddTorque(new Vector3(0, revolutionSpeed, 0));
        //rb.angularVelocity = angularVel;
        transform.Rotate(Vector3.up, revolutionSpeed);
        //rb.AddTorque(angularVel);
    }
}
