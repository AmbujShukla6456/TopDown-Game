using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float[] fireSpeed={1.5f,-1.5f};
    public float distance=0.3f;
    public Transform[] fireBalls;

    private void Update()
    {
        for(int i=0;i<fireBalls.Length;i++)
        {
            fireBalls[i].position=transform.position + new Vector3(-Mathf.Cos(Time.time *fireSpeed[i])*distance,Mathf.Sin(Time.time*fireSpeed[i])*distance,0);
        }
    }
}
