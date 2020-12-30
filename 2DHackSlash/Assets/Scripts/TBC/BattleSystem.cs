using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    private DamageCalculations damageCalculations;
    [SerializeField] private BattleStates bStates;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    private UnitStats playerUnit;
    private UnitStats enemyUnit;

    private GameObject player;
    private GameObject enemy;

    private void Awake()
    {
        damageCalculations = GetComponent<DamageCalculations>();
    }

    private void Start()
    {
        bStates = BattleStates.SETUP;
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        Debug.Log("Setup Battle");
        
        player =  Instantiate(playerPrefab, playerBattleStation);
        player.transform.localScale = new Vector3(-8, 8, 0);
        playerUnit = player.GetComponent<PlayerInformation>().unitStats;

        enemy = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemy.GetComponent<EnemyInformation>().unitStats;

        Debug.Log("Setup Done");

        yield return new WaitForSeconds(2f);

        Debug.Log("Check next State");

        CheckTurn(); 
    }

    private void CheckTurn()
    {
        Debug.Log("Check Who's Turn it is");

        playerUnit.turnIsDone = false;
        enemyUnit.turnIsDone = false;

        if (playerUnit.speed >= enemyUnit.speed)
        {
            bStates = BattleStates.PLAYERTURN;
            PlayerTurn();
        }
        else
        {
            bStates = BattleStates.ENEMYTURN;
            EnemyTurn();
        }
    }

    private void PlayerTurn()
    {
        Debug.Log("PlayerTurn State");

        OnAttackButton();
    }

    public void OnAttackButton()
    {
        Debug.Log("Player Attack State");
       
        if (bStates != BattleStates.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        Debug.Log("Attack " + enemyUnit.name);

        //Damage The Enemy
        enemyUnit.currentHP -= damageCalculations.CalculateDamage(playerUnit.meleeDamage, enemyUnit.meleeDefence);

        Debug.Log(enemyUnit.name + " has only " + enemyUnit.currentHP + " health left");

        yield return new WaitForSeconds(2f);

        playerUnit.turnIsDone = true;

        //Check if enemy is dead
        if(enemyUnit.currentHP <= 0)
        {
            bStates = BattleStates.WON;
            BattleWon();
        }
        else if(!enemyUnit.turnIsDone)
        {
            bStates = BattleStates.ENEMYTURN;
            EnemyTurn();
        }
        else
        {
            bStates = BattleStates.CHECKSPEED;
            CheckTurn();
        }
        //change state based on what happend
    }

    

    private void EnemyTurn()
    {
        Debug.Log("Enemy Turn");

        if (bStates != BattleStates.ENEMYTURN)
        {
            return;
        }

        StartCoroutine(EnemyAttack());
    }

    IEnumerator EnemyAttack()
    {
        Debug.Log("Attack " + playerUnit.name);

        playerUnit.currentHP -= damageCalculations.CalculateDamage(enemyUnit.meleeDamage, playerUnit.meleeDefence);

        Debug.Log(playerUnit.name + " has only " + playerUnit.currentHP + " health left");

        yield return new WaitForSeconds(1f);

        enemyUnit.turnIsDone = true;

        //Check if enemy is dead
        if (playerUnit.currentHP <= 0)
        {
            bStates = BattleStates.LOST;
            BattleLost();
        }
        else if (!playerUnit.turnIsDone)
        {
            bStates = BattleStates.PLAYERTURN;
            PlayerTurn();
        }
        else
        {
            bStates = BattleStates.CHECKSPEED;
            CheckTurn();
        }
        //change state based on what happend
    }

    private void BattleWon()
    {
        Debug.Log("You Won");

        Destroy(enemy);
    }

    private void BattleLost()
    {
        Debug.Log("You Lost");

        Destroy(player);
    }
}