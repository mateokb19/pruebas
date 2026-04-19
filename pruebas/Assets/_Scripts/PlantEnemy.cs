using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEnemy : MonoBehaviour
{
    [SerializeField] private Transform shootPoint; // Punto donde se disparan las balas
    [SerializeField] private GameObject bulletPrefab; // Prefab de la bala
    [SerializeField] private float shootInterval = 2f; // Tiempo entre disparos (en segundos)
    [SerializeField] private float detectionRange = 5f; // Rango para detectar al jugador

    private Animator animator;
    private float shootTimer;
    private Transform playerTransform;

    void Start()
    {
        animator = GetComponent<Animator>();
        shootTimer = shootInterval;

        // Busca al jugador en la escena
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        // Calcula la distancia al jugador
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Flipea la planta según la dirección del jugador
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        if (directionToPlayer.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Mira a la derecha
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // Mira a la izquierda
        }

        // Si el jugador está en rango, cuenta el tiempo para disparar
        if (distanceToPlayer <= detectionRange)
        {
            shootTimer -= Time.deltaTime;

            if (shootTimer <= 0)
            {
                Shoot();
                shootTimer = shootInterval;
            }
        }
    }

    void Shoot()
    {
        Debug.Log("Iniciando disparo");

        // Activa la animación de disparo
        if (animator != null)
        {
            Debug.Log("Animator encontrado - activando IsShooting");
            animator.SetBool("IsShooting", true);
        }
        else
        {
            Debug.Log("ADVERTENCIA: Animator es NULL");
        }

        // Obtén la dirección al jugador
        Vector2 directionToPlayer = (playerTransform.position - shootPoint.position).normalized;

        // Instancia la bala
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

        // Calcula el ángulo para rotar la bala hacia el jugador
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Pasa la dirección al script de la bala
        PlantBullet plantBullet = bullet.GetComponent<PlantBullet>();
        if (plantBullet != null)
        {
            plantBullet.SetDirection(directionToPlayer);
        }

        // Destruye la bala después de 3 segundos
        Destroy(bullet, 3f);

        StartCoroutine(ResetShootAnimation());
    }

    private IEnumerator ResetShootAnimation()
    {
        yield return new WaitForSeconds(1f); // Tiempo que dura tu animación de disparo
        Debug.Log("Volviendo a Idle - IsShooting = false");
        animator.SetBool("IsShooting", false);
    }
}
