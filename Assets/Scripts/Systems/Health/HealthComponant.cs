using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponant : MonoBehaviour, IDamagable
{
    public float Health => CurrentHealth;
    public float MaxHealth => TotalHealth;
    [SerializeField] private float CurrentHealth;
    [SerializeField] private float TotalHealth;

    //[SerializeField] private ConsumableScriptable PotionItem;
    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if(CurrentHealth <= 0)
        {
            Destroy();
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        CurrentHealth = TotalHealth;
        CurrentHealth = 50;
        //PotionItem.UseItem(GetComponent<PlayerController>());
    }

    public void HealPlayer(int effect)
    {
        if(CurrentHealth < MaxHealth)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + effect, 0, MaxHealth);
        }
    }
    
}
