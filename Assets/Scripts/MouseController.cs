using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MouseController : MonoBehaviour
{
    RaycastHit raycastHit;
    Camera PlayerCamera;

    [SerializeField]
    private float ClickDistance = 1.5f;
    int layerMask = 1 << 16;
    private readonly int RMB = 1;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCamera = gameObject.GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.yellow);

        if (Input.GetMouseButtonDown(RMB))
        {
            Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit, ClickDistance, layerMask))
            {
                if (raycastHit.transform)
                {
                    //Debug.Log(raycastHit.transform.gameObject);
                    IUsable Clickable = raycastHit.transform.GetComponent<IUsable>();
                    //Debug.Log(Clickable);
                    if (Clickable != null)
                    {
                        if (typeof(IInteractionable).IsAssignableFrom(Clickable.GetType()))
                        {
                            //Debug.Log("Interactionable");
                            ((IInteractionable)Clickable).PerformInteraction();
                        } else {
                            //Debug.Log("Usable");
                            Clickable.PerformAction();
                        }
                    }
                }
            }
        }
    }
}
