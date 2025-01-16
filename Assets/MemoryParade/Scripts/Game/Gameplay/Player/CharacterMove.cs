//using Unity.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMove : MonoBehaviour
{
    private Animator _animator;
    private GameObject _gameObject;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _gameObject = GameObject.Find("BattleCanvas");
        if (_animator == null)
        {
            Debug.LogError("Animator не найден на объекте " + gameObject.name);
        }

        _animator.SetFloat("X", 0);
        _animator.SetFloat("Y", 0);
    }

    void FixedUpdate()
    {
        if (_gameObject != null)
            _gameObject.SetActive(false);
        if (_animator == null)
        {
            Debug.LogWarning("Animator равен null на объекте " + gameObject.name);
            return;
        }

        var move = GetMove();

        if (move != Vector2.zero)
        {
            _animator.SetFloat("X", move.x);
            _animator.SetFloat("Y", move.y);

            transform.Translate(move * 0.02f); // скорость
        }
        else
        {
            // Обноляем основные параметры, для того, чтобы персонаж стоял
            _animator.SetFloat("X", 0);
            _animator.SetFloat("Y", 0);
        }
    }

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