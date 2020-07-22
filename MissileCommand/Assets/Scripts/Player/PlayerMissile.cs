﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Vector2 origin = Vector2.zero;

    private Vector2 target = Vector2.zero;

    [SerializeField]
    private LineRenderer projetileLineRenderer;

    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private GameObject targetMarker;

    [SerializeField]
    private GameObject projectile;

    private bool isBeingFired = false;

    void Update()
    {
        //TODO remove this.
        if (this.isBeingFired)
        {   
            this.projectile.transform.position = Vector3.MoveTowards(this.projectile.transform.position, this.targetMarker.transform.position, speed * Time.deltaTime);
            this.projetileLineRenderer.positionCount = 2;
            this.projetileLineRenderer.SetPosition(0, this.origin);
            this.projetileLineRenderer.SetPosition(1, this.projectile.transform.position);

            if ((this.targetMarker.transform.position - this.projectile.transform.position).sqrMagnitude < 0.1f)
            {
                this.projectile.SetActive(false);

                //TODO pool explosions
                Instantiate(explosion, this.targetMarker.transform.position, Quaternion.identity);

                this.targetMarker.SetActive(false);
                this.projetileLineRenderer.positionCount = 0;
                this.isBeingFired = false;

                PlayerMissilePool.Instance.ReturnToPool(this);
            }
        }
    }

    public void FireMissileInternal(Vector2 target, Vector2 origin)
    {
        this.origin = origin;
        this.target = target;
        this.isBeingFired = true;
        this.targetMarker.SetActive(true);
        this.targetMarker.transform.position = target;
        
        this.projectile.SetActive(true);
        this.projectile.transform.position = origin;
    }
}