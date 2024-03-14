using System.Runtime.InteropServices.WindowsRuntime;
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
