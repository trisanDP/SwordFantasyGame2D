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

public interface IIntractable {
  /*  protected InventoryManager GetInventoryM(InventoryManager manager) {
        return manager;
    }*/
    public void OnEntract();

}