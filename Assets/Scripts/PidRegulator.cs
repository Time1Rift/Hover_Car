using UnityEngine;
using System;

[Serializable]
public class PidRegulator
{
    [SerializeField, Min(0)] private float _ki = 1f;
    [SerializeField, Min(0)] private float _kp = 1f;
    [SerializeField, Min(0)] private float _kd = 1f;
    [SerializeField] private float _minOut = -1000;
    [SerializeField] private float _maxOut = 1000;

    private float _integral;
    private float _prevError;
    private float _error;
    private float _currentDelta;
    private float _targetPoint;

    public PidRegulator(float ki, float kp, float kd)
    {
        _ki = ki;
        _kp = kp;
        _kd = kd;
    }

    public PidRegulator() { }

    public float Tick(float time, float input) => ComputePID(time, input);

    public float Tick(float time, float input, float target)
    {
        SetTarget(target);
        return ComputePID(time, input);
    }

    public void SetTarget(float target) => _targetPoint = target;

    public void SetMinMax(float min, float max)
    {
        _minOut = min;
        _maxOut = max;
    }

    private float ComputePID(float time, float input)
    {
        _error = _targetPoint - input;
        _integral = Math.Clamp(_integral + _error * time * _ki, _minOut, _maxOut);
        _currentDelta = (_error - _prevError) / time;
        _prevError = _error;
        return Math.Clamp((_error * _kp) + _integral + (_currentDelta * _kd), _minOut, _maxOut);
    }
}