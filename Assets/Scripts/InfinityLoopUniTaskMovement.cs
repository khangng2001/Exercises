using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityLoopUniTaskMovement : MonoBehaviour
{
    [SerializeField] private float _amplitude = 5f;
    [SerializeField] private float _frequency = 1f;
    
    private Vector3 newPosition = Vector3.zero;
    
    
    void Update()
    {
        
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
