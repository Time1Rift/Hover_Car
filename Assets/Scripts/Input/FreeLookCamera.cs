using UnityEngine;

public class FreeLookCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _minAngleY;
    [SerializeField] private float _maxAngleY;
    [SerializeField, Min(0)] private float _maxLength = 300;
    [SerializeField] private LayerMask _layerMask;

    private Transform _transform;
    private float _verticalAngle;
    private float _horizontalAngle;

    private void OnValidate()
    {
        if(_maxAngleY <= _minAngleY)
            _maxAngleY = _minAngleY + 1;
    }

    private void Awake()
    {
        _transform = transform;
    }

    private void LateUpdate()
    {
        _transform.position = _target.position;
        _transform.rotation = Quaternion.Euler(_verticalAngle, _horizontalAngle, 0);
    }

    public void Rotate(float inputX, float inputY)
    {
        _horizontalAngle += inputX;
        _verticalAngle -= inputY;
        _verticalAngle = Mathf.Clamp(_verticalAngle, _minAngleY, _maxAngleY);
    }

    public Vector3 Raycast()
    {
        Vector3 forward = _cameraTransform.forward;

        if (Physics.Raycast(_cameraTransform.position, forward, out RaycastHit hitInfo, _maxLength, _layerMask, QueryTriggerInteraction.Ignore))
            return hitInfo.point;

        return (_cameraTransform.position + forward * _maxLength);
    }
}