using Cysharp.Threading.Tasks; 
using UnityEngine;

public class InfinityLoopMovementUniTask : MonoBehaviour
{
    [SerializeField] private float _amplitude = 5f;
    [SerializeField] private float _frequency = 1f;

    private Vector3 newPosition = Vector3.zero;

    private void Start()
    {
        MoveAsync().Forget();
    }

    private async UniTaskVoid MoveAsync()
    {
        while (true)
        {
            // Calculate x and y positions
            float x = Mathf.Cos(Time.time * _frequency + Mathf.PI) * _amplitude;
            float y = Mathf.Sin(2 * Time.time * _frequency) * _amplitude / 2;
            float z = transform.position.z;

            // Update the position
            newPosition = new Vector3(x, y, z);
            transform.position = newPosition;

            // Wait for the next frame
            await UniTask.NextFrame();
        }
    }
}