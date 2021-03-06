﻿using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class CommetLogic : MonoBehaviour
{

    public float movementRadious,spinSpeed;
    private float currentAngle;
    private Vector3 orign;

    void Start()
    {
        transform.position = new Vector3(movementRadious,0);
        transform.position += orign;
        currentAngle = 0;
        spinSpeed *= Mathf.Deg2Rad;
        
    }

    public Vector3 Orign
    {
        set
        {
            orign = value;
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void moveNormal()
    {
        transform.position -= new Vector3(movementRadious * Mathf.Cos(currentAngle), movementRadious * Mathf.Sin(currentAngle));
        currentAngle += spinSpeed;
        transform.position += new Vector3(movementRadious * Mathf.Cos(currentAngle), movementRadious * Mathf.Sin(currentAngle));
        //transform.position += GetComponentInParent<Transform>().position; ;
    }

    public void defend()
    {
        
    }



    internal void CollidedWithPlayer(Collider2D other)
    {

        var collisionController = other.GetComponent<CollisionController>();
        collisionController.OnCollisionEnter2DManual(gameObject);
    }
}
