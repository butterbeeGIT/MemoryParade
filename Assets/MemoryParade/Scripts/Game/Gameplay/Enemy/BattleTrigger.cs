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
    private Vector3 startEnemyPosition;
    private CinemachineVirtualCamera camera;
    //public string battleSceneName;

    void Start()
    {
        playerMove = FindObjectOfType<CharacterMove>();
        characterAttack = FindObjectOfType<CharacterAttack>();
        enemy = FindObjectOfType<Follow>();
        camera = FindAnyObjectByType<CinemachineVirtualCamera>();
        //startPlayerPosition = playerMove.transform.position;
        startEnemyPosition = enemy.transform.position;
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
        battleCanvas.SetActive(true); // Показываем окно боя
        
        playerMove.enabled = false;
        characterAttack.enabled = true;

        enemy.enabled = false;
        camera.enabled = false;
        enemy.transform.position = new Vector3((float)(startPlayerPosition.x +1 ), (float)(startPlayerPosition.y + 0.42), 0);
        //playerMove.transform.position = new Vector3(startPlayerPosition.x - 1, (float)(startPlayerPosition.y + 0.37), 0);
        //player.EnableAttackMode(); // Включаем только режим атаки
        //player.DisableMovement(); // Отключаем движение игрока
        //enemy.DisableMovement(); // Отключаем движения врага
    }
}
