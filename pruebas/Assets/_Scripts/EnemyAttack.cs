using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float detectionRange = 3f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float attackCooldown = 1.5f;

    private EnemyMovement movement;
    private Animator animator;
    private bool isAttacking;
    private float cooldownTimer;

    void Start()
    {
        movement = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();

        if (movement == null)
            Debug.LogError("[EnemyAttack] No se encontró EnemyMovement en el mismo GameObject.", this);
        if (animator == null)
            Debug.LogError("[EnemyAttack] No se encontró Animator en el mismo GameObject.", this);
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (isAttacking) return;

        Collider2D player = DetectPlayer();

        if (player != null && cooldownTimer <= 0f)
        {
            float dist = Vector2.Distance(transform.position, player.transform.position);

            if (dist <= attackRange)
                StartCoroutine(PerformAttack());
        }
    }

    private Collider2D DetectPlayer()
    {
        Collider2D[] nearby = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        foreach (Collider2D col in nearby)
        {
            if (!col.CompareTag("Player")) continue;

            CatTransform cat = col.GetComponent<CatTransform>();
            if (cat != null && cat.IsCat) continue;

            return col;
        }
        return null;
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;
        cooldownTimer = attackCooldown;

        if (animator != null)
            animator.SetTrigger("Attack");

        if (movement != null)
            movement.canMove = false;

        // Frame del impacto
        yield return new WaitForSeconds(0.3f);

        Collider2D hit = DetectPlayer();

        if (hit != null && Vector2.Distance(transform.position, hit.transform.position) <= attackRange)
        {
            HealthManaUI.TakeDamage(damage);
            Debug.Log($"[EnemyAttack] Puñetazo! -{damage} | Vida: {PlayerStats.health}");
        }

        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
        if (movement != null)
            movement.canMove = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
