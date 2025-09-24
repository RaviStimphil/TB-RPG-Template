using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RefillStat : Stat
{
    public int currentValue;

    public void RestoreCurrentValue(int value){
        currentValue = Math.Min(currentValue + value, baseValue);
    }
}
