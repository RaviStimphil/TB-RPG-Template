using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "EnemyStats", menuName ="ScriptableObjects/EnemyStats")]
public class EnemyUnitStats : UnitStats
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
