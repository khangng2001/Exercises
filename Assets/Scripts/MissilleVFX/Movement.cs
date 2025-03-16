using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float rotateSpeed;

    private Vector2 _input;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    void GetInput()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void OnMove()
    {
        if (_rb != null)
        {
            _rb.position += _input.normalized.magnitude * transform.forward * moveSpeed * Time.deltaTime;

            if (_input.normalized.magnitude > 0.1f)
            {
                _rb.rotation = Quaternion.Slerp(_rb.rotation, Quaternion.LookRotation(new Vector3(_input.x, 0, _input.y)), rotateSpeed * Time.deltaTime);
            }
        }
    }
}
