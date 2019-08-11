using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed_walk = 4.0f;
    public float speed_run = 71.0f;
    public float jump_height = 7.0f;

    float height;


    private CharacterController CharacterComponent;

    // Use this for initialization
    void Start()
    {
        CharacterComponent = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX, deltaZ;
        float speed = speed_walk;
        if (Input.GetKeyDown("left shift"))
        {
            speed = speed_run;
            Debug.Log("speeeeeed");
        }
        else if (Input.GetKeyUp("left shift"))
            speed = speed_walk;

        deltaX = Input.GetAxis("Horizontal") * speed;
        deltaZ = Input.GetAxis("Vertical") * speed;

        if (Input.GetButton("Jump"))
        {
            Debug.Log("skok");
        }
        if (CharacterComponent.isGrounded && Input.GetButton("Jump"))
        {
            height += jump_height;

        } else if (!CharacterComponent.isGrounded)
        {
            height += Physics.gravity.y * Time.deltaTime;
        }

        Vector3 movement = new Vector3(deltaX, height, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed_walk);

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        CharacterComponent.Move(movement);

    }
}