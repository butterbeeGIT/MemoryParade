using Assets.MemoryParade.Scripts.Game.GameRoot;
using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    private GameObject battleCanvas;
    private CharacterMove playerMove;
    private Follow enemy;
    private CharacterAttack characterAttack;
    private Vector3 startPlayerPosition;

    public bool BattleIsStart = false;
    private BattleSystem battleSystem;

    private CinemachineVirtualCamera camera;
    private Camera main;
    private PlayerСharacteristics сharacteristics;

    void Start()
    {
        battleCanvas = GameObject.Find("BattleCanvas");
        playerMove = FindObjectOfType<CharacterMove>();
        characterAttack = FindObjectOfType<CharacterAttack>();
        enemy = FindObjectOfType<Follow>();
        camera = FindAnyObjectByType<CinemachineVirtualCamera>();

        сharacteristics = FindAnyObjectByType<PlayerСharacteristics>();
        battleSystem = FindAnyObjectByType<BattleSystem>();

        main = FindObjectOfType<Camera>();
        startPlayerPosition = playerMove.transform.position;
        battleCanvas.SetActive(false); // Скрываем окно боя
    }

    void Update()
    {
        startPlayerPosition = playerMove.transform.position;
        if (!battleSystem.BattleIsEnd && Vector2.Distance(playerMove.transform.position, enemy.transform.position) < 0.1f)
        {
            StartBattle();
        }
        if (battleSystem.BattleIsEnd && Vector2.Distance(playerMove.transform.position, enemy.transform.position) < 0.1f)
        {
            сharacteristics.numberOfWins++;
            Destroy(gameObject);
        }
        if (battleSystem.BattleIsEnd)
        {
            StartCoroutine(WaiterEnemyDie());
            //EndBattle();
        }
        if (battleSystem.PlayerLose)
        {
            StartCoroutine(WaiterPlayerDie());
            //SceneTransitionManager.Instance.GoToScene(Scenes.LOBBY);
        }
    }

    void StartBattle()
    {
        // Увеличиваем обу камеры, для того, чтобы приблизить игрока и врага
        camera.m_Lens.OrthographicSize = (float)1.533734;
        main.orthographicSize = (float)1.533734;
        // Показываем окно боя
        battleCanvas.SetActive(true); 
        // Отключаем скрипт для передвижения персонажа и включаем скрипт дляя атаки
        playerMove.enabled = false;
        characterAttack.enabled = true;
        // Отключаем скрипт для врага. Чтобы он не следовал за персонажем
        enemy.enabled = false;
        // Отключаем камеру персонажа
        camera.enabled = false;
        // Двигаем врага на платформу
        enemy.transform.position = new Vector3((float)(startPlayerPosition.x - 0.8 ), (float)(startPlayerPosition.y + 0.42), 0);
        BattleIsStart = true;
    }
    void EndBattle()
    {
        // Возвращаем все на начальные позиции
        camera.m_Lens.OrthographicSize = (float)3.5;
        main.orthographicSize = (float)3.5;
        // Показываем окно боя
        battleCanvas.SetActive(false);
        // Отключаем скрипт для передвижения персонажа и включаем скрипт дляя атаки
        playerMove.enabled = true;
        characterAttack.enabled = false;
        // Отключаем скрипт для врага. Чтобы он не следовал за персонажем
        enemy.enabled = false;
        // Отключаем камеру персонажа
        camera.enabled = true;
    }
    IEnumerator WaiterEnemyDie()
    {
        yield return new WaitForSeconds(3f);
        EndBattle();
    }

    IEnumerator WaiterPlayerDie()
    {
        yield return new WaitForSeconds(3f);
        SceneTransitionManager.Instance.GoToScene(Scenes.LOBBY);
    }
}
