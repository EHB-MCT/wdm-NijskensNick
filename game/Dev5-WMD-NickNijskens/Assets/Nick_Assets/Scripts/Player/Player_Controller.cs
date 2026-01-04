using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    #region Physics & direction parameters
    Vector3 getMoveDirection;
    Rigidbody rb => GetComponent<Rigidbody>();
    Camera cam => Camera.main;
    #endregion
    #region Horizontal movement parameters
    Vector2 moveInput = new Vector2();
    [Header("---HORIZONTAL MOVEMENT---")]
    [SerializeField] float acceleration = 6f;
    [SerializeField] float maxSpeed = 6f;
    public bool isMoving = false;
    [SerializeField] bool fpsMode = true;
    #endregion
    #region Debug parameters
    [Header("---DEBUG PARAMETERS---")]
    [SerializeField] bool logData = false;
    #endregion

    void Update()
    {
        getMoveDirection = MoveDirection();
        MovePlayer(getMoveDirection);
        if (isMoving && !fpsMode) RotatePlayer(getMoveDirection);
    }

    #region Horizontal movement
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (moveInput.x != 0 || moveInput.y != 0)
        {
            isMoving = true;
            if (logData)
            {
                Debug.Log(moveInput);
            }
        }
        else
        {
            isMoving = false;
        }
    }

    private Vector3 MoveDirection()
    {
        Vector3 adjustedMovement = new Vector3(moveInput.x, 0f, moveInput.y);
        return CameraAdjustedMoveDirection(adjustedMovement);
    }

    private Vector3 CameraAdjustedMoveDirection(Vector3 moveDirection)
    {
        Vector3 forward = cam.transform.forward * moveDirection.z;
        Vector3 right = cam.transform.right * moveDirection.x;
        Vector3 combinedDirection = right + forward;
        Vector3 finalVector = new Vector3(combinedDirection.x, 0f, combinedDirection.z);
        return finalVector;
    }

    private void MovePlayer(Vector3 direction)
    {
        if (!Player_GroundDetection.instance.IsGrounded(transform.position)) return;
        Vector3 forceDirection = direction * acceleration * Time.deltaTime;
        Vector3 cachedVelocity = rb.linearVelocity + new Vector3(forceDirection.x, 0, forceDirection.z);
        rb.linearVelocity = cachedVelocity;
        rb.linearVelocity = Vector3.ClampMagnitude(cachedVelocity, maxSpeed);
    }
    #endregion

    #region Rotation
    void RotatePlayer(Vector3 direction)
    {
        if (direction == Vector3.zero) return;
        transform.rotation = Quaternion.LookRotation(direction);
    }
    #endregion
}
