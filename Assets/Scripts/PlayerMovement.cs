using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    enum EState
    {
        RUNNING,
        SNEAKING,
        WALKING
    }
    [SerializeField]
    EState CurrentState = EState.WALKING;

    [SerializeField]
    private float speedWalk = 2.0f;
    [SerializeField]
    private float speedRun = 4.0f;
    [SerializeField]
    private float speedCrouch = 1.0f;

    [SerializeField]
    private float sprintMaxTime = 4f; // in seconds
    public float sprintTimeLeft = 4f;
    [SerializeField]
    private float sprintRegenMod = 1;
    [SerializeField]
    private float sprintRegenThreshold = 2; // minimum time to rest from running


    [SerializeField]
    private Vector3 jumpForce = new Vector3(0, 3f, 0);
    
    private float speed = 0;
    
    private bool isRegenerting = false;
    private bool isSprintLocked = false;
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

        switch (CurrentState)
        {
            case EState.WALKING:
                speed = speedWalk;
                break;
            case EState.SNEAKING:
                speed = speedCrouch;
                break;
            case EState.RUNNING:
                if (!isSprintLocked) // Normal running 
                {
                    //Debug.Log("run");
                    sprintTimeLeft -= Time.deltaTime;
                }
                if (sprintTimeLeft < 0) // Exhausted
                {
                    Debug.Log("no run");
                    CurrentState = EState.WALKING;
                    isSprintLocked = true;
                }
                speed = speedRun;
                break;
            default:
                break;
        }
        
        if (isRegenerting)
        {
            Debug.Log("regen norun");
            sprintTimeLeft += Time.deltaTime * sprintRegenMod;

            if (isSprintLocked && sprintTimeLeft > sprintRegenThreshold)    // End sprint cooldown
            {
                Debug.Log("regen run");
                isSprintLocked = false;
            }
        }
        if (isRegenerting && sprintTimeLeft > sprintMaxTime) // Regenerated
        {
            Debug.Log("no regen");
            isRegenerting = false;
        }

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
            CurrentState = EState.RUNNING;
            isRegenerting = false;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            CurrentState = EState.WALKING;
            isRegenerting = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            CurrentState = EState.SNEAKING;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            CurrentState = EState.WALKING;
        }
        

    }
}