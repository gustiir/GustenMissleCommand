﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Vector2 origin = Vector2.zero;

    private Vector2 target = Vector2.zero;

    [SerializeField]
    private LineRenderer projetileLineRenderer;

    [SerializeField]
    private GameObject targetMarker;

    [SerializeField]
    private GameObject projectile;

    private bool isBeingFired = false;

    void Update()
    {
        if (isBeingFired)
        {
            projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, targetMarker.transform.position, speed * Time.deltaTime);
            projetileLineRenderer.positionCount = 2;
            projetileLineRenderer.SetPosition(0, origin);
            projetileLineRenderer.SetPosition(1, projectile.transform.position);

            if ((targetMarker.transform.position - projectile.transform.position).sqrMagnitude < 0.1f)
            {
                ExplodeAndReturnToPool();
            }
        }
    }

    public void FireMissileInternal(Vector2 target, Vector2 origin)
    {
        this.origin = origin;
        this.target = target;
        isBeingFired = true;
        targetMarker.SetActive(true);
        targetMarker.transform.position = target;

        projectile.SetActive(true);
        projectile.transform.position = origin;
    }

    public void ExplodeAndReturnToPool()
    {
        projectile.SetActive(false);

        var enemyExplosion = EnemyExplosionPool.Instance.Get();
        enemyExplosion.transform.position = projectile.transform.position;
        enemyExplosion.gameObject.SetActive(true);

        targetMarker.SetActive(false);
        projetileLineRenderer.positionCount = 0;
        isBeingFired = false;

        EnemyMissilePool.Instance.ReturnToPool(this);
    }
}
