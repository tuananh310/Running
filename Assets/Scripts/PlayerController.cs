using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private Vector2 _input;
    [SerializeField] private Transform cam; // main camera
    private CharacterController _characterController;
    private Vector3 _direction;

    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    [SerializeField] private float speed;

    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyRotation();
        ApplyMovement();
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded && _velocity < 0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }

        _direction.y = _velocity;
    }

    private void ApplyRotation()
    {
        if (_input.sqrMagnitude == 0) return;

        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg; // Lấy góc muốn xoay
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime); // Làm mượt khi xoay
        transform.rotation = Quaternion.Euler(0f, angle, 0f); // Xoay nhân vật
    }

    private void ApplyMovement()
    {
        _characterController.Move(_direction * speed * Time.deltaTime); // Di chuyển nhân vật
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0f, _input.y);
    }
}
