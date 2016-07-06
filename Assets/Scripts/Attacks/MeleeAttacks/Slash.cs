using UnityEngine;
using System.Collections;

public class Slash : BaseAttack
{
    public Slash()
    {
        attackName = "Slash";
        attackDesc = "Slash iz best";
        attackDamage = 10f;
        attackCost = 0;
        element = Element.NORMAL;
    }
}
