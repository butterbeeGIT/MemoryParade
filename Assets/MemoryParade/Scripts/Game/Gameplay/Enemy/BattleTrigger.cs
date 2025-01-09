using Assets.MemoryParade.Scripts.Game.GameRoot;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    public GameObject battleCanvas;
    private CharacterMove playerMove;
    private Follow enemy;
    private CharacterAttack characterAttack;
    private Vector3 startPlayerPosition;

    public bool BattleIsStart = false;

    private CinemachineVirtualCamera camera;
    private Camera main;

    void Start()
    {
        /*enemyAnimator = GetComponent<Animator>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();*/

        playerMove = FindObjectOfType<CharacterMove>();
        characterAttack = FindObjectOfType<CharacterAttack>();
        enemy = FindObjectOfType<Follow>();
        camera = FindAnyObjectByType<CinemachineVirtualCamera>();

        main = FindObjectOfType<Camera>();
        startPlayerPosition = playerMove.transform.position;
        battleCanvas.SetActive(false); // Скрываем окно боя
    }

    void Update()
    {
        startPlayerPosition = playerMove.transform.position;
        if (Vector2.Distance(playerMove.transform.position, enemy.transform.position) < 0.1f)
        {
            StartBattle();
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
}
