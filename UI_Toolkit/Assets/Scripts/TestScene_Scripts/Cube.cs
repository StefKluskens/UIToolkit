using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    private float _targetScale = 1.0f;
    private Vector3 _velocity;
    private Quaternion _targetRotation;

    private void OnEnable()
    {
        MainScreen.ScaleChanged += OnScaleChanged;
        MainScreen.SpinClicked += OnSpinClicked;
    }

    private void OnDisable()
    {
        MainScreen.ScaleChanged -= OnScaleChanged;
        MainScreen.SpinClicked -= OnSpinClicked;
    }

    private void OnScaleChanged(float newScale)
    {
        _targetScale = newScale;
    }

    private void OnSpinClicked()
    {
        _targetRotation = transform.rotation * Quaternion.Euler(Random.insideUnitSphere * 360);
    }

    private void Update()
    {
        transform.localScale = Vector3.SmoothDamp(transform.localScale, Vector3.one * _targetScale, ref _velocity, 0.15f);

        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, 5 * Time.deltaTime);
    }
}
