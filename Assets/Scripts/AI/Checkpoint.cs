using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Vector3 wallSize;
    
    private void OnDrawGizmos() {
        if(transform.childCount < 2){
            return;
        }
        
        for(int i = 0; i < transform.childCount - 1; i++){
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }

        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }

    public void AnglesSizeCheckPointWalls(){
        Transform cur_Pnt, nxt_Pnt, prev_Pnt;
        int nxt, prev;
        Quaternion cur_Rot, prev_Rot;

        for(int i = 0; i<transform.childCount; i++){
            nxt = NextIndex(i);
            prev = PrevIndex(i);

            cur_Pnt = transform.GetChild(i);
            nxt_Pnt = transform.GetChild(nxt);
            prev_Pnt = transform.GetChild(prev);

            cur_Pnt.localScale = wallSize;

            cur_Pnt.LookAt(nxt_Pnt);
            cur_Rot = new Quaternion(cur_Pnt.transform.rotation.x, cur_Pnt.transform.rotation.y, cur_Pnt.transform.rotation.z, cur_Pnt.transform.rotation.w);
            cur_Pnt.LookAt(prev_Pnt);
            prev_Rot = new Quaternion(prev_Pnt.transform.rotation.x, prev_Pnt.transform.rotation.y, prev_Pnt.transform.rotation.z, prev_Pnt.transform.rotation.w);

            cur_Pnt.transform.rotation = Quaternion.Lerp(cur_Rot, prev_Rot, 0.5f);
        }
    }

    int NextIndex(int x){
        if(x < transform.childCount - 1){
            return x + 1;
        }
        else{
            return 0;
        }
    }

    int PrevIndex(int x){
        if(x == 0 ){
            return transform.childCount - 1;
        }
        else{
            return x - 1;
        }
    }
}
