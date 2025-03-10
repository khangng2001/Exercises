using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BouncingBallDot : MonoBehaviour
{
    [SerializeField] private float bouncingStrength = 0.8f;
    [SerializeField] private float initialForce = 5f; //initialVelocity
    [SerializeField] private float desiredAngle = 45f;

    private Vector3 _velocity;
    private float _angleRadian;
    private readonly float _groundValue = 0.5f;
    private Vector3 _targetPosition;
    private void Start()
    {
        Calculate();
    }

    private void Update()
    {
        ThrowingAndBouncing();
    }

    private void Calculate()
    {
        _angleRadian = desiredAngle * Mathf.Deg2Rad;
        
        //Calculate initialVelocity X and Y
        float initialVelocityX = initialForce * Mathf.Cos(_angleRadian);
        float initialVelocityY = initialForce * Mathf.Sin(_angleRadian);
        _velocity = new Vector3(initialVelocityX, initialVelocityY, 0);
    }

    private void ThrowingAndBouncing()
    {
         _velocity.y += Physics2D.gravity.y * Time.deltaTime;
        
        float newPosX = transform.position.x + _velocity.x * Time.deltaTime;
        float newPosY = transform.position.y + _velocity.y * Time.deltaTime;
        
        newPosY = Mathf.Max(newPosY, 0f);
        
        if (newPosY < _groundValue)
        {
            _velocity.y = -_velocity.y * bouncingStrength;
            _velocity.x *= bouncingStrength; //create friction --> ball will stop eventually
        }
        
        _targetPosition.Set(newPosX, newPosY,0f);
        transform.DOMove(_targetPosition, 0.05f);
    }
}
