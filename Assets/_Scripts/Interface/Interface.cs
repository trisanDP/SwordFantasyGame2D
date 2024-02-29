using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    
}

public interface IDamageable
{
    void TakeDamage(float damageAmount,int knockBackF, GameObject damageFrom);
}

public interface IConsumable
{
    void Consume();
}