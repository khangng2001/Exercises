using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityLoopMovement : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] private float _amplitude = 5f;
    [SerializeField] private float _frequency = 1f;
    
    private Vector3 newPosition = Vector3.zero;
    void Update()
    {
        // float x = transform.position.x;
        // float y = Mathf.Sin(Time.time * _frequency) * _amplitude;
        
        //float scale = 2 / (3 - Mathf.Cos(2 * Time.time));
        // float x = Mathf.Cos(Time.time * _frequency) * _amplitude;
        // float y = Mathf.Sin(Time.time * _frequency) * _amplitude;
        
        // float x = scale * Mathf.Cos(Time.time);
        // float y = scale * Mathf.Sin(2 * (Time.time)) / 2 ;

        // float x = Mathf.Cos(Time.time) * _amplitude;
        // float y = Mathf.Sin(2 * (Time.time)) * _amplitude / 2;
        
        float x = Mathf.Cos(Time.time * _frequency + Mathf.PI) * _amplitude;
        
        float y = Mathf.Sin(2 * Time.time * _frequency) * _amplitude / 2;
        
        float z = transform.position.z;
        
        newPosition = new Vector3(x, y, z);
        transform.position = newPosition;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(newPosition, 2f * Vector3.one);
    }
}
