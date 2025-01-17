using Assets.MemoryParade.Scripts.Game.Gameplay.Player;
using System.Collections;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.MemoryParade.Scripts.Game.GameRoot;

public class BattleSystem : MonoBehaviour
{
    private TextMeshProUGUI playerHPText;
    private TextMeshProUGUI enemyHPText;

    public int attackCount = 0;
    public int powerAttackCount = 0;

    public GameObject battleCanvas;
    private int playerHP;// = 100;
    private int enemyHP = 100;
    private int playerDamage;

    private Animator playerAnimator;
    private Animator enemyAnimator;

    private BattleTrigger battle;
    private PowerAttack powerAttack;
    private SuperAttack superAttack;
    private PlayerСharacteristics сharacteristics;

    public bool BattleIsEnd = false;
    public bool PlayerLose = false;

    private bool canAttack = true;

    void Start()
    {
        playerAnimator = GetComponent<Animator>(); // Предполагаем, что скрипт прикреплен к объекту игрока
        //enemyAnimator = GameObject.Find("Mummy_0").GetComponent<Animator>(); // Найдите объект врага по имени

        battle = FindAnyObjectByType<BattleTrigger>();

        сharacteristics = PlayerСharacteristics.Instance;//FindAnyObjectByType<PlayerСharacteristics>();

        //battleCanvas = GameObject.Find("BattleCanvas");
        if (battleCanvas == null)
        {
            Debug.LogWarning($"Не найден BattleCanvas");
        }
        playerHP = сharacteristics.healthPoints;

        playerHPText = GameObject.Find("PlayerHP").GetComponent<TextMeshProUGUI>();
        enemyHPText = GameObject.Find("EnemyHP").GetComponent<TextMeshProUGUI>();

        powerAttack = FindAnyObjectByType<PowerAttack>();
        superAttack = FindAnyObjectByType<SuperAttack>();
        
        if (SceneManager.GetActiveScene().name == Scenes.GAMEPLAY)
            battleCanvas.SetActive(false);

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
    public void SetCurrentEnemyAnimator(BattleTrigger enemy)
    {
        enemyAnimator = enemy.GetComponent<Animator>();
        battle = enemy;
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
        playerDamage = сharacteristics.baseAttack*3;
        Invoke("EnemyAttack", 1f);
    }

    public void PlayerPowerAttack()
    {
        powerAttackCount++;
        canAttack = false;
        Attack();
        playerDamage = сharacteristics.baseAttack*2;
        Invoke("EnemyAttack", 1f);
    }

    void PlayerAttack()
    {
        attackCount++;
        canAttack = false;
        Attack();
        //playerDamage = 50;
        playerDamage = сharacteristics.baseAttack;
        // После этого запускаем анимацию врага
        Invoke("EnemyAttack", 1f); // Время ожидания для завершения анимации атаки игрока
    }

    public void BattleEnd()
    {        
        powerAttackCount = 0;
        attackCount = 0;
        canAttack = true;
        enemyHP = 100;
    }
    void EnemyDie()
    {
        enemyAnimator.SetBool("die", true);
        //enemyAnimator.transform.position = new Vector3(enemyAnimator.transform.position.x, (float)(enemyAnimator.transform.position.y - 0.3), 0);
        Invoke("Treasure", 3f);
    }

    void PlayerDie()
    {
        playerAnimator.SetTrigger("die");
        playerAnimator.transform.position = new Vector3(playerAnimator.transform.position.x, (float)(playerAnimator.transform.position.y - 0.3), 0);
    }
    void Treasure()
    {
        enemyAnimator.SetTrigger("treasure");
        enemyAnimator.transform.localScale = new Vector3(1.5f, 1.5f, 0);
    }
    public void EnemyAttack()
    {
        enemyHP -= playerDamage;
        if (enemyHP <= 0)
        {
            enemyHP = 0;
            Debug.Log("Вы выиграли");
            EnemyDie();
            BattleIsEnd = true;
            сharacteristics.healthPoints = playerHP;
            //BattleEnd();
            playerAnimator.SetTrigger("battleIsEnd");
            return;
        }
        enemyAnimator.SetBool("turn", true);
        canAttack = true;
        // Возврат врага в базовую анимацию
        Invoke("ResetEnemyAnimation", 1f); // Время ожидания для завершения анимации атаки врага
    }

    void ResetEnemyAnimation()
    {
        enemyAnimator.SetBool("turn", false);
        int damage = Random.Range(1, 9); // Урон от врага 1-9
        playerHP -= damage;
        if (playerHP <= 0)
        {
            playerHP = 0;
            Debug.Log("Вы проиграли");
            //playerAnimator.SetTrigger("battleIsEnd");
            PlayerDie();
            //BattleEnd();
            PlayerLose = true;
        }
    }
}