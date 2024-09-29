using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : EnemyController
{
    public Transform target;

    public float speed = 200.0f;
    public float nextWaypointDistance = 3.0f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    public float aggroRadius = 10.0f;
    Seeker seeker;

    Rigidbody2D rb;

     private void Start()
    {
        base.Start();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        // Adiciona um CircleCollider2D para a área de aggro
        CircleCollider2D aggroCollider = gameObject.AddComponent<CircleCollider2D>();
        aggroCollider.isTrigger = true;
        aggroCollider.radius = aggroRadius;
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
        UpdateLifeBar();

        if (target == null)
    {
            path = null;
            gameObject.GetComponent<AIDestinationSetter>().enabled = false;
            gameObject.GetComponent<AIDestinationSetter>().target = null;
            return;
    }
    if (path == null || path.vectorPath == null)
    {
        Debug.LogWarning("Path is null");
        return;
    }

    if (rb == null)
    {
        Debug.LogError("Rigidbody2D is null");
        return;
    }
    try{

    if (currentWaypoint >= path.vectorPath.Count)
    {
        reachedEndOfPath = true;
        return;
    }
    else
    {
        reachedEndOfPath = false;
    }
    }
    catch (System.Exception e)
    {
        //Debug.LogError(e);
    }

    Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
    Vector2 force = direction * speed * Time.deltaTime;

    rb.AddForce(force);

    float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

    if (distance < nextWaypointDistance)
    {
        currentWaypoint++;
    }

    if (rb.velocity.x >= 0.01f)
    {
        transform.localScale = new Vector3(-1f, 1f, 1f);
    }
    else if (rb.velocity.x <= -0.01f)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    if (Life <= 0)
    {
        Destroy(gameObject);
    }

}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered aggro range");
            target = collision.transform;
            gameObject.GetComponent<AIDestinationSetter>().enabled = true;
            gameObject.GetComponent<AIDestinationSetter>().target = collision.gameObject.transform;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exited aggro range");

            target = null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with enemy");
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            Player.Instance.TakeDamage(enemy.damage, knockbackDirection, 2);
        }
    }
}
