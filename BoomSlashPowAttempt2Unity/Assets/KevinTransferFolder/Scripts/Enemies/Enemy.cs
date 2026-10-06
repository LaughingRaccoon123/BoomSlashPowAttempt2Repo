using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    private Animator anim;
    private NavMeshAgent agent;

    [SerializeField] private float detectionRange = 12f;
    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private float turnSpeed = 5.0f;

    [SerializeField] private float health = 20f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (agent == null || !agent.enabled || !agent.isOnNavMesh) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion rotationTarget = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, turnSpeed * Time.deltaTime);
        }

        if (distanceToPlayer <= attackRange)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;

            anim.SetBool("run", false);
            anim.SetBool("walk", false);
            anim.SetBool("attacking", true);

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

        // Safety check: Only trigger animation if 'anim' isn't empty
        if (anim != null)
        {
            anim.SetBool("damage", true);
        }
        else
        {
            // This will print out the exact name of the broken object in the Console
            Debug.LogError($"[ANIMATOR MISSING] The object named '{gameObject.name}' is missing an Animator component!", this);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
