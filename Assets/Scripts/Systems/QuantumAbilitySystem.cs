using UnityEngine;

namespace QuantumRealm.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float chaseRange = 10f;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float attackCooldown = 1f;

        private float attackTimer;

        private void Update()
        {
            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag("Player")?.transform;
                return;
            }

            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= chaseRange)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                direction.y = 0f;

                if (distance > attackRange)
                {
                    transform.position += direction * moveSpeed * Time.deltaTime;
                }
                else
                {
                    attackTimer -= Time.deltaTime;
                    if (attackTimer <= 0f)
                    {
                        Attack();
                        attackTimer = attackCooldown;
                    }
                }
            }
        }

        private void Attack()
        {
            Debug.Log("Inimigo atacou o jogador");
        }
    }
}
