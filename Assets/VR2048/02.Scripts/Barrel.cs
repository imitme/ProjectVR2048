﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private string collisionTagNameforExplosion = "BULLET";

    public GameObject expEffect;
    public int limitHitCount = 3;
    [SerializeField] private float expTimeMin = 0.0f;
    [SerializeField] private float expTimeMax = 0.0f;
    public Mesh[] meshes;

    private int hitCount = 0;
    private Rigidbody rb;
    private float expTime = 0;
    private GameObject barrel = null;
    private MeshFilter meshFilter;

    private void Start()
    {
        barrel = this.gameObject;
        rb = GetComponent<Rigidbody>();

        expTimeMin = 3.0f;
        expTimeMax = expTimeMin * 2.0f;
        expTime = Random.Range(expTimeMin, expTimeMax);

        meshFilter = GetComponent<MeshFilter>();
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag(collisionTagNameforExplosion))
        {
            CollBarrel();
            if (++hitCount == limitHitCount) { StartCoroutine(ExpBarrelProcess()); }
        }
    }

    private void CollBarrel()
    {
        int idx = Random.Range(0, meshes.Length);
        meshFilter.sharedMesh = meshes[idx];
    }

    private IEnumerator ExpBarrelProcess()
    {
        MoveBarrel();
        yield return new WaitForSeconds(expTime);
        HideBarrelEffects();
        ShowExpEffect();
        Destroy(barrel, 2.0f);
    }

    private void MoveBarrel()
    {
        rb.isKinematic = false;
    }

    private void ShowExpEffect()
    {
        Instantiate(expEffect, transform.position, Quaternion.identity);
    }

    private void HideBarrelEffects()
    {
        Transform[] bulletImpacts = GetComponentsInChildren<Transform>();
        foreach (var billetImpact in bulletImpacts)
        {
            billetImpact.gameObject.SetActive(false);
        }
        barrel.GetComponent<MeshRenderer>().enabled = false;
    }
}