
namespace StardropTools.GameKit.Actors
{
    public class ActorHealth : BaseActorComponent, IDamageable, IHealable
    {
        [UnityEngine.SerializeField] private int health;
        [UnityEngine.SerializeField] private int maxHealth;
        [UnityEngine.SerializeField] private bool canTakeDamage = true;
        [UnityEngine.SerializeField] private bool canHeal = true;
        [UnityEngine.SerializeField] private bool canDie = true;

        public int Health
        {
            get => health;
            set => health = value;
        }

        public int MaxHealth
        {
            get => maxHealth;
            set => maxHealth = value;
        }

        public bool CanTakeDamage
        {
            get => canTakeDamage;
            set => canTakeDamage = value;
        }

        public bool CanHeal
        {
            get => canHeal;
            set => canHeal = value;
        }

        public bool CanDie
        {
            get => canDie;
            set => canDie = value;
        }

        public bool IsAlive => Health > 0;
        public float HealthPercent => (float)Health / MaxHealth * 100;

        public readonly EventDelegate OnDeath = new EventDelegate();
        public readonly EventDelegate OnRevive = new EventDelegate();
        public readonly EventDelegate OnMaxHealth = new EventDelegate();

        public readonly EventDelegate<int> OnHealthChanged = new EventDelegate<int>();

        public void ApplyDamage(int damage)
        {
            if (CanTakeDamage)
            {
                Health -= damage;
                HealthChanged();
            }
        }

        public void ApplyDamagePercent(float percent)
        {
            ApplyDamage(UnityEngine.Mathf.CeilToInt(MaxHealth * percent));
        }

        public void ApplyHeal(int amount)
        {
            if (CanHeal)
            {
                Health += amount;
                if (Health > MaxHealth)
                {
                    Health = MaxHealth;
                    OnMaxHealth.Invoke();
                }
                HealthChanged();
            }
        }

        public void ApplyHealPercent(float percent)
        {
            ApplyHeal(UnityEngine.Mathf.CeilToInt(MaxHealth * percent));
        }

        public void Kill()
        {
            if (CanDie)
            {
                Health = 0;
                OnDeath.Invoke();
                HealthChanged();
            }
        }

        public void Revive()
        {
            Health = MaxHealth;
            OnRevive.Invoke();
            HealthChanged();
        }

        public void SetHealth(int health)
        {
            Health = health;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
                OnMaxHealth.Invoke();
            }
            else if (Health < 0)
            {
                Health = 0;
            }
            HealthChanged();
        }

        public void SetMaxHealth(int maxHealth)
        {
            MaxHealth = maxHealth;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
                OnMaxHealth.Invoke();
            }
            HealthChanged();
        }

        protected void HealthChanged()
        {
            OnHealthChanged.Invoke(Health);

            if (Health <= 0)
            {
                OnDeath.Invoke();
            }
            else if (Health == MaxHealth)
            {
                OnMaxHealth.Invoke();
            }
        }

        public void Initialize(BaseActor entity, int health = 100, int maxHealth = 100)
        {
            SetHealth(health);
            SetMaxHealth(maxHealth);

            base.Initialize(entity);
        }
    }
}
