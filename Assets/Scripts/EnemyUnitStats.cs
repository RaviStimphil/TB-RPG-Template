using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }/*
    public void ChooseTarget(List<GameObject> choices){
        int index = 0;
        Dictionary<Unit, int> chanceChoices = new Dictionary<Unit, int>();
        int[] totalChance = new int[choices.Count];
        foreach(GameObject unit in choices){
            Unit realUnit = unit.GetComponent<Unit>();
            chanceChoices.TryAdd(realUnit, 0);
            if(hateBuildup.ContainsKey(realUnit)){
                chanceChoices[realUnit] += hateBuildup[realUnit];
            }else{
                //If there is no unit in buildup, assume that its 0.
                ChanceChoices[realUnit] += 0; 
            }
            ChanceChoices[realUnit] += Unit.unitStat.aggro.currentValue;
            
            
        }

        //Logic to randomly pick a unit, based on weight.

        //Returns the gameObject it wants to pick. 
    }*/
}
