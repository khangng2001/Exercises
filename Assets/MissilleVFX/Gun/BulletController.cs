using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private AnimationCurve flowCurve;
    [SerializeField] private float height;
    [SerializeField] private float speed;

    [SerializeField, Range(0, 1)] private float value;
    [SerializeField] private float deactiveAfterTime;

    [SerializeField] private GameObject VFX;
    [SerializeField] private GameObject visual;

    [SerializeField] private List<GameObject> trails;

    private Vector3 startLocation;
    private Vector3 endLocation;

    private bool isRunning = false;

    private void Update()
    {
        if (isRunning)
        {
            if (value >= 1f)
            {
                End();
            }
            RunningValue();
            RunningWithValue();
        }
    }

    public void Init(Vector3 startLocation, Vector3 endLocation, float height)
    {
        this.startLocation = startLocation;
        this.endLocation = endLocation;
        this.height = height;
        transform.position = startLocation;
        value = 0;
        isRunning = true;
        RunVXF(false);
        ActiveVisual(true);
        TurnOffEffectTrail();
        TurnOnRandomEffectTrail();
    }

    void RunningValue()
    {
        value += speed * Time.deltaTime;
    }

    void RunningWithValue()
    {
        Vector3 horizontalLocation = startLocation + (endLocation - startLocation) * value;
        Vector3 verticalLocation = Vector3.up * flowCurve.Evaluate(value) * height;
        transform.position = horizontalLocation + verticalLocation;
    }

    IEnumerator DeactiveAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    void End()
    {
        isRunning = false;
        StartCoroutine(DeactiveAfterTime(deactiveAfterTime));
        RunVXF(true);
        ActiveVisual(false);
    }

    void RunVXF(bool isActive)
    {
        if (VFX)
        {
            VFX.SetActive(isActive);
        }
    }    
    void ActiveVisual(bool isActive)
    {
        if (visual)
        {
            visual.SetActive(isActive);
        }
    }

    void TurnOffEffectTrail()
    {
        foreach (GameObject trail in trails)
        {
            trail.SetActive(false);
        }
    }

    void TurnOnRandomEffectTrail()
    {
        if (trails.Count > 0)
        {
            int randomNumber = Random.Range(0, trails.Count);
            trails[randomNumber].SetActive(true);
        }
    }

    #region HEHE
    //[SerializeField] private float bouncingStrength = 0.8f;
    //[SerializeField] private float initialForce = 5f; //initialVelocity
    //[SerializeField] private float desiredAngle = 45f;

    //private Vector3 _velocity;
    //private float _angleRadian;
    //private readonly float _groundValue = 0.5f;
    //private float _stoppingThreshold = 0.1f;

    //private void Start()
    //{
    //    InitialCalculate();
    //}

    //private void Update()
    //{
    //    ThrowAndBouncing();
    //}

    //private void InitialCalculate()
    //{
    //    _angleRadian = desiredAngle * Mathf.Deg2Rad;

    //    //Calculate initialVelocity X and Y using in projectile motion formula 
    //    //Vx = v0 * Cos(angle)
    //    //Vy = v0 * Sin(angle)
    //    float velocityX = initialForce * Mathf.Sin(_angleRadian);
    //    float velocityY = initialForce * Mathf.Cos(_angleRadian);

    //    _velocity = new Vector3(velocityX, velocityY, 0);

    //    transform.position = new Vector3(velocityX, velocityY);
    //}

    //private void ThrowAndBouncing()
    //{
    //    if (Mathf.Abs(_velocity.x) < _stoppingThreshold && Mathf.Abs(_velocity.y) < _stoppingThreshold 
    //                                                    && transform.position.y <= _groundValue)
    //    {
    //        _velocity = Vector3.zero;
    //        transform.position = new Vector3(transform.position.x, _groundValue, 0);
    //        return;
    //    }

    //    //Calculate Y-velocity
    //    _velocity.y += Physics2D.gravity.y * Time.deltaTime;
    //    float newPosX = transform.position.x + _velocity.x * Time.deltaTime;
    //    float newPosY = transform.position.y + _velocity.y * Time.deltaTime;

    //    if (newPosY <= _groundValue) // --> touches ground
    //    {
    //        _velocity.y = -_velocity.y * bouncingStrength;
    //        _velocity.x *= bouncingStrength; //create friction --> ball will stop eventually
    //        newPosY = _groundValue;
    //    }
    //    transform.position = new Vector3(newPosX, newPosY);
    //}
    #endregion
}
