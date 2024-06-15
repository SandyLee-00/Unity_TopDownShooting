using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private float healthChangeDelay = 0.5f;
    [SerializeField] private AudioClip damageClip;

    private CharacterStatHandler statHandlers;
    private float timeSinceLastChange = float.MaxValue;
    private bool isAttacked = false;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    public float CurrentHealth { get; private set; }

    public float MaxHealth => statHandlers.CurrentStat.maxHealth;

    private void Awake()
    {
        statHandlers = GetComponent<CharacterStatHandler>();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;

    }

    private void Update()
    {
        if (isAttacked && timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;

            if (timeSinceLastChange >= healthChangeDelay)
            {
                OnInvincibilityEnd?.Invoke();
                isAttacked = false;
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (CurrentHealth == 0)
        {
            return false;
        }

        if(timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (CurrentHealth <= 0f)
        {
            CallDeath();
            return true;
        }
        if(change > 0)
        {
            OnHeal?.Invoke();
        }
        else
        {
            OnDamage?.Invoke();
            isAttacked = true;
            if (damageClip) SoundManager.PlayClip(damageClip);
        }

        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}