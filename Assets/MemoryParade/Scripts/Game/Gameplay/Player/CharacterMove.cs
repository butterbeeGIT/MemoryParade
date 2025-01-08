//using Unity.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMove : MonoBehaviour
{
    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.LogError("Animator �� ������ �� ������� " + gameObject.name);
        }

        _animator.SetFloat("X", 0);
        _animator.SetFloat("Y", 0);
    }

    void FixedUpdate()
    {

        if (_animator == null)
        {
            Debug.LogWarning("Animator ����� null �� ������� " + gameObject.name);
            return;
        }

        var move = GetMove();

        if (move != Vector2.zero)
        {
            _animator.SetFloat("X", move.x);
            _animator.SetFloat("Y", move.y);
            _animator.SetFloat("attackX", move.x);
            _animator.SetFloat("attackY", move.y);

            transform.Translate(move * 0.02f); // ��������
        }
        else
        {
            // �������� �������� ���������, ��� ����, ����� �������� �����
            _animator.SetFloat("X", 0);
            _animator.SetFloat("Y", 0);
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }*/
    }

    /*void Attack()
    {
        _animator.SetTrigger("Attack");
    }*/

    private Vector2 GetMove()
    {
        Vector2 move = Vector2.zero;
        if (Keyboard.current.upArrowKey.isPressed)
        {
            move += new Vector2(0, 1);
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            move += new Vector2(0, -1);
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            move += new Vector2(-1, 0);
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            move += new Vector2(1, 0);
        }

        return move == Vector2.zero ? move : move.normalized;
    }
}