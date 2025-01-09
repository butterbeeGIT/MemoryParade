using Unity.VisualScripting;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    private int playerHP = 100;
    private int enemyHP = 100;
    private Animator playerAnimator;
    private Animator enemyAnimator;
    private BattleTrigger battle;

    public bool BattleIsEnd = false;

    private bool canAttack = true;
    //private bool attack = false;

    void Start()
    {
        playerAnimator = GetComponent<Animator>(); // Предполагаем, что скрипт прикреплен к объекту игрока
        enemyAnimator = GameObject.Find("Mummy_0").GetComponent<Animator>(); // Найдите объект врага по имени

        battle = FindAnyObjectByType<BattleTrigger>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack && battle.BattleIsStart)
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
        canAttack = false;
        playerAnimator.SetBool("turn", true);
        playerAnimator.SetTrigger("Attack");
        enemyHP -= 50; // Пример урона
        if (enemyHP <= 0)
        {
            Debug.Log("Вы выиграли");
            BattleIsEnd = true;
            EnemyDie();
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

    void EnemyAttack()
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
