using Assets.MemoryParade.Scripts.Game.Gameplay.Enemy;
using Assets.MemoryParade.Scripts.Game.GameRoot;
using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    private Follow enemyFollow;
    //private GameObject battleCanvas;
    private CharacterMove playerMove;
    private GameObject player;
    private CharacterAttack characterAttack;
    private SpriteRenderer spriteRendererEnemy;

    public bool BattleIsStart = false;
    private BattleSystem battleSystem;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private Camera main;
    private Vector3 startPos;

    private bool init = false;

    void Init()
    {
        init = true;
        enemyFollow = GetComponent<Follow>();

        if (enemyFollow == null)
        {
            Debug.LogWarning($"Не найден скрипт Follow");
        }

        //if (battleCanvas == null)
        //{
        //    Debug.LogWarning($"Не найден BattleCanvas");
        //}
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
            Debug.LogWarning($"player Не найден");
        playerMove = player.GetComponent<CharacterMove>();
        characterAttack = FindObjectOfType<CharacterAttack>();

        cinemachineVirtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();

        battleSystem = player.GetComponent<BattleSystem>();
        spriteRendererEnemy = gameObject.GetComponent<SpriteRenderer>();
        startPos = playerMove.transform.position;
        main = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (!init) Init();
        Debug.LogWarning($"init {init}");
        enemyFollow.SetCurrentFollowEnemy(this);
        //battleCanvas = enemyFollow.battleCanvas;
        startPos = playerMove.transform.position;
       
        if (!battleSystem.BattleIsEnd && enemyFollow.canBattle && !battleSystem.battleCanvas.activeSelf)
        {
            battleSystem.SetCurrentEnemyAnimator(this);
            StartBattle();
        }
        if (battleSystem.BattleIsEnd && Vector2.Distance(playerMove.transform.position, enemyFollow.transform.position) < 0.1f)
        {
            //PlayerСharacteristics.Instance.numberOfWins = PlayerСharacteristics.Instance.numberOfWins + 1;

            PlayerСharacteristics.Instance.AddScore();
            Destroy(gameObject);
        }
        if (battleSystem.BattleIsEnd && battleSystem.battleCanvas.activeSelf && enemyFollow.canBattle)
        {
            StartCoroutine(WaiterEnemyDie());
        }
        if (battleSystem.PlayerLose && battleSystem.battleCanvas.activeSelf && enemyFollow.canBattle)
        {
            StartCoroutine(WaiterEnemyDie());
        }
    }
    public void ChangePositionGameObject(GameObject obj)
    {
        //учет приближения камеры
        float cameraApp = 1.533734f / 3.5f;

        BoxCollider2D boxCollider = obj.GetComponent<BoxCollider2D>();
        float halfHeight = boxCollider.size.y / 2 * obj.transform.localScale.y;

        //выравнивает
        Vector3 shiftPositionPlayer = new Vector3(2.26f * cameraApp, -0.08f * cameraApp + halfHeight, 0);
        Vector3 shiftPositionEnemy = new Vector3(-2.13f * cameraApp, -0.08f * cameraApp + halfHeight, 0);
        Vector3 pos = battleSystem.battleCanvas.transform.position;
        pos.z = 0;

        if (obj.CompareTag("Player"))
            obj.transform.position = pos + shiftPositionPlayer;
        else if (obj.CompareTag("Enemy"))
            obj.transform.position = pos + shiftPositionEnemy;
    }
    void StartBattle()
    {
        Debug.Log($"StartBattle");
        //// Увеличиваем обу камеры, для того, чтобы приблизить игрока и врага
        //cinemachineVirtualCamera.m_Lens.OrthographicSize = BattleCanvasManager.orthographicSize;
        UpdateCamera(true);

        // Показываем окно боя
        battleSystem.battleCanvas.SetActive(true);
        // Отключаем скрипт для передвижения персонажа и включаем скрипт дляя атаки
        ChangePositionGameObject(player);
        playerMove.enabled = false;
        //двигаем персонажа на платформу
        characterAttack.enabled = true;
        ChangePositionGameObject(gameObject);
        //characterAttack.transform.position = new Vector3(startPos.x + 1, (float)(startPos.y + 0.37), 0);
        // Отключаем скрипт для врага. Чтобы он не следовал за персонажем
        enemyFollow.enabled = false;
        
        // Двигаем врага на платформу
        
        //enemyFollow.transform.position = new Vector3((float)(characterAttack.transform.position.x -0.8), (float)(characterAttack.transform.position.y + 0.4), 0);
        spriteRendererEnemy.sortingOrder = 5;
        BattleIsStart = true;
    }
    void EndBattle()
    {
        Debug.Log($"EndBattle");
        // Возвращаем все на начальные позиции
        //cinemachineVirtualCamera.m_Lens.OrthographicSize = (float)3.5;
        Vector3 pos = battleSystem.battleCanvas.transform.position;
        pos.z = 0;
        player.transform.position = pos;
        // Включаем камеру персонажа
        UpdateCamera(false);

        // выключаем окно боя
        battleSystem.battleCanvas.SetActive(false);
        // Включаем скрипт для передвижения персонажа и выключаем скрипт дляя атаки
        playerMove.enabled = true;
        characterAttack.enabled = false;
        spriteRendererEnemy.sortingOrder = 1;
        
        battleSystem.BattleIsEnd = false;
        main.Render();
        init = false;
    }

    private void UpdateCamera(bool zoom)
    {
        if (zoom)
        {
            // Отключаем камеру персонажа
            cinemachineVirtualCamera.enabled = false;
            main.orthographicSize = BattleCanvasManager.orthographicSize;
        }
        else
        {
            main.orthographicSize = (float)3.5;
            Camera.main.transform.position = player.transform.position;
            Camera.main.ResetAspect();
            cinemachineVirtualCamera.enabled = true;
        }
        var brain = Camera.main.GetComponent<CinemachineBrain>();
        brain.ManualUpdate(); // Принудительное обновление виртуальной камеры
        //Camera.main.Render();
    }
    
    IEnumerator WaiterEnemyDie()
    {
        Debug.Log($"Waiter");
        enemyFollow.canBattle = false;
        yield return new WaitForSeconds(3f);
        EndBattle();
    }

    IEnumerator WaiterPlayerDie()
    {
        yield return new WaitForSeconds(3f);
        PlayerСharacteristics.Instance.healthPoints = 100;
        SceneTransitionManager.Instance.GoToScene(Scenes.LOBBY);
    }
}

