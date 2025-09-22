using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;

public class GameBattleData
{
    public List<GameObject> allyUnits;
    public List<GameObject> enemyUnits;
    public int turnCount;
    
    public void UpdateData(List<GameObject> allies, List<GameObject> enemies, int turns){
        allyUnits = allies;
        enemyUnits = enemies;
        turnCount = turns; 
    }
}
