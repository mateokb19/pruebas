using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private float autoPatrolRange = 3f;

    public float speed = 2f;
    public float chaseSpeed = 3f;
    public float chaseRange = 4f;
    public float maxHealth = 3f;

    [System.NonSerialized] public bool canMove = true;

    private float health;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform player;
    private CatTransform _playerCat;

    private Transform patrolA;
    private Transform patrolB;
    private Transform currentTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = maxHealth;

        if (chaseSpeed <= 0f) chaseSpeed = speed;
        if (chaseRange <= 0f) chaseRange = 4f;

        SetupPatrolPoints();
    }

    private void SetupPatrolPoints()
    {
        if (pointA != null && pointB != null)
        {
            patrolA = pointA.transform;
            patrolB = pointB.transform;
        }
        else
        {
            // Genera dos puntos automáticos a ambos lados de la posición inicial
            GameObject autoA = new GameObject("PatrolA_auto");
            GameObject autoB = new GameObject("PatrolB_auto");
            autoA.transform.position = transform.position + Vector3.left * autoPatrolRange;
            autoB.transform.position = transform.position + Vector3.right * autoPatrolRange;
            patrolA = autoA.transform;
            patrolB = autoB.transform;
        }

        currentTarget = patrolB;
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            SetWalking(false);
            return;
        }

        if (player == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null)
            {
                player = obj.transform;
                _playerCat = obj.GetComponent<CatTransform>();
            }
        }

        bool playerVisible = player != null &&
                             (_playerCat == null || !_playerCat.IsCat) &&
                             Mathf.Abs(player.position.x - transform.position.x) <= chaseRange;

        if (playerVisible)
            Chase();
        else
            Patrol();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
            Destroy(gameObject);
    }

    private void Chase()
    {
        float dir = player.position.x - transform.position.x;
        rb.velocity = new Vector2(Mathf.Sign(dir) * chaseSpeed, rb.velocity.y);
        FaceDirection(dir);
        SetWalking(true);
    }

    private void Patrol()
    {
        if (Mathf.Abs(transform.position.x - currentTarget.position.x) < 0.2f)
            currentTarget = currentTarget == patrolB ? patrolA : patrolB;

        float dir = currentTarget.position.x - transform.position.x;
        rb.velocity = new Vector2(Mathf.Sign(dir) * speed, rb.velocity.y);
        FaceDirection(dir);
        SetWalking(true);
    }

    private void FaceDirection(float dir)
    {
        transform.eulerAngles = dir >= 0 ? Vector3.zero : new Vector3(0, 180, 0);
    }

    private void SetWalking(bool walking)
    {
        if (animator != null)
            animator.SetBool("IsWalking", walking);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        if (pointA == null || pointB == null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position + Vector3.left * autoPatrolRange,
                            transform.position + Vector3.right * autoPatrolRange);
        }
    }
}
