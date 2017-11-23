
using UnityEngine;

public class Bullet : MonoBehaviour {


    private Transform target;
    private Quaternion lookRotation;

    public GameObject bulletDestroyEffect;
    public GameObject bulletImpactEffect;

    
    public int damage = 5;
    public float speed = 70f;
    public float explosionRadius = 15f;

    public void Seek(Transform _target)
    {
        target = _target;
        lookRotation = new Quaternion(0, 0, 0, 1);
    }

	// Update is called once per frame
	void Update () {
        

        if (target==null)
        {
            
            Destroy(gameObject);
            DestroyEffect(lookRotation);
                return;
        }

       
        
        Vector3 dir = target.position-transform.position;
        lookRotation = Quaternion.LookRotation(dir);

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

        GameObject effectIns= (GameObject) Instantiate(bulletImpactEffect, transform.position, transform.rotation);
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

    void DestroyEffect(Quaternion _lookRotation)
    {


        GameObject _destroyEffect = (GameObject)Instantiate(bulletDestroyEffect, transform.position, _lookRotation);
        Destroy(_destroyEffect, 5f);

            

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
