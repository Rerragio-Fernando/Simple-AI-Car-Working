using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScrModels;

public class AIVehicle : CarController
{
    public StructAI ai;
    public float lerp_Multiplier, angleOffset, brakeforce_Multiplier = 1f;
    public int follow_Offset;
    public LayerMask Collision_Layer;

    private Renderer rend;
    private Vector3 direction;
    private float steering_Lerper = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();

        ai.checkpoints = GameObject.FindWithTag("Checkpoint").transform;
        ai.index = 0 + follow_Offset;
    }

    void FixedUpdate()
    {
        steeringAngle = settings.max_Steer_Angle;
        var forward = transform.TransformDirection(Vector3.forward);

        Physics.Raycast(transform.position, forward, out hit, 1f, Collision_Layer);

        ai.dir_Steer = ai.checkpoints.GetChild(ai.index).position - transform.position;

        Debug.DrawRay(transform.position, ai.dir_Steer * 10f, Color.white);
        Debug.DrawRay(transform.position, forward * 10f, Color.blue);

        float angle = Vector3.SignedAngle(forward, ai.dir_Steer.normalized, Vector3.up);
        
        if(angle > angleOffset){
            Acceleration(0.5f);
            MoveRight(1f);
        }
        else if(angle < -angleOffset){
            Acceleration(1f);
            MoveLeft(1f);
        }
        else{
            Acceleration(1f);
            MoveForward(0f);
        }
        
        
        UpdateWheelRotation();
    }

    void Acceleration(float val){
        Accelerate(settings.r_LeftWC, val);
        Accelerate(settings.r_RightWC, val);
    }

    void MoveForward(float l){
        ChangeSteeringLerper(l);
        Brake_Car_Release();
        Steer_Car();
    }

    void MoveLeft(float l){
        ChangeSteeringLerper(-l);
        if(!Stopped()){
            Brake_Car();
        }
        Steer_Car();
    }

    void MoveRight(float l){
        ChangeSteeringLerper(l);
        if(!Stopped()){
            Brake_Car();
        }
        Steer_Car();
    }

    void Steer_Car(){
        Steer(settings.f_LeftWC, steeringAngle * steering_Lerper);
        Steer(settings.f_RightWC, steeringAngle * steering_Lerper);
    }

    void Brake_Car_Release(){
        Brake(settings.r_LeftWC, 0f);
        Brake(settings.r_RightWC, 0f);
    }

    void Brake_Car(){
        Brake(settings.r_LeftWC, brakeforce_Multiplier * ((settings.speed * settings.motor_Force)/2f));
        Brake(settings.r_RightWC, brakeforce_Multiplier * ((settings.speed * settings.motor_Force)/2f));
    }

    void ChangeSteeringLerper(float n){
        steering_Lerper = Mathf.Lerp(steering_Lerper, n, lerp_Multiplier * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col) {
        if(col.CompareTag("Wall")){
            ai.index = CalcNextCheckpoint();
        }
    }

    private int CalcNextCheckpoint(){
        int cur = ExtractNumberFromString(ai.checkpoints.GetChild(ai.index).name);
        int next = cur + 1;

        if(next > ai.checkpoints.childCount - 1){
            next = 0 + follow_Offset;
        }

        return next;
    }

    private int ExtractNumberFromString(string str){
        return System.Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(str, "[^0-9]", ""));
    }
}
