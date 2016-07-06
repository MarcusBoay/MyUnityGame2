using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION,
        CHECKALIVE,
        WIN,
        LOSE
    }
    public PerformAction battleStates;

    public List<HandleTurn> performList = new List<HandleTurn>();

    public List<GameObject> herosInBattle = new List<GameObject>();
    public List<GameObject> mobsInBattle = new List<GameObject>();

    public enum HeroGUI
    {
        ACTIVATE,
        WAITING,
        INPUT1,
        INPUT2,
        DONE
    }
    public HeroGUI HeroInput;

    public List<GameObject> HeroesToManage = new List<GameObject>();
    private HandleTurn HeroChoice;

    public GameObject enemyButton;
    public Transform Spacer;

    public GameObject AttackPanel;
    public GameObject EnemySelectPanel;
    public GameObject MagicPanel;
    private Transform AST;

    //attack of heroes
    public Transform actionSpacer;
    public Transform magicSpacer;
    public GameObject actionButton;
    public GameObject magicButton;
    private List<GameObject> atkBtns = new List<GameObject>();
    //enemy buttons
    private List<GameObject> enemyBtns = new List<GameObject>();

    void Start()
    {
        battleStates = PerformAction.WAIT;
        mobsInBattle.AddRange(GameObject.FindGameObjectsWithTag("Mob"));
        herosInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        HeroInput = HeroGUI.ACTIVATE;
        AST = GameObject.Find("BattleCanvas").transform.FindChild("AttackStatsPanel");

        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(false);
        MagicPanel.SetActive(false);

        EnemyButtons();
    }

    void Update()
    {
        switch (battleStates)
        {
            case PerformAction.PERFORMACTION:
                //idle
                break;
            case PerformAction.TAKEACTION:
                GameObject performer = GameObject.Find(performList[0].Attacker);
                if (performList[0].Type == "Enemy")
                {
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                    /*for (int i = 0; i < herosInBattle.Count; i++)
                    {
                        if (performList[0].AttackersTarget == herosInBattle[i])
                        {
                            ESM.heroToAttack = performList[0].AttackersTarget;
                            ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                            break;
                        }
                        else
                    */
                    performList[0].AttackersTarget = herosInBattle[Random.Range(0, herosInBattle.Count)];
                    ESM.heroToAttack = performList[0].AttackersTarget;

                    ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                    battleStates = PerformAction.PERFORMACTION;
                    //old mp checker
                    /*
                    if (ESM.myMob.curMP - performList[0].chosenAttack.attackCost >= 0)
                    {
                        ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                        battleStates = PerformAction.PERFORMACTION;
                    }
                    else
                    {
                        //remove this performer from the list in BSM
                        performList.RemoveAt(0);
                        //reset BSM -> wait
                        if (battleStates != PerformAction.WIN && battleStates != PerformAction.LOSE)
                        {
                            battleStates = PerformAction.WAIT;
                            //reset player state
                            ESM.curCoolDown = 0f;
                            ESM.currentState = EnemyStateMachine.TurnState.PROCESSING;
                        }
                        else
                        {
                            ESM.currentState = EnemyStateMachine.TurnState.WAITING;
                        }
                    }
                    */
                }
                else if (performList[0].Type == "Hero")
                {
                    HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
                    HSM.enemyToAttack = performList[0].AttackersTarget;
                    //mp checker
                    if (HSM.myHero.curMP - performList[0].chosenAttack.attackCost >= 0)
                    {
                        HSM.currentState = HeroStateMachine.TurnState.ACTION;
                        battleStates = PerformAction.PERFORMACTION;
                    }
                    else
                    {
                        Debug.Log("Not enough mp");
                        //remove this performer from the list in BSM
                        performList.RemoveAt(0);
                        //reset BSM -> wait
                        if (battleStates != PerformAction.WIN && battleStates != PerformAction.LOSE)
                        {
                            battleStates = PerformAction.WAIT;
                            //reset player state
                            HSM.curCoolDown = 0f;
                            HSM.currentState = HeroStateMachine.TurnState.PROCESSING;
                        }
                        else
                        {
                            HSM.currentState = HeroStateMachine.TurnState.WAITING;
                        }
                    }
                }
                break;
            case PerformAction.WAIT:
                if (performList.Count > 0)
                {
                    battleStates = PerformAction.TAKEACTION;
                }
                break;
            case PerformAction.CHECKALIVE:
                if (herosInBattle.Count < 1)
                {
                    battleStates = PerformAction.LOSE;
                    //lose game
                }
                else if (mobsInBattle.Count < 1)
                {
                    battleStates = PerformAction.WIN;
                    //win game
                }
                else
                {
                    //call function 
                    ClearAttackPanel();
                    HeroInput = HeroGUI.ACTIVATE;
                }
                break;
            case PerformAction.WIN:
                Debug.Log("win");
                for (int i = 0; i < herosInBattle.Count; i++)
                {
                    herosInBattle[i].GetComponent<HeroStateMachine>().currentState = HeroStateMachine.TurnState.WAITING;
                }
                break;
            case PerformAction.LOSE:
                Debug.Log("lose");
                break;
        }

        switch (HeroInput)
        {
            case (HeroGUI.ACTIVATE):
                if (HeroesToManage.Count > 0)
                {
                    HeroesToManage[0].transform.FindChild("Selector").gameObject.SetActive(true);
                    //create handleturn instance
                    HeroChoice = new HandleTurn();
                    AttackPanel.SetActive(true);
                    //populate action buttons
                    CreateAttackButtons();
                    HeroInput = HeroGUI.WAITING;
                }
                break;
            case (HeroGUI.WAITING):
                //idle
                break;
            case (HeroGUI.DONE):
                HeroInputDone();
                break;
        }
    }

    public void CollectActions(HandleTurn input)
    {
        performList.Add(input);
    }

    public void EnemyButtons()
    {
        //clean up 
        //destroying the enemybtn GAME OBJECT
        foreach (GameObject enemyBtn in enemyBtns)
        {
            Destroy(enemyBtn);
        }
        //destroying the enemybtn in the LIST
        enemyBtns.Clear();
        //craete buttons
        foreach (GameObject enemy in mobsInBattle)
        {
            GameObject newButton = Instantiate(enemyButton) as GameObject;
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();
            EnemyStateMachine curEnemy = enemy.GetComponent<EnemyStateMachine>();

            Text buttonText = newButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
            buttonText.text = curEnemy.myMob.myName;

            button.EnemyPrefab = enemy;

            newButton.transform.SetParent(Spacer, false);
            enemyBtns.Add(newButton);
        }
    }

    public void Input1()//attack button
    {
        HeroChoice.Attacker = HeroesToManage[0].name;
        HeroChoice.AttackersGameObject = HeroesToManage[0];
        HeroChoice.Type = "Hero";
        HeroChoice.chosenAttack = HeroesToManage[0].GetComponent<HeroStateMachine>().myHero.attacksList[0];
        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(true);
    }

    public void Input2(GameObject chosenEnemy)//enemy selection
    {
        HeroChoice.AttackersTarget = chosenEnemy;
        HeroInput = HeroGUI.DONE;
    }

    void HeroInputDone()
    {
        performList.Add(HeroChoice);
        ClearAttackPanel();
        HeroesToManage[0].transform.FindChild("Selector").gameObject.SetActive(false);
        HeroesToManage.RemoveAt(0);
        HeroInput = HeroGUI.ACTIVATE;
    }

    void ClearAttackPanel()
    {
        EnemySelectPanel.SetActive(false);
        AttackPanel.SetActive(false);
        MagicPanel.SetActive(false);
        AST.gameObject.SetActive(false);
        foreach (GameObject atkBtn in atkBtns)
        {
            Destroy(atkBtn);
        }
        atkBtns.Clear();
    }

    //create action buttons
    void CreateAttackButtons()
    {
        GameObject AttackButton = Instantiate(actionButton) as GameObject;
        Text AttackButtonText = AttackButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
        AttackButtonText.text = "Attack";
        AttackButton.GetComponent<Button>().onClick.AddListener(()=>Input1());
        AttackButton.transform.SetParent(actionSpacer, false);
        atkBtns.Add(AttackButton);

        GameObject MagicAttackButton = Instantiate(actionButton) as GameObject;
        Text MagicAttackButtonText = MagicAttackButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
        MagicAttackButtonText.text = "Magic";
        MagicAttackButton.GetComponent<Button>().onClick.AddListener(() => Input3());
        MagicAttackButton.transform.SetParent(actionSpacer, false);
        atkBtns.Add(MagicAttackButton);

        if (HeroesToManage[0].GetComponent<HeroStateMachine>().myHero.magicAttacks.Count > 0)
        {
            foreach (BaseAttack magicAtk in HeroesToManage[0].GetComponent<HeroStateMachine>().myHero.magicAttacks)
            {
                GameObject MagicButton = Instantiate(magicButton) as GameObject;
                Text MagicButtonText = MagicButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
                MagicButtonText.text = magicAtk.attackName;
                AttackButton ATB = MagicButton.GetComponent<AttackButton>();
                ATB.magicAttackToPerform = magicAtk;
                MagicButton.transform.SetParent(magicSpacer, false);
                atkBtns.Add(MagicButton);
                if (HeroesToManage[0].GetComponent<HeroStateMachine>().myHero.curMP < magicAtk.attackCost)
                {
                    MagicButton.GetComponent<Button>().interactable = false; //deactivates magic button if not enough mp
                }
            }
        }
        else
        {
            MagicAttackButton.GetComponent<Button>().interactable = false;
        }
    }
    //chosen magic atk
    public void Input4(BaseAttack chosenMagic)
    {
        HeroChoice.Attacker = HeroesToManage[0].name;
        HeroChoice.AttackersGameObject = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.chosenAttack = chosenMagic;
        MagicPanel.SetActive(false);
        EnemySelectPanel.SetActive(true);
    }
    //switching to magic atks
    public void Input3()
    {
        AttackPanel.SetActive(false);
        MagicPanel.SetActive(true);
    }
}