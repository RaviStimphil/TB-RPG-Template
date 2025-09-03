using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UnitStats", menuName ="ScriptableObjects/UnitStats")]

public class UnitStats : ScriptableObject
{
    [SerializeField]
    public int maxHealth;
    public int currentHealth;
    public int attack;
    public int speed;

    private List<Buff> buffs;

    public int basePhysicalDefense;
    public float ratioPhysicalDefense = 1.0f;
    public int addedPhysicalDefense;
    public int equipPhysicalDefense; 
    public int finalPhysicalDefense;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBuff(Buff buff){
        switch(buff.parameter){
            case "defense":
                ratioPhysicalDefense += buff.ratioModifier;
                addedPhysicalDefense += buff.addedModifier;
                break;
        }
        buffs.Add(buff);
    }

    public void RemoveBuff(Buff buff){
        switch(buff.parameter){
            case "defense":
                ratioPhysicalDefense -= buff.ratioModifier;
                addedPhysicalDefense -= buff.addedModifier;
                break;
        }
        buffs.Remove(buff);
    }

    public void CountDownBuff(){
        for(int i = 0; i < buffs.Count; i++){
            if(!buffs[i].isPermanent){
                buffs[i].turnCount -= 1;
                if(buffs[i].turnCount == 0){
                    RemoveBuff(buffs[i]);
                    i--;
                }
            }
        }
    }

}
