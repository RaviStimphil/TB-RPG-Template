using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat 
{
    public int baseValue;
    public int equipmentValue;
    public float ratioValue;
    public int addedValue;
    public int finalValue;

    public void AssignBaseValue(int value){
        baseValue = value;
    }
    public void AdjustRatio(float value){
        ratioValue += value;
    }
    
    public void AdjustDifference(int value){
        addedValue += value;
    }

    public void AssignEquipValue(int value){
        equipmentValue = value;
    }

    //public void
}
