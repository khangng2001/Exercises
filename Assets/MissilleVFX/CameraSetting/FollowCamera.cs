using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset;

    private void LateUpdate()
    {
        if (target) transform.position = target.position + offset;
    }
}
