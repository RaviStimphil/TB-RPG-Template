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
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
