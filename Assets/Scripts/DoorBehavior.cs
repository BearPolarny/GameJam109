using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour, IUsable
{
    public bool used = false;
    public bool opening = false, closing = false;
    public Transform pivot;

    [SerializeField]
    private int RotateSpeed = 100;
    [SerializeField]
    private float RotationEnd = -0.7f;

    public void PerformAction()
    {
        if (used)
        {
            closing = true;
            opening = false;
            used = false;


        } else
        {
            used = opening = true;
            closing = false;
        }
    }

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if (opening)
        {
            transform.RotateAround(pivot.position, Vector3.down, Time.deltaTime * RotateSpeed);
            //Debug.Log(transform.rotation.y);

            if(transform.rotation.y < RotationEnd)
            {
                //Debug.Log("Kuniec");
                opening = false;
            }
        }
        else if (closing)
        {
            transform.RotateAround(pivot.position, Vector3.up, Time.deltaTime * RotateSpeed);
            if (transform.rotation.y > 0)
            {
                closing = false;
                
            }
        }
    }


}