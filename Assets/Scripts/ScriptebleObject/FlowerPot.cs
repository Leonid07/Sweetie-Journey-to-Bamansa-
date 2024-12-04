using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlowerPot", menuName = "Flower_Pot/Flowers", order = 1)]
public class FlowerPot : ScriptableObject
{
    public string playerName;
    public int playerLevel;
    public float playerHealth;
}
