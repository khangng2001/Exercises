using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BouncingBallMovement : MonoBehaviour
{
    [SerializeField] private float bouncingStrength = 0.8f;
    [SerializeField] private float initialForce = 5f; //initialVelocity
    [SerializeField] private float desiredAngle = 45f;

    private Vector3 _velocity;
    private float _angleRadian;
    private readonly float _groundValue = 0.5f;
    
    private void Start()
    {
        InitialCalculate();
    }

    private void Update()
    {
        ThrowAndBouncing();
    }
    
    private void InitialCalculate()
    {
        _angleRadian = desiredAngle * Mathf.Deg2Rad;
        
        //Calculate initialVelocity X and Y
        float initialVelocityX = initialForce * Mathf.Cos(_angleRadian);
        float initialVelocityY = initialForce * Mathf.Sin(_angleRadian);
        
        _velocity = new Vector3(initialVelocityX, initialVelocityY, 0);
        
    }
    
    private void ThrowAndBouncing()
    {
        //Calculate Y-velocity
        _velocity.y += Physics2D.gravity.y * Time.deltaTime;
        
        float newPosX = transform.position.x + _velocity.x * Time.deltaTime;
        float newPosY = transform.position.y + _velocity.y * Time.deltaTime;
        
        newPosY = Mathf.Max(newPosY, 0.5f);
        
        if (newPosY <= _groundValue)
        {
            _velocity.y = -_velocity.y * bouncingStrength;
            _velocity.x *= bouncingStrength; //create friction --> ball will stop eventually
        }
        transform.position = new Vector3(newPosX, newPosY);
    }
}
