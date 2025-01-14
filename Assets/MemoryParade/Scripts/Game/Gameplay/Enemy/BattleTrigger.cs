using Assets.MemoryParade.Scripts.Game.Gameplay.Enemy;
using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    private Follow follow;
    private GameObject battleCanvas;
    private CharacterMove playerMove;
    private GameObject player;
    //private Follow enemy;
    private CharacterAttack characterAttack;
    private Vector3 startPlayerPosition;

    public bool BattleIsStart = false;
    private BattleSystem battleSystem;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private Camera main;

    void Start()
    {
        follow = GetComponent<Follow>();

        if (follow == null)
        {
            Debug.LogWarning($"Не найден скрипт Follow");
        }
        if (battleCanvas == null)
            battleCanvas = BattleCanvasManager.Instance;
        if (battleCanvas == null)
        {
            Debug.LogWarning($"Не найден BattleCanvas");
        }
        player = GameObject.FindGameObjectWithTag("Player");
        playerMove = player.GetComponent<CharacterMove>();
        characterAttack = FindObjectOfType<CharacterAttack>();
        //enemy = FindObjectOfType<Follow>();
        cinemachineVirtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();

        battleSystem = FindAnyObjectByType<BattleSystem>();

        main = FindObjectOfType<Camera>();
        startPlayerPosition = playerMove.transform.position;
        battleCanvas.SetActive(false); // Скрываем окно боя
    }

    void Update()
    {
        startPlayerPosition = playerMove.transform.position;
        if (!battleSystem.BattleIsEnd && follow.canBattle)// && Vector2.Distance(playerMove.transform.position, enemy.transform.position) < 0.1f)
        {
            BattleSystem battleSystem = player.GetComponent<BattleSystem>();
            battleSystem.SetCurrentEnemyAnimator(GetComponent<Animator>());
            StartBattle();
        }
        if (battleSystem.BattleIsEnd)
        {
            StartCoroutine(Waiter());
            //EndBattle();
        }
    }

    void StartBattle()
    {
        // Увеличиваем обу камеры, для того, чтобы приблизить игрока и врага
        cinemachineVirtualCamera.m_Lens.OrthographicSize = (float)1.533734;
        main.orthographicSize = (float)1.533734;
        // Показываем окно боя
        battleCanvas.SetActive(true);
        // Отключаем скрипт для передвижения персонажа и включаем скрипт дляя атаки
        playerMove.enabled = false;
        characterAttack.enabled = true;
        // Отключаем скрипт для врага. Чтобы он не следовал за персонажем
        follow.enabled = false;
        // Отключаем камеру персонажа
        cinemachineVirtualCamera.enabled = false;
        // Двигаем врага на платформу
        follow.transform.position = new Vector3((float)(startPlayerPosition.x - 0.8), (float)(startPlayerPosition.y + 0.42), 0);
        BattleIsStart = true;
    }
    void EndBattle()
    {
        // Возвращаем все на начальные позиции
        cinemachineVirtualCamera.m_Lens.OrthographicSize = (float)3.5;
        main.orthographicSize = (float)3.5;
        // Показываем окно боя
        battleCanvas.SetActive(false);
        // Отключаем скрипт для передвижения персонажа и включаем скрипт дляя атаки
        playerMove.enabled = true;
        characterAttack.enabled = false;
        // Отключаем скрипт для врага. Чтобы он не следовал за персонажем
        follow.enabled = false;
        // Отключаем камеру персонажа
        cinemachineVirtualCamera.enabled = true;
        // Двигаем врага на платформу
        //enemy.transform.position = new Vector3((float)(startPlayerPosition.x - 0.8), (float)(startPlayerPosition.y + 0.42), 0);
    }
    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(3f);
        EndBattle();
    }
}

