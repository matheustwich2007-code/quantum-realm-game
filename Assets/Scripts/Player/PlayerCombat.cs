using UnityEngine;
using QuantumRealm.UI;

namespace QuantumRealm.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [Header("Combat")]
        [SerializeField] private Camera weaponCamera;
        [SerializeField] private float shootRange = 80f;
        [SerializeField] private float fireRate = 0.12f;
        [SerializeField] private int baseDamage = 20;
        [SerializeField] private int ammoCapacity = 30;
        [SerializeField] private LayerMask hitMask;
        [SerializeField] private HUDController hud;

        private float nextShotTime;
        private int currentAmmo;
        private PlayerHealth playerHealth;
        private MobileTouchController touchController;

        private void Start()
        {
            currentAmmo = ammoCapacity;
            playerHealth = GetComponent<PlayerHealth>();
            touchController = GetComponent<MobileTouchController>();

            if (weaponCamera == null)
            {
                weaponCamera = Camera.main;
            }

            if (hud != null)
            {
                hud.SetAmmo(currentAmmo);
            }
        }

        private void Update()
        {
            bool firing = Input.GetButton("Fire1");
            if (touchController != null)
            {
                firing = firing || touchController.FirePressed;
            }

            if (firing && Time.time >= nextShotTime)
            {
                Fire();
                nextShotTime = Time.time + fireRate;
            }

            if (Input.GetKeyDown(KeyCode.R) || (touchController != null && touchController.DashPressed))
            {
                Reload();
            }
        }

        private void Fire()
        {
            if (currentAmmo <= 0)
            {
                Reload();
                return;
            }

            currentAmmo--;
            if (hud != null)
            {
                hud.SetAmmo(currentAmmo);
            }

            if (Physics.Raycast(weaponCamera.transform.position, weaponCamera.transform.forward, out RaycastHit hit, shootRange, hitMask))
            {
                var enemy = hit.collider.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    enemy.TakeDamage(baseDamage);
                }
            }
        }

        private void Reload()
        {
            currentAmmo = ammoCapacity;
            if (hud != null)
            {
                hud.SetAmmo(currentAmmo);
            }
        }
    }

    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private float damageAmount = 12f;
        [SerializeField] private Transform target;
        [SerializeField] private float chaseRange = 10f;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float attackCooldown = 1f;

        private int health;
        private float nextAttackTime;

        private void Awake()
        {
            health = maxHealth;
        }

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
                else if (Time.time >= nextAttackTime)
                {
                    AttackPlayer();
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }

        public void TakeDamage(int amount)
        {
            health -= amount;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void AttackPlayer()
        {
            var playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Damage(damageAmount);
            }
        }
    }
}
