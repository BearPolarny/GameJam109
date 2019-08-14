using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    //[SerializeField]
    public List<IItem> Items = new List<IItem>();
    [SerializeField]
    public string Word = null;
    // Start is called before the first frame update
    void Start()
    {
        //Items.Add(new Key(1));
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
