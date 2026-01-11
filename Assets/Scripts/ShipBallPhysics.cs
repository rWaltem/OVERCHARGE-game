using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShipBallPhysics : MonoBehaviour
{
    [Header("Base Control")]
    public GameObject controlSphere;
    public Vector3 modelOffset;
    
    
    private GetStats playerStats;
    private Rigidbody ctlRB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
