using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObjects/CustomUnits", order = 1)]
public class UnitStats : ScriptableObject
{
    public Sprite characterImage;

    public string unitName;
    public int unitLevel;

    public int meleeDamage;
    public int meleeDefence;

    public int maxHP;
    public int currentHP;

    public int speed;


    public bool turnIsDone;
}