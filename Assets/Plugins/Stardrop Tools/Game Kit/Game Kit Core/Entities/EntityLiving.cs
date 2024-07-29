using UnityEngine;

namespace StardropTools.GameKit
{
    public class EntityLiving : BaseEntity, IDamageable, IHealable
    {
        [SerializeField] protected EntityHealthComponent health;

        public int Health => health != null ? health.Health : 0;
        public int MaxHealth => health != null ? health.MaxHealth : 0;
        public float HealthPercent => health != null ? health.HealthPercent : 0;
        public bool IsAlive => health != null ? health.IsAlive : false;

        public bool CanDamage => health != null ? health.CanTakeDamage : false;
        public bool CanHeal => health != null ? health.CanHeal : false;
        public bool CanDie => health != null ? health.CanDie : false;

        public void InitializeHealthComponent(int health = 100, int maxHealth = 100)
        {
            if (this.health == null)
            {
                this.health = gameObject.AddComponent<EntityHealthComponent>();

                this.health.SetHealth(health);
                this.health.SetMaxHealth(maxHealth);

                this.health.Initialize(this);
            }
        }

        public void HealthSubscribe()
        {
            if (health != null)
            {
                health.OnDeath.Subscribe(OnDeath);
                health.OnRevive.Subscribe(OnRevive);
            }
        }

        public void HealthUnsubscribe()
        {
            if (health != null)
            {
                health.OnDeath.Unsubscribe(OnDeath);
                health.OnRevive.Unsubscribe(OnRevive);
            }
        }

        public void ApplyDamage(int damage)
        {
            if (health != null)
            {
                health.ApplyDamage(damage);
            }
        }

        public void ApplyDamagePercent(float percent)
        {
            if (health != null)
            {
                health.ApplyDamagePercent(percent);
            }
        }

        public void ApplyHeal(int amount)
        {
            if (health != null)
            {
                health.ApplyHeal(amount);
            }
        }

        public void ApplyHealPercent(float percent)
        {
            if (health != null)
            {
                health.ApplyHealPercent(percent);
            }
        }

        public void Kill()
        {
            if (health != null)
            {
                health.Kill();
            }
        }

        public void Revive()
        {
            if (health != null)
            {
                health.Revive();
            }
        }

        public void SetHealth(int health)
        {
            if (this.health != null)
            {
                this.health.SetHealth(health);
            }
        }

        public void SetMaxHealth(int maxHealth)
        {
            if (health != null)
            {
                health.SetMaxHealth(maxHealth);
            }
        }

        public void SetCanDamage(bool canDamage)
        {
            if (health != null)
            {
                health.CanTakeDamage = canDamage;
            }
        }

        public void SetCanHeal(bool canHeal)
        {
            if (health != null)
            {
                health.CanHeal = canHeal;
            }
        }

        public void SetCanDie(bool canDie)
        {
            if (health != null)
            {
                health.CanDie = canDie;
            }
        }

        protected virtual void OnDeath()
        {
            // Handle death, e.g., play animations, drop loot, etc.
        }

        protected virtual void OnRevive()
        {
            // Handle revival, e.g., reset state, play animations, etc.
        }

        private void OnDestroy()
        {
            HealthUnsubscribe();
        }
    }
}
