using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField, Min(0)] private float _maxDistance = 1.5f; //  2.5 - for the rear wheels
    [SerializeField, Min(0)] private float _maxForce = 300; //  400 - for the rear wheels
    [SerializeField, Min(0)] private float _damping = 20f;
    [SerializeField, Min(0)] private float _progressivity = 1.74f;
    [SerializeField, Min(0)] private float _upFactot = 0.75f;

    private Transform _transform;
    private Vector3 _forward;
    private Vector3 _forceDirection;
    private float _forceFactor;
    private Vector3 _worldSpeed;
    private Vector3 _worldOldPosition;

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        _forward = _transform.forward;

        if (Physics.Raycast(_transform.position, _forward, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
            Lift(_forward, hitInfo.distance);

        Damping();
    }

    private void Lift(Vector3 forward, float distance)
    {
        _forceFactor = Mathf.Pow(Mathf.Clamp01(1 - distance / _maxDistance), _progressivity);
        _forceDirection = Vector3.Slerp(-forward, Vector3.up, _upFactot);
        _rigidbody.AddForceAtPosition(_forceDirection * _maxForce * _forceFactor, _transform.position, ForceMode.Force);
    }

    private void Damping()
    {
        _worldSpeed = (_transform.position - _worldOldPosition) * Time.fixedDeltaTime;  //  calculating the speed
        float dotResult = Mathf.Clamp01(Vector3.Dot(_forceDirection.normalized, _worldSpeed.normalized));   //  checks the need to use dumping
        _rigidbody.AddForceAtPosition(-_forceDirection * _worldSpeed.magnitude * dotResult * _damping, _transform.position, ForceMode.Force);
        _worldOldPosition = _transform.position;
    }
}