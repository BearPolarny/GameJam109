using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour, IClickable
{
    public bool used = false;
    public bool opening = false, closing = false;
    public Transform pivot;
    
    public bool PerformAction()
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
        return !used;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (opening)
        {
            transform.RotateAround(pivot.position, Vector3.down, Time.deltaTime*100);
            //Debug.Log(transform.rotation.y);

            if(transform.rotation.y < -0.7)
            {
                //Debug.Log("Kuniec");
                opening = false;
            }
        }
        else if (closing)
        {
            transform.RotateAround(pivot.position, Vector3.up, Time.deltaTime * 100);
            if (transform.rotation.y > 0)
            {
                closing = false;
                
            }
        }
    }


}