using Assets.MemoryParade.Scripts.Game.Gameplay.Enemy;
using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    private Follow enemyFollow;
    private GameObject battleCanvas;
    private CharacterMove playerMove;
    private GameObject player;
    private CharacterAttack characterAttack;
    private Vector3 startPlayerPosition;

    public bool BattleIsStart = false;
    private BattleSystem battleSystem;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private Camera main;

    void Start()
    {
        enemyFollow = GetComponent<Follow>();

        if (enemyFollow == null)
        {
            Debug.LogWarning($"Не найден скрипт Follow");
        }
        if (battleCanvas == null)
        {
            battleCanvas = BattleCanvasManager.Instance;
            if (battleCanvas == null)
            {
                Debug.LogWarning($"Не найден BattleCanvas");
            }
        }    
        player = GameObject.FindGameObjectWithTag("Player");
        playerMove = player.GetComponent<CharacterMove>();
        characterAttack = FindObjectOfType<CharacterAttack>();

        cinemachineVirtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();

        battleSystem = player.GetComponent<BattleSystem>();

        main = FindObjectOfType<Camera>();
        startPlayerPosition = playerMove.transform.position;
        battleCanvas.SetActive(false); // Скрываем окно боя
    }

    void Update()
    {
        startPlayerPosition = playerMove.transform.position;
        if (!battleSystem.BattleIsEnd && enemyFollow.canBattle && !battleCanvas.activeSelf)
        {
            battleSystem.SetCurrentEnemyAnimator(this);
            StartBattle();
        }
        if (battleSystem.BattleIsEnd)
        {
            StartCoroutine(Waiter());
        }
    }

    void StartBattle()
    {
        // Увеличиваем обу камеры, для того, чтобы приблизить игрока и врага
        cinemachineVirtualCamera.m_Lens.OrthographicSize = BattleCanvasManager.orthographicSize;
        main.orthographicSize = BattleCanvasManager.orthographicSize;
        // Показываем окно боя
        battleCanvas.SetActive(true);
        // Отключаем скрипт для передвижения персонажа и включаем скрипт дляя атаки
        playerMove.enabled = false;
        //двигаем персонажа на платформу
        ///player.transform.position = battleCanvas.transform.position + BattleCanvasManager.shiftPositionPlayer;
        BattleCanvasManager.ChangePositionGameObject(player);
        characterAttack.enabled = true;
        // Отключаем скрипт для врага. Чтобы он не следовал за персонажем
        enemyFollow.enabled = false;
        // Отключаем камеру персонажа
        cinemachineVirtualCamera.enabled = false;
        // Двигаем врага на платформу 
        BattleCanvasManager.ChangePositionGameObject(gameObject);
        ///enemyFollow.transform.position = battleCanvas.transform.position + BattleCanvasManager.shiftPositionEnemy;

        //enemyFollow.transform.position = new Vector3((float)(startPlayerPosition.x - 0.8), (float)(startPlayerPosition.y + 0.42), 0);
        BattleIsStart = true;
    }
    void EndBattle()
    {
        // Возвращаем все на начальные позиции
        cinemachineVirtualCamera.m_Lens.OrthographicSize = (float)3.5;
        main.orthographicSize = (float)3.5;
        // Показываем окно боя
        battleCanvas.SetActive(false);
        // Включаем скрипт для передвижения персонажа и выключаем скрипт дляя атаки
        playerMove.enabled = true;
        characterAttack.enabled = false;

        // Включаем камеру персонажа
        cinemachineVirtualCamera.enabled = true;
    }
    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(3f);
        EndBattle();
    }
}

