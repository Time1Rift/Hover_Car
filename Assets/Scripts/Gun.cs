using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField, Min(0)] private float _verticalSpeed = 2;

    [SerializeField] private Transform _verticalAxis;
    [SerializeField] private Transform _horizontalAxis;

    [SerializeField] private PidRegulator _xAxisController = new PidRegulator();
    [SerializeField] private PidRegulator _yAxisController = new PidRegulator();

    [SerializeField] private Transform _hoverTransform;

    private Vector3 _targetPoint;

    private void Update()
    {
        Vector3 aimDirection = (_targetPoint - _verticalAxis.position).normalized;

        float verticalAngle = Vector3.SignedAngle(_hoverTransform.forward, _verticalAxis.forward, _verticalAxis.up);
        float needAngle = verticalAngle + Vector3.SignedAngle(_verticalAxis.forward, aimDirection, _verticalAxis.up);

        _verticalAxis.rotation = Quaternion.AngleAxis(_yAxisController.Tick(Time.deltaTime, verticalAngle, needAngle) * Time.deltaTime * _verticalSpeed, _verticalAxis.up) * _verticalAxis.rotation;
    }

    public void SetTargit(Vector3 target) => _targetPoint = target;
}