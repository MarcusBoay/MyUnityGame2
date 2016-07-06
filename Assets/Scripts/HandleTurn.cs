using UnityEngine;
using System.Collections;

[System.Serializable]
public class HandleTurn
{
    public string Attacker;
    public GameObject AttackersGameObject; //who is attacking
    public GameObject AttackersTarget; //who is being attacked
    public string Type;

    //which attack is performed
    public BaseAttack chosenAttack;
}
