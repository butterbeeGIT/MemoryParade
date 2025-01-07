using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private Follow follow;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Убедитесь, что у персонажа установлен тег "Player"
        {
            follow = GetComponent<Follow>(); 
            Debug.Log("Персонаж обнаружен!");
            // Здесь можно добавить логику для атаки или следования за персонажем
            follow.speed = 0.01f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        follow = GetComponent<Follow>();
        if (other.CompareTag("Player"))
        {
            Debug.Log("Персонаж покинул область!");
            // Здесь можно добавить логику для прекращения действия
            follow.speed = 0f;
        }
    }
}
