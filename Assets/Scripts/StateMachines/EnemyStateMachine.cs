using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyStateMachine : MonoBehaviour
{

    private BattleStateMachine BSM;
    public BaseMob myMob;

    public enum TurnState
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState currentState;
    //for the progress bar
    public float curCoolDown = 0f;
    public float maxCoolDown = 5f;
    //this gameobject
    private Vector3 startPostion;
    public GameObject selector;
    //timeforaction stuff
    private bool actionStarted = false;
    public GameObject heroToAttack;
    private float animSpeed = 1600f;
    private bool isAlive = true;

    void Start()
    {
        currentState = TurnState.PROCESSING;
        selector.SetActive(false);
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        startPostion = transform.position;
        curCoolDown = Random.Range(0f, 2.5f);
    }

    void Update()
    {
        switch (currentState)
        {
            case TurnState.ACTION:
                StartCoroutine(TimeForAction());
                break;
            case TurnState.CHOOSEACTION:
                try
                {
                    ChooseAction();
                    currentState = TurnState.WAITING;
                }
                catch
                {
                    //do nothing in case of win or lose
                }
                break;
            case TurnState.DEAD:
                if (!isAlive)
                {
                    return;
                }
                else
                {
                    curCoolDown = 0f;
                    //change tag of enemy
                    this.gameObject.tag = "DeadEnemy";
                    //not attackable by heroes
                    BSM.mobsInBattle.Remove(this.gameObject);
                    //disable the selector 
                    selector.SetActive(false);
                    //remove all inputs enemy atks
                    if (BSM.mobsInBattle.Count > 0)
                    {
                        for (int i = 0; i < BSM.performList.Count; i++)
                        {
                            if (BSM.performList[i].AttackersGameObject == this.gameObject)
                            {
                                BSM.performList.Remove(BSM.performList[i]);
                            }
                            if (BSM.performList[i].AttackersTarget == this.gameObject)
                            {
                                BSM.performList[i].AttackersTarget = BSM.mobsInBattle[Random.Range(0, BSM.mobsInBattle.Count)];
                            }
                        }
                    }
                    //change color to grey//play dead animations
                    this.gameObject.GetComponent<Image>().color = new Color32(105, 105, 105, 245);
                    //set alive false
                    isAlive = false;
                    //reset enemy buttons
                    BSM.EnemyButtons();
                    //check alive
                    BSM.battleStates = BattleStateMachine.PerformAction.CHECKALIVE;
                }
                break;
            case TurnState.PROCESSING:
                if (BSM.battleStates != BattleStateMachine.PerformAction.PERFORMACTION && BSM.battleStates != BattleStateMachine.PerformAction.WIN && BSM.battleStates != BattleStateMachine.PerformAction.LOSE)
                {
                    UpdateProgressBar();
                }
                break;
            case TurnState.WAITING:
                //idle state
                break;
        }
    }

    void UpdateProgressBar()
    {
        curCoolDown = curCoolDown + Time.deltaTime;
        //float calcCoolDown = curCoolDown / maxCoolDown;
        if (curCoolDown >= maxCoolDown)
        {
            currentState = TurnState.CHOOSEACTION;
        }
    }

    void ChooseAction()
    {
        HandleTurn myAttack = new HandleTurn();
        myAttack.Attacker = myMob.myName;
        myAttack.Type = "Enemy";
        myAttack.AttackersGameObject = this.gameObject;
        myAttack.AttackersTarget = BSM.herosInBattle[Random.Range(0, BSM.herosInBattle.Count)];
        myAttack.chosenAttack = myMob.attacksList[Random.Range(0, myMob.attacksList.Count)];
        while (myAttack.chosenAttack.attackCost > myMob.curMP) //if mob has no mp to cast magic, redecide
        {
            myAttack.chosenAttack = myMob.attacksList[Random.Range(0, myMob.attacksList.Count)];
        }
        BSM.CollectActions(myAttack);
    }

    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;
        //animate the enemy near the hero to attack //fix this
        Vector3 heroPosition = new Vector3(heroToAttack.transform.position.x + 175f, heroToAttack.transform.position.y, heroToAttack.transform.position.z);
        while (MoveToPoint(heroPosition))
        {
            yield return null;
        }
        //wait a bit
        yield return new WaitForSeconds(0.5f);
        //do damage
        DoDamage();
        //animate back to start position
        while (MoveToPoint(startPostion))
        {
            yield return null;
        }
        //remove this performer from the list in BSM
        BSM.performList.RemoveAt(0);
        //reset BSM -> wait
        BSM.battleStates = BattleStateMachine.PerformAction.WAIT;
        //end coroutine
        actionStarted = false;
        //reset enemy state
        curCoolDown = 0f;
        currentState = TurnState.PROCESSING;
    }

    private bool MoveToPoint(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    void DoDamage()
    {
        TakeAwayMP();
        double calcDamage = 0;
        //elemental system
        if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.NORMAL)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<HeroStateMachine>().myHero.resistNormal);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.FIRE)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<HeroStateMachine>().myHero.resistFire);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.DARK)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<HeroStateMachine>().myHero.resistDark);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.EARTH)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<HeroStateMachine>().myHero.resistEarth);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.ELECTRIC)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<HeroStateMachine>().myHero.resistElectric);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.HOLY)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<HeroStateMachine>().myHero.resistHoly);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.ICE)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<HeroStateMachine>().myHero.resistIce);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.METAL)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<HeroStateMachine>().myHero.resistMetal);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.POISON)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<HeroStateMachine>().myHero.resistPoison);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.WATER)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<HeroStateMachine>().myHero.resistWater);
        }
        else if (BSM.performList[0].chosenAttack.element == BaseAttack.Element.WIND)
        {
            calcDamage = BSM.performList[0].chosenAttack.attackDamage * (1 - BSM.performList[0].AttackersTarget.GetComponent<HeroStateMachine>().myHero.resistWind);
        }
        heroToAttack.GetComponent<HeroStateMachine>().TakeDamage(System.Convert.ToSingle(calcDamage));
    }

    void TakeAwayMP()
    {
        myMob.curMP -= BSM.performList[0].chosenAttack.attackCost;
    }

    public void TakeDamage(float getDamageAmount)
    {
        myMob.curHP -= getDamageAmount;
        if (myMob.curHP <= 0)
        {
            myMob.curHP = 0;
            currentState = TurnState.DEAD;
        }
    }
}
