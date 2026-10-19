using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public Transform player;
    private Animator anim;
    private NavMeshAgent agent;

    [SerializeField] private float detectionRange = 12f;
    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private float turnSpeed = 5.0f;

    [SerializeField] private float health = 20f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float attackCooldown = 1.5f;
    private float distanceToPlayer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (agent == null || !agent.enabled || !agent.isOnNavMesh) return;

        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion rotationTarget = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, turnSpeed * Time.deltaTime);
        }

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer(damage);

        } else if (distanceToPlayer <= detectionRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);

            anim.SetBool("run", true);
            anim.SetBool("walk", false);
            anim.SetBool("attacking", false);
        } else if (distanceToPlayer > detectionRange)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;

            anim.SetBool("run", false);
            anim.SetBool("walk", false);
            anim.SetBool("attacking", false);
        }
    }

    public void EnemyTakeDamage(float damage)
    {
        health -= damage;

        if (anim != null)
        {
            anim.SetBool("damage", true);
        }

        if (health <= 0)
        {
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        if (anim != null)
        {
            anim.SetBool("dead", true);
        }
        agent.enabled = false;

        Collider collider = GetComponent<Collider>();

        if (collider != null)
        {
            collider.enabled = false;
        }

        Destroy(gameObject, 1f);
    }

    private void AttackPlayer(float damage)
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        anim.SetBool("run", false);
        anim.SetBool("walk", false);
        anim.SetBool("attacking", true);

        if (distanceToPlayer <= attackRange)
        {
            playerHealth.PlayerTakeDamage(damage);
            WaitForFrames(42);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    IEnumerator WaitForFrames(int frames)
    {
        while (frames > 0)
        {
            frames--;
            yield return null;
        }
    }
}
