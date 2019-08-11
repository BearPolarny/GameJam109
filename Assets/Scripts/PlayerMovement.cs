using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speedWalk = 2.0f;
    [SerializeField]
    private float speedRun = 4.0f;
    [SerializeField]
    private float speedCrouch = 1.0f;

    [SerializeField]
    private float sprintMaxTime = 4f; // in seconds
    private float sprintTimeLeft = 4f;
    [SerializeField]
    private float sprintRegenMod = 1;
    [SerializeField]
    private float sprintRegenThreshold = 2; // minimum time to rest from running


    [SerializeField]
    private Vector3 jumpForce = new Vector3(0, 3f, 0);
    
    private float speed = 0;
    
    private bool isRunning = false;
    private bool isRegenerting = false;
    private bool isSprintLocked = false;
    private bool isCrouching = false;
    //private bool isGrounded = true;

    private CharacterController CharacterComponent;
    private Rigidbody Rigidbody;

    // Use this for initialization
    void Start()
    {
        CharacterComponent = GetComponent<CharacterController>();
        Rigidbody = GetComponent<Rigidbody>();
        speed = speedWalk;
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardMovement();

        float deltaX, deltaZ;

        deltaX = Input.GetAxis("Horizontal") * speed;
        deltaZ = Input.GetAxis("Vertical") * speed;


        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        movement = transform.TransformDirection(movement);
        movement *= Time.deltaTime;
        CharacterComponent.Move(movement);

    }

    void KeyboardMovement()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSprintLocked)
        {
            isRunning = true;
            isRegenerting = false;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            isRegenerting = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
        }

        if (isRunning || isCrouching)  // Crouching has bigger priority than running
        {
            if (isCrouching)
            {
                speed = speedCrouch;
                isRunning = false;

            } else
            {
                speed = speedRun;
            }
        } else
        {
            speed = speedWalk;
        }

        if (isRunning && !isSprintLocked)
        {
            // Mam czas - biegne
            Debug.Log("run");
            sprintTimeLeft -= Time.deltaTime;
        }
        if (!isCrouching && sprintTimeLeft < 0)
        {
            Debug.Log("no run");
            // nie mam czasu - nie biegne, zaczynam regenerowac
            speed = speedWalk;
            isSprintLocked = true;
        }
        if (isRegenerting)
        {
            Debug.Log("regen norun");
            sprintTimeLeft += Time.deltaTime * sprintRegenMod;
            
            if (isSprintLocked && sprintTimeLeft > sprintRegenThreshold)    // lazy evaluation? mam nadzieję
            {
                Debug.Log("regen run");
                // mam czas regeneruje biegne
                isSprintLocked = false;
            }
        }
        if (sprintTimeLeft > sprintMaxTime)
        {
            Debug.Log("no regen");
            isRegenerting = false;
        }

    }
}