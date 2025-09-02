using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    public UnitStats originStat;
    public UnitStats unitStat;
    public string unitName;
    public bool isDead;
    public bool isGood;
    public List<BaseSkill> skills; 
    
    void OnEnable(){
        Debug.Log("Subscribing " + name);
        BattleControl.AfterActionEvent += RespondToEvent;
    }
    void OnDisable(){
        BattleControl.AfterActionEvent -= RespondToEvent;
    }

    public void RespondToEvent(UnitAction action){
        Debug.Log("It is currently working" + name);
    }
    void Start(){
        unitStat = Instantiate(originStat);
    }
    public Unit(string name, int health, int attack, bool good){
        this.unitName = name;
        this.unitStat = new UnitStats(); 
        unitStat.maxHealth = health;
        unitStat.currentHealth = health;
        unitStat.attack = attack;
        isGood = good;
    }

    public void DealDamage(GameObject target){
        target.gameObject.GetComponent<Unit>().unitStat.currentHealth = target.gameObject.GetComponent<Unit>().unitStat.currentHealth - skills[0].damageCal(this.unitStat);
        target.gameObject.GetComponent<Unit>().TakingDamage(this.gameObject);

    }

    public void TakingDamage(GameObject target){
        print(name + " has " + this.unitStat.currentHealth + " remaining.");
        if(unitStat.currentHealth <= 0){
            isDead = true;

        }
        
    }
}
