using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    RaycastHit raycastHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform)
                {
                    IClickable Clickable;
                    if (raycastHit.transform.GetComponent<IClickable>() != null)
                    {
                        Clickable = raycastHit.transform.GetComponent<IClickable>();
                        Clickable.PerformAction();
                    }
                }
            }
        }
    }
}
