using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam; // main camera
    public float speed = 16f; // tốc độ di chuyển

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // sự kiện di chuyển trái phải
        float vertical = Input.GetAxisRaw("Vertical"); // sự kiện di chuyển tiến lùi
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // hướng di chuyển

        if (direction.magnitude >= 0.1f) // nếu độ lớn của hướng >= 0.1
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; // xác định góc mục tiêu muốn nhân vật xoay
            // float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; // xác định góc mục tiêu muốn nhân vật xoay
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            // transform.rotation = Quaternion.Euler(0f, targetAngle, 0f); // xoay nhân vật
            transform.rotation = Quaternion.Euler(0f, angle, 0f); // xoay nhân vật

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime); // di chuyển nhân vật
        }
    }
}
