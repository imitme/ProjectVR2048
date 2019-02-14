using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    private string collisionTagNameforDestroy = "BULLET";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == collisionTagNameforDestroy)
        {
            Destroy(collision.gameObject);
        }
    }
}