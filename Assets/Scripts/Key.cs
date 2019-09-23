using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Key: MonoBehaviour, IItem, IUsable
{
    public int Id;

    public Key(int id)
    {
        Id = id;
    }

    public void PerformAction()
    {
        //Debug.Log("w Kluczyk ");
        Equipment equipment = GameObject.Find("Player").GetComponent<Equipment>();
        equipment.Items.Add(this);
        Destroy(GameObject.Find(name));

    }
}
