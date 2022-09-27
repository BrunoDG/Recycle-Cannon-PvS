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
}
