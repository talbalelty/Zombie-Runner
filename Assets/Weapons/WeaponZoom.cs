using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] float zoomOutFOV = 60f;
    [SerializeField] float zoomInFOV = 20f;
    [SerializeField] float zoomOutSensitivity = 1f;
    [SerializeField] float zoomInSensitivity = 0.3f;

    FirstPersonController FPcontroller;
    CinemachineVirtualCamera vFPCamera;
    StarterAssetsInputs _input;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        vFPCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        FPcontroller = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        AimDownSight();
    }

    void AimDownSight()
    {
        if (_input.aimDownSight)
        {
            vFPCamera.m_Lens.FieldOfView = zoomInFOV;
            FPcontroller.RotationSpeed = zoomInSensitivity;
        }
        else
        {
            vFPCamera.m_Lens.FieldOfView = zoomOutFOV;
            FPcontroller.RotationSpeed = zoomOutSensitivity;
        }
    }
}

