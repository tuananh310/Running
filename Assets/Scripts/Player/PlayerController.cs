using System;
using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    #region Variables: Movement
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;
    [SerializeField] private Movement movement;

    #endregion

    #region Variables: Rotation
    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    #endregion

    #region Variables: Gravity
    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;

    #endregion

    #region Variables: Jump
    [SerializeField] private float jumpPower;
    private int _numberOfJumps;
    [SerializeField] private int maxNumberOfJumps = 2;

    #endregion

    #region Variables: Attack
    
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float damage, attackRange, shootingRange;

    #endregion
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

    public void MeleeAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RaycastHit[] enemies = EnemyInRange();
            foreach (var item in enemies)
            {
                float angle = Vector3.Angle(item.transform.position - this.transform.position, this.transform.forward);
                if (angle < 45)
                {
                    var enemy = item.transform.gameObject.GetComponent<EnemyAIPatrol>();
                    enemy.currentHealth -= damage;
                    enemy.healthBar.SetHealth(enemy.currentHealth);
                }
            }
        }
    }

    private RaycastHit[] EnemyInRange()
    {
        var listOfObjects = Physics.SphereCastAll(this.transform.position, 5, this.transform.forward, attackRange, enemyLayer);
        return listOfObjects;
    }

    public void Shooting(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Debug.Log("hihihi");
            Vector3 currentPos = this.transform.position;
            currentPos.y += 2;
            BulletManager.instance.Shooting(currentPos);
            // RaycastHit hit;
            // if (Physics.Raycast(currentPos, transform.TransformDirection(Vector3.forward), out hit, shootingRange, enemyLayer))
            // {
            //     Debug.DrawRay(currentPos, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //     Debug.Log(hit.transform.name);
            // }
        }
    }

    private void OnDrawGizmos()
    {
        // Gizmos.color = Color.red;
        // Gizmos.DrawRay(this.transform.position, transform.TransformDirection(Vector3.forward) * 100);
    }

    private void ApplyGravity()
    {
        if (IsGrounded() && _velocity < 0f)
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
        var targetSpeed = movement.isSprinting ? movement.speed * movement.multiplier : movement.speed;
        movement.currentSpeed = Mathf.MoveTowards(movement.currentSpeed, targetSpeed, movement.acceleration * Time.deltaTime);

        _characterController.Move(_direction * movement.currentSpeed * Time.deltaTime); // Di chuyển nhân vật
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(-_input.y, 0f, _input.x);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!IsGrounded() && _numberOfJumps >= maxNumberOfJumps) return;
        if (_numberOfJumps == 0) StartCoroutine(WaitForLanding());

        _numberOfJumps++;
        _velocity = jumpPower;
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        movement.isSprinting = context.started || context.performed;
    }

    private IEnumerator WaitForLanding()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(IsGrounded);

        _numberOfJumps = 0;
    }

    private bool IsGrounded() => _characterController.isGrounded;
}

[Serializable]
public struct Movement
{
    public float speed;
    public float multiplier;
    public float acceleration; // gia tốc
    [HideInInspector] public bool isSprinting;
    [HideInInspector] public float currentSpeed;
}


