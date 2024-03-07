using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStat : CharacterStat
{
    PlayerScript playerScrip;

    public float stundDuration;

    private void Start() {
        playerScrip = GetComponent<PlayerScript>();
        GetComponentInChildren<PlayerEquipmentManager>().onEquipmentChanged += OnEquipmentChanged;
    }

    public override void TakeDamage(float damage, int knockBack,GameObject damageFrom) {
        base.TakeDamage(damage, knockBack,damageFrom);
    }

    public override void Die(){
        base.Die();
        playerScrip.isDead = true;
        gameObject.SetActive(false);
        GameManager.Instance.GameOver();
    }

    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem ){
        if (newItem != null){
            armor.AddModifier(newItem.armorModifier);
            physicalDamage.AddModifier(newItem.damageModifier);
            resistance.AddModifier(newItem.resistanceModifier);
        }
        if(oldItem != null){
            armor.RemoveModifier(oldItem.armorModifier);
            physicalDamage.RemoveModifier(oldItem.damageModifier);
            resistance.RemoveModifier(oldItem.resistanceModifier);
        }
    }



    #region Effect

    internal IEnumerator Stund() {
        playerScrip.canMove = false; //make movement manager as base
        yield return new WaitForSeconds(stundDuration);
        playerScrip.canMove = true;
    }
    #endregion
}
