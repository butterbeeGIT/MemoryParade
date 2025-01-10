using Assets.MemoryParade.Scripts.Game.Gameplay.Player;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public int attackCount = 0;

    private int playerHP = 100;
    private int enemyHP = 100;

    private Animator playerAnimator;
    private Animator enemyAnimator;

    private BattleTrigger battle;
    private PowerAttack powerAttack;
    public bool BattleIsEnd = false;

    private bool canAttack = true;

    void Start()
    {
        playerAnimator = GetComponent<Animator>(); // Предполагаем, что скрипт прикреплен к объекту игрока
        enemyAnimator = GameObject.Find("Mummy_0").GetComponent<Animator>(); // Найдите объект врага по имени

        battle = FindAnyObjectByType<BattleTrigger>();
        powerAttack = FindAnyObjectByType<PowerAttack>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack && battle.BattleIsStart)
        {
            PlayerAttack();
        }
        else if (!powerAttack.click)
        {
            playerAnimator.SetBool("turn", false);
        }
    }
    
    public void PlayerPowerAttack()
    {
        canAttack = false;
        Attack();
        enemyHP -= 10;
        if (enemyHP <= 0)
        {
            Debug.Log("Вы выиграли");
            EnemyDie();
            BattleIsEnd = true;
            playerAnimator.SetTrigger("win");
            return;
        }
        else Debug.Log("Вы атаковали врага! HP врага: " + enemyHP);
        Invoke("EnemyAttack", 1f);
    }

    public void Attack()
    {
        playerAnimator.SetBool("turn", true);
        playerAnimator.SetTrigger("Attack");
    }

    void PlayerAttack()
    {
        attackCount++;
        canAttack = false;
        Attack();
        enemyHP -= 2; // Урон
        if (enemyHP <= 0)
        {
            Debug.Log("Вы выиграли");
            EnemyDie();
            BattleIsEnd = true;
            playerAnimator.SetTrigger("win");
            return;
        }
        else Debug.Log("Вы атаковали врага! HP врага: " + enemyHP);

        // После этого запускаем анимацию врага
        Invoke("EnemyAttack", 1f); // Время ожидания для завершения анимации атаки игрока
    }

    void EnemyDie()
    {
        enemyAnimator.SetBool("die", true);
        enemyAnimator.transform.position = new Vector3(enemyAnimator.transform.position.x, (float)(enemyAnimator.transform.position.y - 0.3), 0);
    }

    public void EnemyAttack()
    {
        enemyAnimator.SetBool("turn", true);

        // Враг атакует
        int damage = Random.Range(1, 9); // Урон от врага
        playerHP -= damage;
        canAttack = true;
        Debug.Log("Враг атаковал! Вы потеряли " + damage + " HP. Ваше HP: " + playerHP);

        // Возврат врага в базовую анимацию
        Invoke("ResetEnemyAnimation", 1f); // Время ожидания для завершения анимации атаки врага
    }

    void ResetEnemyAnimation()
    {
        enemyAnimator.SetBool("turn", false);
    }
}
