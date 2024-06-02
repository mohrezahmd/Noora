using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCollider : PowerUp
{

    protected override void OnEnable()
    {
        //rgbd2D = GetComponent<Rigidbody2D>();
        //rgbd2D.velocity = new Vector2(0, -verticalSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
           
        }
    }
}
