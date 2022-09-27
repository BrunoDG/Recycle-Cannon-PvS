using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMovement : MonoBehaviour
{
    public float speed = 10f;
    private float rayRange = 20.0f;
    private Transform target;

    private GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        wall = GameObject.FindGameObjectWithTag("CityWall");
        target = wall.transform;
        InvokeRepeating("FollowScavenger", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * speed, Space.World);
    }

    void FollowScavenger()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float shortestDistance = Mathf.Infinity;
        GameObject nearPlayer = null;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < shortestDistance)
        {
            shortestDistance = distanceToPlayer;
            nearPlayer = player;
        }

        if (nearPlayer != null && shortestDistance <= rayRange)
        {
            target = nearPlayer.transform;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            DamagePlayer();
        } else if (other.gameObject.tag == "CityWall")
        {
            DamageWall();
        }
    }

    void DamageWall()
    {
        
    }

    void DamagePlayer()
    {

    }

    // Draws the enemy's cone FOV to find and follow the Character.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        float totalFOV = 70.0f;
        float halfFOV = totalFOV / 2.0f;

        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);

        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;

        Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);

    }

}
