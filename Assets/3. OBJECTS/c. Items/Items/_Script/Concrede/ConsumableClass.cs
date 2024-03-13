using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool Class", menuName = "Item/Consumable")]
public class ConsumableClass : ItemClass
{
    public enum Type
    {
        Healing, DamageAmp, SpeedBoost
    }
    public enum Grade
    {
        Grade1, Grade2, Grade3
    }

    public Type type;
    public Grade grade;


    private void PotionReactionManage()
    {
        switch(grade)
        {
            case Grade.Grade1:
                if(type == Type.Healing)
                {
                    HealBy(10);
                }
                else if (type == Type.DamageAmp)
                {
                    HealBy(10);
                }
                break;
            case Grade.Grade2:
                if (type == Type.Healing)
                {
                    HealBy(50);
                } else if (type == Type.DamageAmp)
                {
                    DamageAmpBy(50);
                }
                break;
            case Grade.Grade3:
                if (type == Type.Healing)
                {
                    HealBy(100);
                } else if (type == Type.DamageAmp)
                {
                    DamageAmpBy(100);
                }
                break;



        }
    }
    private void DamageAmpBy(int i)
    {
        /*GameManager.Instance.playerScript.playerHealth.AddHealth(10);*/
        Debug.Log("DamageAmpBy" + i);
    }
    void HealBy(int i) 
    { 
/*           Debug.Log("Healing By"+ i);*/
    }

    public override void Use()
    {
        base.Use();
        PotionReactionManage();
        RemoveFromInventory();
    }
}
