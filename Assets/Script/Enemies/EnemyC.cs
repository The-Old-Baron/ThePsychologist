using UnityEngine;
using Pathfinding;

public class EnemyC : EnemyController
{
    public Transform target;
    public float aggroRange = 10f;
    public float shootInterval = 0.8f;
    public float shootRange = 8f;
    public GameObject arrowPrefab;
    public Transform shootPoint;

    private Seeker seeker;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private float nextShootTime;
    private Rigidbody2D rb;

    void Start()
    {
        base.Start();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget < aggroRange)
        {
            if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector3 direction = ((Vector3)path.vectorPath[currentWaypoint] - transform.position).normalized;
            Vector3 move = direction * Time.deltaTime;
            rb.MovePosition(transform.position + move);

            float distance = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

            if (distance < 1f)
            {
                currentWaypoint++;
            }

            if (distanceToTarget <= shootRange && Time.time >= nextShootTime)
            {
                Shoot();
                nextShootTime = Time.time + shootInterval;
            }
        }

        void Shoot()
        {
            Vector2 direction = (target.position - shootPoint.position).normalized;

            GameObject projectile = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = direction * 5;
        }
    }
}
