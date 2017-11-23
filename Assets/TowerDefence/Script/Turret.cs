using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    [Header("General")]

    private Transform Target;
    private Enemy targetEnemy;

    public float range = 15f;
    [Header("Use Bullets")]
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public GameObject bulletPrefab;
    public GameObject shootEffect;
    [Header("Use Laser")]
    public ParticleSystem particleImpact;
    public LineRenderer lineRender;
    public Light impactLight;
    public int damageOverTime= 30;
    public float slowPct = 0.5f;
    public bool useLaser = false;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

   
    public Transform firePoint;

    public float turnSpeed = 10f;
    public Transform partToRotate;

   



	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        


        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
                targetEnemy = nearestEnemy.GetComponent<Enemy>();
            }

        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            Target = nearestEnemy.transform;

        }
        else
            Target = null;


    }


	void Update () {



        if (Target == null)
        {
            if(useLaser)
            {
                if(lineRender.enabled)
                {
                    lineRender.enabled = false;
                    particleImpact.Stop();
                    impactLight.enabled = false;
                }

            }
            return;
        }

        if (!LockOnTarget())
            return;

        if(useLaser)
        {
            Laser();
        }
        else
        {
         if (fireCountdown <= 0f)
                {
                    Shoot();
                    fireCountdown = 1f / fireRate;
                }

                fireCountdown -= Time.deltaTime;
        }

       


	}
    bool LockOnTarget()
    {

        //Target lock on
        Vector3 dir = Target.position - partToRotate.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation ,lookRotation,Time.deltaTime*turnSpeed).eulerAngles;
        
        partToRotate.rotation = Quaternion.Euler(rotation.x,rotation.y, 0);

        if (Vector3.Angle(partToRotate.transform.forward, dir) <10)   // Checks is  Part To Rotate  looking on target
                return true;                                         // Return True If Part To Rotate Is looking on target

        return false;
    }


    void Laser()
    {
        targetEnemy.TakeDamage( (int)(damageOverTime*Time.deltaTime*3) );
        targetEnemy.Slow(slowPct);

        if (!lineRender.enabled)
        {
            particleImpact.Play();  
            lineRender.enabled = true;
            impactLight.enabled = true;
        }

        lineRender.SetPosition(0, firePoint.position);
        lineRender.SetPosition(1, Target.position);

        Vector3 dir = firePoint.position - Target.position;
        particleImpact.transform.position = Target.position+dir.normalized* 0.5f ;
        particleImpact.transform.rotation = Quaternion.LookRotation(dir);
       
    }

  

        void Shoot()
    {
        GameObject _shootEffect = (GameObject)Instantiate(shootEffect, firePoint.position, firePoint.rotation);
        GameObject _bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bullet = _bulletGO.GetComponent<Bullet>();
        Destroy(_shootEffect,2f);

        if(bullet!=null)
        {
            bullet.Seek(Target);
            
        }
            
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
       
    }

}
