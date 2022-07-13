using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Rigidbody _targetRigidbody;
    [SerializeField] private float _rotationSmoothness = 1f;

    private void LateUpdate()
    {
        transform.position = _targetTransform.position;
        if (_targetRigidbody.velocity.magnitude >= 0.3f)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_targetRigidbody.velocity), Time.deltaTime / _rotationSmoothness);
    }
}