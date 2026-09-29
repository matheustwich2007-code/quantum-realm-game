using UnityEngine;

namespace QuantumRealm.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private Camera weaponCamera;
        [SerializeField] private float shootRange = 80f;
        [SerializeField] private float fireRate = 0.12f;
        [SerializeField] private int baseDamage = 20;
        [SerializeField] private int ammoCapacity = 30;
        [SerializeField] private LayerMask hitMask;

        private float nextShotTime;
        private int currentAmmo;

        private void Start()
        {
            currentAmmo = ammoCapacity;
            if (weaponCamera == null)
            {
                weaponCamera = Camera.main;
            }
        }

        private void Update()
        {
            if (Input.GetButton("Fire1") && Time.time >= nextShotTime)
            {
                Fire();
                nextShotTime = Time.time + fireRate;
            }

            if (Input.GetKeyDown(KeyCode.R))
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
            Debug.Log("Arma recarregada");
        }
    }

    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        private int health;

        private void Awake()
        {
            health = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            health -= amount;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
