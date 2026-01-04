using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_LookAround : MonoBehaviour
{
    #region Look Around Parameters
    Camera cam => Camera.main;
    [Header("---LOOK AROUND PARAMETERS---")]
    [SerializeField] float lookSpeedHorizontal = 0.75f;
    [SerializeField] float lookSpeedVertical = 0.5f;
    [Header("---DEBUG PARAMETERS---")]
    [SerializeField] bool logHorizontalData = false;
    [SerializeField] bool logVerticalData = false;
    private Vector2 lookInput = new Vector2();
    private bool isLooking = false;
    #endregion

    void Update()
    {
        if (isLooking)
        {
            RotatePlayerHorizontal();
            RotatePlayerVertical();
        }
    }

    #region Rotate player
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
        if (lookInput.x != 0 || lookInput.y != 0)
        {
            isLooking = true;
        }
        else
        {
            isLooking = false;
        }
    }
    private void RotatePlayerHorizontal()
    {
        cam.transform.Rotate(Vector3.up, lookInput.x * lookSpeedHorizontal, Space.World);
        if (logHorizontalData)
        {
            Debug.Log(cam.transform.localEulerAngles.y);
        }
    }

    private void RotatePlayerVertical()
    {
        cam.transform.Rotate(Vector3.left, lookInput.y * lookSpeedVertical, Space.Self);
        if (cam.transform.localEulerAngles.z > 90)
        {
            cam.transform.Rotate(Vector3.right, lookInput.y * lookSpeedVertical, Space.Self);
        }
        if (logVerticalData)
        {
            Debug.Log(cam.transform.localEulerAngles.x);
        }
    }
    #endregion
}
