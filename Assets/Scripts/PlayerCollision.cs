using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerController movement;

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        // will disbale movement if player hits obstacle
        if (collisionInfo.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle Hit");
            movement.enabled = false;
        }
    }
}