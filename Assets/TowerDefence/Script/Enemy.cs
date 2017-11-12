using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float startSpeed = 10f;
    [HideInInspector]
    public float speed = 10f;

    public int health = 100;
    public int value = 50;

    

    public GameObject DestroyEffect;

    private void Start()
    {
        speed = startSpeed;
    }


    public void TakeDamage(int amount)
    {
        health -= amount;
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
