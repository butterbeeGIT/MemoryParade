using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private Follow follow;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ���������, ��� � ��������� ���������� ��� "Player"
        {
            follow = GetComponent<Follow>(); 
            Debug.Log("�������� ���������!");
            follow.speed = 0.01f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        follow = GetComponent<Follow>();
        if (other.CompareTag("Player"))
        {
            Debug.Log("�������� ������� �������!");
            follow.speed = 0f;
        }
    }
}
