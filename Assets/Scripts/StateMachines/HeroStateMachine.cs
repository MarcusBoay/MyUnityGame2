using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour
{

    public BaseHero myHero;
    private BattleStateMachine BSM;

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }

    public TurnState currentState;
    public float curCoolDown = 0f;
    public float maxCoolDown = 3f;
    private Image progressBar;
    public GameObject Selector;
    //IEnumerator
    public GameObject enemyToAttack;
    private bool actionStarted = false;
    private Vector3 startPosition;
    private float animSpeed = 1600f;
    private bool isAlive = true;
    //hero panel
    private HeroPanelStats stats;
    public GameObject heroPanel;
    private Transform heroPanelSpacer;

    void Start()
    {
        //find spacer
        heroPanelSpacer = GameObject.Find("BattleCanvas").transform.FindChild("Panel 2").transform.FindChild("HeroPanelSpacer");
        //create panel, fill in info
        CreateHeroPanel();

        curCoolDown = Random.Range(0, 1f);
        Selector.SetActive(false);
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        currentState = TurnState.PROCESSING;
        startPosition = transform.position;
    }

    void Update()
    {
        switch (currentState)
        {
            case TurnState.ACTION:
                StartCoroutine(TimeForAction());
                break;
            case TurnState.ADDTOLIST:
                BSM.HeroesToManage.Add(this.gameObject);
                currentState = TurnState.WAITING;
                break;
            case TurnState.DEAD:
                if (!isAlive)
                {
                    return;
                }
                else
                {
                    //change tag
                    this.gameObject.tag = "DeadHero";
                    //not attackable by enemy //removing hero game object from list
                    BSM.herosInBattle.Remove(this.gameObject);
                    //not managable //removing hero game object from list
                    BSM.HeroesToManage.Remove(this.gameObject);
                    //deactivate selector
                    Selector.SetActive(false);
                    //reset gui
                    BSM.AttackPanel.SetActive(false);
                    BSM.EnemySelectPanel.SetActive(false);
                    //remove item from performList
                    if (BSM.herosInBattle.Count > 0)
                    {
                        for (int i = 0; i < BSM.performList.Count; i++)
                        {
                            if (BSM.performList[i].AttackersGameObject == this.gameObject)
                            {
                                BSM.performList.Remove(BSM.performList[i]);
                            }
                            if (BSM.performList[i].AttackersTarget == this.gameObject)
                            {
                                BSM.performList[i].AttackersTarget = BSM.herosInBattle[Random.Range(0, BSM.herosInBattle.Count)];
                            }
                        }
                    }
                    //change color //play animation
                    this.gameObject.GetComponent<Image>().color = new Color32(105, 105, 105, 250);
                    //reset heroinput
                    BSM.battleStates = BattleStateMachine.PerformAction.CHECKALIVE;
                    isAlive = false;
                }
                break;
            case TurnState.PROCESSING:
                if (BSM.battleStates != BattleStateMachine.PerformAction.PERFORMACTION)
                {
                    UpdateProgressBar();
                }
                break;
            case TurnState.WAITING:
                //idle
                break;
        }
    }

    void UpdateProgressBar()
    {
        curCoolDown = curCoolDown + Time.deltaTime;
        progressBar.transform.localScale = new Vector3(Mathf.Clamp(curCoolDown / maxCoolDown, 0, 1), progressBar.transform.localScale.y, progressBar.transform.localScale.z);
        if (curCoolDown >= maxCoolDown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }

    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;
        //animate the enemy near the hero to attack
        Vector3 enemyPosition = new Vector3(enemyToAttack.transform.position.x - 175f, enemyToAttack.transform.position.y, enemyToAttack.transform.position.z);
        while (MoveToPoint(enemyPosition))
        {
            yield return null;
        }
        //wait a bit
        yield return new WaitForSeconds(0.5f);
        //do damage
        DoDamage();
        //animate back to start position
        while (MoveToPoint(startPosition))
        {
            yield return null;
        }
        //remove this performer from the list in BSM
        BSM.performList.RemoveAt(0);
        //reset BSM -> wait
        if (BSM.battleStates != BattleStateMachine.PerformAction.WIN && BSM.battleStates != BattleStateMachine.PerformAction.LOSE)
        {
            BSM.battleStates = BattleStateMachine.PerformAction.WAIT;
            //reset enemy state
            curCoolDown = 0f;
            currentState = TurnState.PROCESSING;
        }
        else
        {
            currentState = TurnState.WAITING;
        }
        //end coroutine
        actionStarted = false;
    }

    private bool MoveToPoint(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    public void TakeDamage(float getDamageAmount)
    {
        myHero.curHP -= getDamageAmount;
        if (myHero.curHP <= 0)
        {
            myHero.curHP = 0;
            currentState = TurnState.DEAD;
        }
        UpdateHeroPanel();
    }

    void DoDamage()
    {
        TakeAwayMP();
        UpdateHeroPanel();
        double calcDamage = 0;
        //elemental system
        if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.NORMAL)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<EnemyStateMachine>().myMob.resistNormal);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.FIRE)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<EnemyStateMachine>().myMob.resistFire);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.DARK)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<EnemyStateMachine>().myMob.resistDark);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.EARTH)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<EnemyStateMachine>().myMob.resistEarth);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.ELECTRIC)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<EnemyStateMachine>().myMob.resistElectric);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.HOLY)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<EnemyStateMachine>().myMob.resistHoly);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.ICE)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<EnemyStateMachine>().myMob.resistIce);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.METAL)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<EnemyStateMachine>().myMob.resistMetal);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.POISON)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<EnemyStateMachine>().myMob.resistPoison);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.WATER)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<EnemyStateMachine>().myMob.resistWater);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.WIND)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<EnemyStateMachine>().myMob.resistWind);
        }
        enemyToAttack.GetComponent<EnemyStateMachine>().TakeDamage(System.Convert.ToSingle(calcDamage));
        Debug.Log(calcDamage);
    }

    void TakeAwayMP()
    {
        myHero.curMP -= BSM.performList[0].chosenAttack.attackCost;
    }

    void CreateHeroPanel()
    {
        heroPanel = Instantiate(heroPanel) as GameObject;
        stats = heroPanel.GetComponent<HeroPanelStats>();
        stats.heroName.text = myHero.myName;
        stats.heroHP.text = "HP: " + myHero.curHP + " / " + myHero.maxHP;
        stats.heroMP.text = "MP: " + myHero.curMP + " / " + myHero.maxMP;

        progressBar = stats.progressBar;
        heroPanel.transform.SetParent(heroPanelSpacer, false);
    }

    void UpdateHeroPanel()
    {
        stats.heroHP.text = "HP: " + myHero.curHP + " / " + myHero.maxHP;
        stats.heroMP.text = "MP: " + myHero.curMP + " / " + myHero.maxMP;
    }
}

