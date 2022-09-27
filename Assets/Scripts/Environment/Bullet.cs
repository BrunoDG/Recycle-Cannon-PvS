using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public GameObject impactEffect;
    private Transform target;
    private Vector3 direction;

    public void Seek(Vector3 _direction, Transform _target)
    {
        direction = _direction;
        target = _target;
    }

    void Update()
    {
        Vector3 dir = direction - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame && target != null)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Destroy(gameObject);
        
        if (!target.gameObject.tag.Equals("FireTarget"))
        {
            GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2f);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        target = col.transform;
        if (col.gameObject.tag == "Organic" ||
            col.gameObject.tag == "Plastic" ||
            col.gameObject.tag == "Metal" )
        {
            Damage(target, gameObject.tag);
        }
    }

    void Damage(Transform enemy, string type)
    {
        Alien al = enemy.GetComponent<Alien>();
        
        if (al != null)
        {
            if (
                (al.tag == "Metal" ||
                 al.tag == "Plastic") &&
                 PlayerStats.AmmoType == "Organic")
            {
                al.TakeDamage(1f);
                return;
            } else if (al.tag == "Organic" && (
                    PlayerStats.AmmoType == "Metal" ||
                    PlayerStats.AmmoType == "Plastic")
                )
            {
                al.TakeDamage(1f);
                return;
            } else if (al.tag == "Boss") {
                al.TakeDamage(1f);
            } else
            {
                return;
            }
        }
    }
}
