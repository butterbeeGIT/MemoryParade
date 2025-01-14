using Assets.MemoryParade.Scripts.Game.Gameplay.Player;
using System.Collections;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    private TextMeshProUGUI playerHPText;
    private TextMeshProUGUI enemyHPText;

    public int attackCount = 0;
    public int powerAttackCount = 0;

    private int playerHP = 100;
    private int enemyHP = 100;
    private int playerDamage;

    private Animator playerAnimator;
    private Animator enemyAnimator;

    private BattleTrigger battle;
    private PowerAttack powerAttack;
    private SuperAttack superAttack;
    public bool BattleIsEnd = false;

    private bool canAttack = true;

    void Start()
    {
        playerAnimator = GetComponent<Animator>(); // Предполагаем, что скрипт прикреплен к объекту игрока
        //enemyAnimator = GameObject.Find("Mummy_0").GetComponent<Animator>(); // Найдите объект врага по имени

        
        powerAttack = FindAnyObjectByType<PowerAttack>();
        superAttack = FindAnyObjectByType<SuperAttack>();

        playerHPText = GameObject.Find("PlayerHP").GetComponent<TextMeshProUGUI>();
        enemyHPText = GameObject.Find("EnemyHP").GetComponent<TextMeshProUGUI>();
    }

    public void SetCurrentEnemyAnimator(BattleTrigger enemy)
    {
        enemyAnimator = enemy.GetComponent<Animator>();
        battle = enemy;
    }

    void Update()
    {
        if (playerHPText != null && enemyHPText != null)
        {
            playerHPText.text = playerHP.ToString();
            enemyHPText.text = enemyHP.ToString();
        }
        if (Input.GetKeyDown(KeyCode.Space) && canAttack && battle.BattleIsStart)
        {
            PlayerAttack();
        }
        else if (!powerAttack.click && !superAttack.click)
        {
            playerAnimator.SetBool("turn", false);
        }
    }
    public void Attack()
    {
        playerAnimator.SetBool("turn", true);
        playerAnimator.SetTrigger("Attack");
    }

    public void PlayerSuperAttack()
    {
        canAttack = false;
        Attack();
        playerDamage = 50;
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

    public void PlayerPowerAttack()
    {
        powerAttackCount++;
        canAttack = false;
        Attack();
        playerDamage = 10;
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

    void PlayerAttack()
    {
        attackCount++;
        canAttack = false;
        Attack();
        playerDamage = 2;
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
        enemyHP -= playerDamage;
        enemyAnimator.SetBool("turn", true);
        canAttack = true;
        // Возврат врага в базовую анимацию
        Invoke("ResetEnemyAnimation", 1f); // Время ожидания для завершения анимации атаки врага
    }

    void ResetEnemyAnimation()
    {
        enemyAnimator.SetBool("turn", false);
        int damage = Random.Range(1, 9); // Урон от врага
        playerHP -= damage;
    }
}
