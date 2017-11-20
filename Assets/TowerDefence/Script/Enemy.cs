﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    
    [HideInInspector]
    public float speed = 10f;

    [Header("Values")]
    public float startSpeed = 10f;
    public int startHealth = 100;
    private float health;
    public int value = 50;

    

    public GameObject DestroyEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }


    public void TakeDamage(int amount)
    {
        health -= amount;
        healthBar.fillAmount = health/health;
        if(health <=0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.Money += value;
        Destroy(gameObject);

        GameObject destroyEffectOB= (GameObject) Instantiate(DestroyEffect, gameObject.transform.position , Quaternion.identity);

        Destroy(destroyEffectOB, 5f);
    }

    public void Slow(float pct)
    {

        speed = startSpeed * (1f-pct);


    }
}
