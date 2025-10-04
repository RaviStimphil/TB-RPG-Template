using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat 
{
    public int baseValue;
    public int equipmentValue;
    public float ratioValue = 1;
    public int addedValue;
    public int finalValue;
    public int buffValue;
    public bool unlimittedBuff;
    public float maxBuffRatio = 1.5f;
    public float MinBuffRatio = 0.65f;

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
        equipmentValue += value;
    }
    public void AdjustBuff(int value){
        buffValue += value;
    }
    public int FirstValue(){
        return (int) Math.Round((baseValue + equipmentValue) * ratioValue + addedValue);
    }

    public int FinalValue(){
        float buffRatio = (float) 1 + (buffValue / FirstValue());
        if(!unlimittedBuff){
            if(buffRatio > 1.5f){
                buffRatio = 1.5f;
            }else if(buffRatio < 0.65f){
                buffRatio = 0.65f;
            }
        }
        
        finalValue = (int) Math.Round((buffRatio * buffValue) + FirstValue()); 

        return finalValue;
    }

    //public void
}
