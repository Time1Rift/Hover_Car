using System;
using UnityEngine;

[Serializable]
public class HoverControlsInfo
{
    public Vector3 AimingPoint;
    public Vector3 LookPoint;
    public Vector3 MoveInput;

    public event Action ShootPressed;
}