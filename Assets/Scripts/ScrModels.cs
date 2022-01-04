using System;
using System.Collections.Generic;
using UnityEngine;

public static class ScrModels
{
    [Serializable]
    public class AICarSettings{
        public float mov_Speed = 1.0f;
        public float turn_Speed = 0.1f;
    }

    public struct StructAI{
        public Transform checkpoints;
        public int index;
        public Vector3 dir_Steer;
    }

    [Serializable]
    public class AIVehicleSettings{
        public WheelCollider f_LeftWC, f_RightWC, r_LeftWC, r_RightWC;
        public Transform f_LeftT, f_RightT, r_LeftT, r_RightT;
        public float speed = 50f;
        public float max_Steer_Angle = 30f;
        public float motor_Force;
    }
}
