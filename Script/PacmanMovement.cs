using UnityEngine;
using System.Collections;

public class PacmanMovement : MonoBehaviour
{
    CharacterController controller;
    public float speed = 5.0f;
    public float jumpSpeed = 10.0f;

    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;


    // Use this for initialization
    void Start()
    {
        //CharacterController ct = gameObject.AddComponent<CharacterController>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}