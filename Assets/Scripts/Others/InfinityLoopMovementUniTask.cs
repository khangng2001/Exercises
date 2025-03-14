using System.Threading;
using Cysharp.Threading.Tasks; 
using UnityEngine;

public class InfinityLoopMovementUniTask : MonoBehaviour
{
    [SerializeField] private float _amplitude = 5f;
    [SerializeField] private float _frequency = 1f;
    
    private Vector3 newPosition = Vector3.zero;
    private CancellationTokenSource _cancellationTokenSource;
    
    private void Start()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        MoveInInfinityLoopAsync(_cancellationTokenSource.Token).Forget();
    }
    
    private void OnDestroy()
    {
        //ensure the async function is terminated
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }
    
    private async UniTaskVoid MoveInInfinityLoopAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            float x = Mathf.Cos(Time.time * _frequency + Mathf.PI) * _amplitude;
            float y = Mathf.Sin(2 * Time.time * _frequency) * _amplitude / 2;
            float z = transform.position.z;
            
            newPosition = new Vector3(x, y, z);
            transform.position = newPosition;
            
            
            //similar to yield return null --> but called before Update
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken); 
        }
    }
}