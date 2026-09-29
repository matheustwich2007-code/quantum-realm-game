using UnityEngine;

namespace QuantumRealm.Player
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private WeaponDefinition[] weapons;
        [SerializeField] private int currentWeaponIndex;

        public void SwitchWeapon(int index)
        {
            if (index < 0 || index >= weapons.Length)
            {
                return;
            }

            currentWeaponIndex = index;
            Debug.Log($"Equipped weapon: {weapons[currentWeaponIndex].Name}");
        }

        public WeaponDefinition GetCurrentWeapon()
        {
            return weapons[currentWeaponIndex];
        }
    }

    [System.Serializable]
    public class WeaponDefinition
    {
        public string Name;
        public int Damage;
        public float FireRate;
        public int Ammo;
    }
}
