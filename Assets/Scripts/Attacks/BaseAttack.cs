using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseAttack : MonoBehaviour
{
    public string attackName;
    public string attackDesc;
    public float attackDamage;//Base damage
    public float attackCost;//MP cost of attack
    public enum Element
    {
        NORMAL,
        FIRE,
        ICE,
        ELECTRIC,
        WATER,
        POISON,
        HOLY,
        EARTH,
        WIND,
        DARK,
        METAL
    }
    public Element element;
}