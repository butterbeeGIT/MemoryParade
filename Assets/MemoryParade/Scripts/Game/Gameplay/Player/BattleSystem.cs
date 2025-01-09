using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    private int playerHP = 100;
    private int enemyHP = 100;
    private Animator playerAnimator;
    private Animator enemyAnimator;
    private BattleTrigger battle;

    void Start()
    {
        playerAnimator = GetComponent<Animator>(); // Предполагаем, что скрипт прикреплен к объекту игрока
        enemyAnimator = GameObject.Find("Mummy_0").GetComponent<Animator>(); // Найдите объект врага по имени
        if (battle.BattleIsStart)
            StartBattle();
    }

    void StartBattle()
    {
        while (playerHP > 0 && enemyHP > 0)
        {
            PlayerAttack();
            if (enemyHP <= 0) 
                break;
            EnemyAttack();
        }

        if (playerHP <= 0)
            Debug.Log("Игрок проиграл!");
        else
            Debug.Log("Игрок победил!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            PlayerAttack();
        }
        else
        {
            playerAnimator.SetBool("turn", false);
        }
    }

    void PlayerAttack()
    {
        playerAnimator.SetBool("turn", true);
        playerAnimator.SetTrigger("Attack");
        enemyHP -= 50; // Пример урона
        Debug.Log("Вы атаковали врага! HP врага: " + enemyHP);

        // После этого запускаем анимацию врага
        Invoke("EnemyAttack", 1f); // Время ожидания для завершения анимации атаки игрока
    }

    void EnemyAttack()
    {
        enemyAnimator.SetBool("turn", true);

        // Враг атакует
        int damage = Random.Range(1, 9); // Урон от врага
        playerHP -= damage;
        Debug.Log("Враг атаковал! Вы потеряли " + damage + " HP. Ваше HP: " + playerHP);

        // Возврат врага в базовую анимацию
        Invoke("ResetEnemyAnimation", 1f); // Время ожидания для завершения анимации атаки врага
    }

    void ResetEnemyAnimation()
    {
        enemyAnimator.SetBool("turn", false);
    }
}
