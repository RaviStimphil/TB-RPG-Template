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
    public UnityEvent takeDamage;
    public List<BaseSkill> skills; 
    
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
        takeDamage.Invoke();
        
    }
}
