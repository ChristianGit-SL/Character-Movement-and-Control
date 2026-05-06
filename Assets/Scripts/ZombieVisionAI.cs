using UnityEngine;
using UnityEngine.AI;

public class ZombieVisionAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [Tooltip("Layers that BLOCK vision (Walls, Environment, etc). Do NOT include Player or Enemy.")]
    [SerializeField] private LayerMask obstructionMask;

    [Header("Vision")]
    [SerializeField] private float viewDistance = 12f;
    [SerializeField, Range(0, 180)] private float viewAngle = 90f;
    [SerializeField] private float loseSightDelay = 2f;

    [Header("Attack")]
    [SerializeField] private float attackRange = 1.6f;
    [SerializeField] private float attackCooldown = 1.2f;

    [Header("Heights (for LOS ray)")]
    [SerializeField] private float zombieEyeHeight = 1.6f;
    [SerializeField] private float playerTargetHeight = 1.2f;

    private NavMeshAgent agent;
    private Animator anim;

    private float lastSeenTime = -999f;
    private float lastAttackTime = -999f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player == null) return;

        bool canSee = CanSeePlayer();
        if (canSee) lastSeenTime = Time.time;

        bool chasing = (Time.time - lastSeenTime) <= loseSightDelay;

        if (chasing)
        {
            float dist = Vector3.Distance(transform.position, player.position);

            if (dist > attackRange)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
            else
            {
                agent.isStopped = true;

                FaceTarget(player.position);

                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    anim.SetTrigger("Attack");
                    lastAttackTime = Time.time;
                }
            }
        }
        else
        {
            agent.isStopped = true;
        }

        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void FaceTarget(Vector3 worldPos)
    {
        Vector3 look = worldPos - transform.position;
        look.y = 0;
        if (look.sqrMagnitude < 0.001f) return;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(look),
            Time.deltaTime * 10f
        );
    }

    private bool CanSeePlayer()
    {
        Vector3 toPlayer = player.position - transform.position;
        float dist = toPlayer.magnitude;

        if (dist > viewDistance) return false;

        Vector3 dir = toPlayer.normalized;

        // FOV cone (behind = not noticed)
        float angle = Vector3.Angle(transform.forward, dir);
        if (angle > viewAngle * 0.5f) return false;

        // LOS check (blocked by environment)
        return HasLineOfSight();
    }

    private bool HasLineOfSight()
    {
        Vector3 eye = transform.position + Vector3.up * zombieEyeHeight;
        Vector3 target = player.position + Vector3.up * playerTargetHeight;

        Vector3 toTarget = target - eye;
        float dist = toTarget.magnitude;

        // If we hit ANY obstruction between eye and target, LOS is blocked.
        bool blocked = Physics.Raycast(eye, toTarget.normalized, dist, obstructionMask, QueryTriggerInteraction.Ignore);

        // Debug line (Scene view)
        Debug.DrawLine(eye, target, blocked ? Color.red : Color.green);

        return !blocked;
    }

    private void OnDrawGizmosSelected()
    {
        // Vision radius
        Gizmos.color = new Color(1f, 1f, 0f, 0.2f);
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        // Vision cone edges
        Vector3 left = DirFromAngle(-viewAngle * 0.5f);
        Vector3 right = DirFromAngle(viewAngle * 0.5f);

        Vector3 eye = transform.position + Vector3.up * zombieEyeHeight;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(eye, eye + left * viewDistance);
        Gizmos.DrawLine(eye, eye + right * viewDistance);
    }

    private Vector3 DirFromAngle(float angleDegrees)
    {
        float rad = (transform.eulerAngles.y + angleDegrees) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
    }
}