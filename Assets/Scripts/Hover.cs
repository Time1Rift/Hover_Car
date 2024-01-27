using UnityEngine;

[RequireComponent(typeof(ConstantForce))]
public class Hover : MonoBehaviour
{
    [SerializeField, Min(0)] private float _moveForce = 2000;
    [SerializeField, Min(0)] private float _rotateForce = 10;
    [SerializeField] private PidRegulator _regulator = new PidRegulator(); // 0.1; 0.5; 0.5; -50; 50
    [SerializeField] private Gun _gun;

    private ConstantForce _constantForce;
    private IHoverControls _hoverControls;
    private HoverControlsInfo _controlInfo;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _constantForce = GetComponent<ConstantForce>();
        _hoverControls = GetComponent<IHoverControls>();
        _controlInfo = _hoverControls.GetControls();
    }

    private void Update()
    {
        float currentAngle = Vector3.SignedAngle(_transform.forward, _controlInfo.LookPoint - _transform.position, _transform.up);

        float angle = _regulator.Tick(Time.deltaTime, currentAngle, 0);

        _constantForce.relativeTorque = -Vector3.up * angle * _rotateForce;
        _constantForce.relativeForce = _controlInfo.MoveInput * _moveForce;

        _gun.SetTargit(_controlInfo.AimingPoint);
    }
}