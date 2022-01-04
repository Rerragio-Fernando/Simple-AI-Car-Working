using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScrModels;

public class CarController : MonoBehaviour
{
    public AIVehicleSettings settings;
    public RaycastHit hit;

    [HideInInspector]
    public Rigidbody rb;

    [HideInInspector]
    public float steeringAngle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    #region Steering Methods
    public void Steer(WheelCollider wc){
        wc.steerAngle = steeringAngle;
    }
    public void Steer(WheelCollider wc, float angle){
        wc.steerAngle = angle;
    }
    #endregion

    public void UpdateWheelRotation(){
        WheelRotation(settings.f_LeftWC, settings.f_LeftT);
        WheelRotation(settings.f_RightWC, settings.f_RightT);
        WheelRotation(settings.r_LeftWC, settings.r_LeftT);
        WheelRotation(settings.r_RightWC, settings.r_RightT);
    }

    public void Accelerate(WheelCollider wc, float input){
        wc.motorTorque = input * settings.motor_Force * settings.speed;
    }

    public void Brake(WheelCollider wc, float force){ 
        wc.brakeTorque = force;
    }

    public bool Stopped(){
        if(rb.velocity.magnitude < 50f){
            return true;
        }
        return false;
    }

    public bool Blocked(){
        if(hit.collider != null){
            return true;
        }
        return false;
    }

    public void WheelRotation(WheelCollider wc, Transform wheel){
        Vector3 rot_Position = wheel.position;
        Quaternion rot = transform.rotation;

        wc.GetWorldPose(out rot_Position, out rot);

        wheel.position = rot_Position;
        wheel.rotation = rot;
    }
}
