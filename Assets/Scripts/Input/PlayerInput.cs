using UnityEngine;

public class PlayerInput : MonoBehaviour, IHoverControls
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const string MouseX = "Mouse X";
    private const string MouseY =  "Mouse Y";

    [SerializeField] private FreeLookCamera _freeLookCamera;
    [SerializeField, Range(1, 5)] private float _sensitivity;

    private HoverControlsInfo _controls = new HoverControlsInfo();

    private void Update()
    {
        _freeLookCamera.Rotate(Input.GetAxis(MouseX) * _sensitivity, Input.GetAxis(MouseY) * _sensitivity);
        _controls.MoveInput = Vector3.forward * Input.GetAxis(Vertical) + Vector3.right * Input.GetAxis(Horizontal);
        _controls.AimingPoint = _controls.LookPoint = _freeLookCamera.Raycast();
    }

    public HoverControlsInfo GetControls() => _controls;
}