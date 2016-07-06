using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour {

    public BaseAttack magicAttackToPerform;
    private Transform attackStatsPanel;
    private GameObject battleCanvas;
    private BattleStateMachine BSM;

    void Start()
    {
        attackStatsPanel = GameObject.Find("BattleCanvas").transform.FindChild("AttackStatsPanel");
    }

    public void CastMagicAttack()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().Input4(magicAttackToPerform);
    }

    public void ShowMagicStats()
    {
        attackStatsPanel.gameObject.SetActive(true);
        Text PT = attackStatsPanel.FindChild("Spacer").transform.FindChild("PowerText").GetComponent<Text>();
        PT.text = "Power Level: " + magicAttackToPerform.attackDamage.ToString();
        Text MT = attackStatsPanel.Find("Spacer").transform.FindChild("MPText").GetComponent<Text>();
        MT.text = "MP Cost: " + magicAttackToPerform.attackCost.ToString();
    }

    public void HideMagicStats()
    {
        Text PT = attackStatsPanel.FindChild("Spacer").transform.FindChild("PowerText").GetComponent<Text>();
        PT.text = "";
        Text MT = attackStatsPanel.Find("Spacer").transform.FindChild("MPText").GetComponent<Text>();
        MT.text = "";
        attackStatsPanel.gameObject.SetActive(false);
    }
}
