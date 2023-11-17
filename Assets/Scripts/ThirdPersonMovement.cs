using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Rigidbody rb;
    public Transform cam; // main camera
    public float speed = 16f; // tốc độ di chuyển
    // [SerializeField] private float gravity = -20f;
    // [SerializeField] private float rotationSpeed = 16f;
    [SerializeField] private float jumpSpeed = 15f;
    Vector3 moveVelocity;
    Vector3 turnVelocity;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // private void Awake()
    // {
    //     characterController = GetComponent<CharacterController>();
    //     // rb = GetComponent<Rigidbody>();
    // }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // sự kiện di chuyển trái phải
        float vertical = Input.GetAxisRaw("Vertical"); // sự kiện di chuyển tiến lùi
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // hướng di chuyển
        if (Input.GetButtonDown("Jump"))
        {
            direction.y = jumpSpeed;
        }
        // if (controller.isGrounded)
        // {
        //     moveVelocity = transform.forward * speed * vertical;
        //     turnVelocity = transform.up * rotationSpeed * horizontal;
        //     if (Input.GetButtonDown("Jump"))
        //     {
        //         direction.y = jumpSpeed;
        //     }
        // }
        // moveVelocity.y += gravity * Time.deltaTime;
        // controller.Move(moveVelocity * Time.deltaTime);
        // transform.Rotate(turnVelocity * Time.deltaTime);


        if (direction.magnitude >= 0.1f) // nếu độ lớn của hướng >= 0.1
        {
            // float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; // xác định góc mục tiêu muốn nhân vật xoay
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; // xác định góc mục tiêu muốn nhân vật xoay
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            // transform.rotation = Quaternion.Euler(0f, targetAngle, 0f); // xoay nhân vật
            transform.rotation = Quaternion.Euler(0f, angle, 0f); // xoay nhân vật

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime); // di chuyển nhân vật
        }
    }
}
