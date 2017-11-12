﻿
using UnityEngine;

public class Bullet : MonoBehaviour {


    private Transform target;



    public GameObject BulletImpactEffect;


    public int damage = 5;
    public float speed = 70f;
    public float explosionRadius = 15f;

    public void Seek(Transform _target)
    {
        target = _target;

    }

	// Update is called once per frame
	void Update () {

        if(target==null)
        {
            Destroy(gameObject);
                return;
        }

        Vector3 dir = target.position-transform.position;

        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude<= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        transform.LookAt(target);

	}

    void HitTarget()
    {

        GameObject effectIns= (GameObject) Instantiate(BulletImpactEffect, transform.position, transform.rotation);
        Destroy(effectIns,5f);
        if(explosionRadius>0f)
        {

            Explode();
        }else
        {
            Damage(target);

        }
        
        Destroy(gameObject);
      

    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position , explosionRadius);
        foreach(Collider collider in colliders)
        {
            if (collider.tag == "enemy")
            {
                Damage(collider.transform);
            }
        }

    }

    void Damage(Transform enemy)
    {
         Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
