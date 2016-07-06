using UnityEngine;
using System.Collections;

public class Fire1 : BaseAttack
{
    public Fire1()
    {
        attackName = "Fire 1";
        attackCost = 10f;
        attackDesc = "Basic fire spell";
        attackDamage = 20f;
        element = Element.FIRE;
    }
}
