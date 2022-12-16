using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceItem : Item
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        //
        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("ice trigged");
            
        }
        
    }
}
