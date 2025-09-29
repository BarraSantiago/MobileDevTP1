using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<WheelCollider> throttleWheels = new();
    public List<WheelCollider> steeringWheels = new();
    public float throttleCoefficient = 20000f;
    public float maxTurn = 20f;
    private float acel = 1f;
    private float giro;

    // Use this for initialization
    private void Start()
    {
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        foreach (WheelCollider wheel in throttleWheels) wheel.motorTorque = throttleCoefficient * T.GetFDT() * acel;
        foreach (WheelCollider wheel in steeringWheels) wheel.steerAngle = maxTurn * giro;
        giro = 0f;
    }

    public void SetGiro(float giro)
    {
        this.giro = giro;
    }

    public void SetAcel(float val)
    {
        acel = val;
    }
}