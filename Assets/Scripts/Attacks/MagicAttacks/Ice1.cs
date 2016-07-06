using UnityEngine;
using System.Collections;

public class Ice1 : BaseAttack
{
    public Ice1()
    {
        attackName = "Ice 1";
        attackDesc = "Basic ice spell";
        attackDamage = 15f;
        attackCost = 10f;
        element = Element.ICE;
    }
}
