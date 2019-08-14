using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DoorBehavior : MonoBehaviour, IInteractionable
{
    [SerializeField]
    bool locked = true;

    [SerializeField]
    private bool used = false;
    private bool opening = false, closing = false;
    [SerializeField]
    private Transform pivot;
    public int Id;
    


    [SerializeField]
    private int RotateSpeed = 100;
    [SerializeField]
    private float RotationEnd = 0f;
    [SerializeField]
    private float RotationStart = 0f;
    [SerializeField]
    private float rot = 0f;

    BoxCollider BoxCollider;
    public void PerformAction()
    {

        BoxCollider.enabled = false;

        if (used)
        {
            //Debug.LogWarning("used");
            closing = true;
            opening = false;
            used = false;


        }
        else
        {
            //Debug.LogWarning("unused");
            used = true;
            opening = true;
            closing = false;
        }
    }
    public void PerformInteraction()
    {

        // Zawsze potem uzywaj normalnie
        if (!locked)
        {
            //Debug.LogWarning("unlocked");
            PerformAction();
            return;
        }

        // Sprawdz czy jest kluczyk
        Equipment equipment = GameObject.Find("Player").GetComponent<Equipment>();
        Key key;
        try
        {
            key = (Key)equipment.Items.Find(x => (x.GetType() == typeof(Key) && ((Key)x).Id == Id));
            Debug.Log(key.Id);
        // Jezeli jest to uzyj i odblokuj drzwi
            locked = false;
            equipment.Items.Remove(key);
            Debug.Log("Interakcja");
            
        } catch (System.Exception)
        {
            Debug.LogWarning("Brak Klucza");

        }

        //if (key != null)
        //{
        //    locked = false;
        //}




    }

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider = GetComponent<BoxCollider>();
        RotationStart = transform.rotation.y;
        RotationEnd = RotationStart - 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        if (opening)
        {
            transform.RotateAround(pivot.position, Vector3.down, Time.deltaTime * RotateSpeed);
            //Debug.Log(transform.rotation.y);

            if (transform.rotation.y < RotationEnd)
            {
                //Debug.Log("Kuniec");
                opening = false;
                BoxCollider.enabled = true;
            }
        }
        else if (closing)
        {
            transform.RotateAround(pivot.position, Vector3.up, Time.deltaTime * RotateSpeed);
            if (transform.rotation.y > RotationStart)
            {
                closing = false;
                BoxCollider.enabled = true;

            }
        }
        rot = transform.rotation.y;
    }

}