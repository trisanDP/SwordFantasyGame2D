using UnityEngine;

public class CharacterStat : MonoBehaviour, IDamageable
{
    Rigidbody2D rb;

    [Header("Health")]
    [Min(0)]public int maxHealth = 100;
    public int currentHealth { get; private set; }

    [Header("Stats")]
    public Stat physicalDamage;
    public Stat armor;
    public Stat resistance;

    private void Awake()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();

    }

    public virtual void TakeDamage(float damage, int knockBack,GameObject damageFrom)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0,int.MaxValue);
        currentHealth -= (int)damage;
        Debug.Log(name + " takes " + damage + " damage");
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public void AddHealth(int add)
    {
        if (currentHealth > 0)
        {
            currentHealth += add;
            Debug.Log("added " + add + " Health to" + name);
            
        }
    }

    public virtual void Die()
    {
        Debug.Log(name + " Died");
    }


    #region effect
    public virtual void  KnockBack(int force,GameObject target) {
        Vector2 direction = (transform.position - target.transform.position).normalized;
        rb.AddForce(force * direction, ForceMode2D.Impulse);
    }
    #endregion
}

#region Note
//Add a Damage Over Time Function Like Take Damage Function for poision damage and Bleed Damage, Magic Damage
#endregion