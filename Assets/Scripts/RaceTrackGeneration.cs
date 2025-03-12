using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaceTrackGeneration : MonoBehaviour
{
    [SerializeField] private float xAxis;
    [SerializeField] private float zAxis;
    [SerializeField] private int segments;

    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        CalculateEllipse();
    }

    private void CalculateEllipse()
    {
        Vector3[] points = new Vector3[segments + 1];
        for (int i = 0; i < segments; i++)
        {
            float angle = ((float)i / (float)segments * Mathf.PI);
             //float angle = ((float)i / (float)segments * 360 * Mathf.Deg2Rad);
             float x = Mathf.Sin(angle) * xAxis;
             float y = Mathf.Cos(angle) * zAxis;
             points[i] = new Vector3(x,0f,y);
             
        }

        points[segments] = points[0];
        _lineRenderer.positionCount = segments + 1;
        _lineRenderer.SetPositions(points);
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            if (_lineRenderer == null)
            {
                _lineRenderer = GetComponent<LineRenderer>();
            }
            CalculateEllipse();
        }
    }
}
