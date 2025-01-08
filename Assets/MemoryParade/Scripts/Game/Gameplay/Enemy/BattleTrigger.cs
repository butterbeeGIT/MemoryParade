using Assets.MemoryParade.Scripts.Game.GameRoot;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    public GameObject battleCanvas; // Ссылка наCanvas с окном боя
    private CharacterMove playerMove; // Ваш скрипт игрока
    private Follow enemy; // Ваш скрипт врага
    private CharacterAttack characterAttack;
    private Vector3 startPlayerPosition;
    //private Vector3 startEnemyPosition;
    private CinemachineVirtualCamera camera;

    private Camera main;
    //public string battleSceneName;

    void Start()
    {
        playerMove = FindObjectOfType<CharacterMove>();
        characterAttack = FindObjectOfType<CharacterAttack>();
        enemy = FindObjectOfType<Follow>();
        camera = FindAnyObjectByType<CinemachineVirtualCamera>();

        main = FindObjectOfType<Camera>();
        startPlayerPosition = playerMove.transform.position;
       // startEnemyPosition = enemy.transform.position;
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

        //SceneManager.LoadScene("Fight");
        //Увеличиваем обу камеры, для того, чтобы приблизить игрока и врага
        camera.m_Lens.OrthographicSize = (float)1.533734;
        main.orthographicSize = (float)1.533734;
        //camera.Follow = null;
        battleCanvas.SetActive(true); // Показываем окно боя
        
        playerMove.enabled = false;
        characterAttack.enabled = true;

        enemy.enabled = false;
        //camera.m_Lens.OrthographicSize = (float)1.533734;
        camera.enabled = false;
        //characterAttack.transform.position = new Vector3(characterAttack.transform.position.x - 1, (float)(characterAttack.transform.position.y + 0.37), 0);
        enemy.transform.position = new Vector3((float)(startPlayerPosition.x +1 ), (float)(startPlayerPosition.y + 0.42), 0);
        //playerMove.transform.position = new Vector3(startPlayerPosition.x - 1, (float)(startPlayerPosition.y + 0.37), 0);
        //player.EnableAttackMode(); // Включаем только режим атаки
        //player.DisableMovement(); // Отключаем движение игрока
        //enemy.DisableMovement(); // Отключаем движения врага
    }
}
