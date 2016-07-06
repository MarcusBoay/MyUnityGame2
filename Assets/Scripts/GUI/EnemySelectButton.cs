using UnityEngine;
using System.Collections;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject EnemyPrefab;

    public void SelectEnemy()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().Input2(EnemyPrefab); //save input of enemy prefab
    }

    public void HideSelector()
    {
        EnemyPrefab.transform.FindChild("Selector").gameObject.SetActive(false);
    }

    public void ShowSelector()
    {
        EnemyPrefab.transform.FindChild("Selector").gameObject.SetActive(true);
    }
}
