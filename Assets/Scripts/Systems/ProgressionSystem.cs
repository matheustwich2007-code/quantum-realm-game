using UnityEngine;

namespace QuantumRealm.Systems
{
    public abstract class QuantumAbility
    {
        public string Name { get; protected set; }
        public float Cooldown { get; protected set; }
        public int EnergyCost { get; protected set; }

        public virtual bool CanUse(int currentEnergy)
        {
            return currentEnergy >= EnergyCost;
        }

        public abstract void Execute(GameObject owner);
    }

    public class TimeSlowAbility : QuantumAbility
    {
        public TimeSlowAbility()
        {
            Name = "Time Slow";
            Cooldown = 10f;
            EnergyCost = 25;
        }

        public override void Execute(GameObject owner)
        {
            Time.timeScale = 0.5f;
            Debug.Log("Habilidade: Time Slow ativada");
        }
    }

    public class QuantumLeapAbility : QuantumAbility
    {
        public QuantumLeapAbility()
        {
            Name = "Quantum Leap";
            Cooldown = 12f;
            EnergyCost = 30;
        }

        public override void Execute(GameObject owner)
        {
            if (owner == null) return;
            owner.transform.position += owner.transform.forward * 4f;
            Debug.Log("Habilidade: Quantum Leap ativada");
        }
    }
}
