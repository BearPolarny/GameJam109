using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed_walk = 2.0f;
    public float speed_run = 4.0f;
    public float speed_crouch = 1.0f;
    public float max_sprint = 4f;

    public float sprint_left = 3f;

    public float jump_height = 4.0f;

    public float height;
    float speed;
    bool isRunning = false;
    bool isRegenerting = false;

    private CharacterController CharacterComponent;
    private bool isCrouching = false;
    private float sprint_modificator = 1;

    // Use this for initialization
    void Start()
    {
        CharacterComponent = GetComponent<CharacterController>();
        speed = speed_walk;
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardMovement();

        if (isRunning && sprint_left > 0)
        {
            sprint_left -= Time.deltaTime;
        }
        else if (sprint_left <= 0 && !isCrouching)
        {
            speed = speed_walk;
            isRegenerting = true;
        } else if (sprint_left < max_sprint)
        {
            sprint_left += Time.deltaTime * sprint_modificator;

            if (isRegenerting && sprint_left > 2)
            {
                isRegenerting = false;
            }
        }
    }

    void KeyboardMovement()
    {
        float deltaX, deltaZ;
        if (Input.GetKeyDown("left shift") && sprint_left > 2 && !isRegenerting)
        {
            speed = speed_run;
            isRunning = true;
            Debug.Log("speeeeeed");
        }
        else if (Input.GetKeyUp("left shift"))
        {
            speed = speed_walk;
            isRunning = false;
        }

        if (Input.GetKeyDown("left ctrl"))
        {
            isCrouching = true;
            speed = speed_crouch;
        }
        else if (Input.GetKeyUp("left ctrl"))
        {
            speed = speed_walk;
            isCrouching = false;
        }


        deltaX = Input.GetAxis("Horizontal") * speed;
        deltaZ = Input.GetAxis("Vertical") * speed;

        // Skakanie ssie pałkę motzno. Czasami skacze jak powinien, a przez resztę przypadków skacze jakby nogę złamał
        if (CharacterComponent.isGrounded && Input.GetButton("Jump"))
        {
            height = 0; // to chyba naprawiło ^ bug
            height += jump_height;

        }
        else if (!CharacterComponent.isGrounded)
        {
            height += Physics.gravity.y * Time.deltaTime;
        }

        Vector3 movement = new Vector3(deltaX, height, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed_run);  //Przez to, że zapomiałem o tym i że zablokowałem maks prędkość na speed_walk przez pół godziny zastanawiałem się dlaczego nie mogę biegać...

        movement = transform.TransformDirection(movement);
        movement *= Time.deltaTime;
        CharacterComponent.Move(movement);
    }
}