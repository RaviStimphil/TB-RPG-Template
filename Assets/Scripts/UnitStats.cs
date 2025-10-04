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

    public Dictionary<Unit, int> hateBuildup = new Dictionary<Unit, int>();

    private List<Buff> buffs;

    public int basePhysicalDefense;
    public float ratioPhysicalDefense = 1.0f;
    public int addedPhysicalDefense;
    public int equipPhysicalDefense; 
    public int finalPhysicalDefense;

    public Stat aggro;
    
    
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
    public Unit ChooseTarget(List<GameObject> choices){
        int totalChance = 0;
        Dictionary<Unit, int> chanceChoices = new Dictionary<Unit, int>();
        foreach(GameObject unit in choices){
            Unit realUnit = unit.GetComponent<Unit>();
            chanceChoices.TryAdd(realUnit, 0);
            if(hateBuildup.ContainsKey(realUnit)){
                chanceChoices[realUnit]+= hateBuildup[realUnit];
                totalChance += hateBuildup[realUnit];
            }else{
                //If there is no unit in buildup, assume that its 0.
                chanceChoices[realUnit] += 0; 
                totalChance += 0;
            }
            chanceChoices[realUnit] += realUnit.unitStat.aggro.FinalValue();
            totalChance += realUnit.unitStat.aggro.FinalValue();
            
        }
        Unit finalUnit = new Unit();
        int randomThing = Random.Range(0, totalChance);
        foreach(KeyValuePair<Unit, int> entry in chanceChoices){
            if(entry.Value > randomThing){
                finalUnit = entry.Key;
                break;
            }else{
                randomThing -= entry.Value;
            }
        }
        
        return finalUnit;
        //Logic to randomly pick a unit, based on weight.

        //Returns the gameObject it wants to pick. 
    }

}
