using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    
    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
   {
        if (collider.gameObject.tag == "Map")
        {
            Destroy(this.gameObject);
        }
        if (collider.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

}