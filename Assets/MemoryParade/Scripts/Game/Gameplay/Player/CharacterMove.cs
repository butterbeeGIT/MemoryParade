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
            Debug.LogError("Animator не найден на объекте " + gameObject.name);
        }

        _animator.SetFloat("X", 0);
        _animator.SetFloat("Y", 0);
    }

    void FixedUpdate()
    {

        if (_animator == null)
        {
            Debug.LogWarning("Animator равен null на объекте " + gameObject.name);
            return;
        }

        var move = GetMove();
        /*float lastX = 0;
        float lastY = 0;*/

        if (move != Vector2.zero)
        {
            _animator.SetFloat("X", move.x);
            _animator.SetFloat("Y", move.y);
            /*lastX = move.x;
            lastY = move.y;*/
            // Предыдущие значения для того, чтобы знать в какую сторону атаковать
            _animator.SetFloat("attackX", move.x);
            _animator.SetFloat("attackY", move.y);

            transform.Translate(move * 0.02f); // скорость
        }
        else
        {
            // Обноляем основные параметры, для того, чтобы персонаж стоял
            _animator.SetFloat("X", 0);
            _animator.SetFloat("Y", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        _animator.SetTrigger("Attack");
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

//using UnityEngine;
//using UnityEngine.InputSystem;

//public class CharacterMove : MonoBehaviour
//{
//    private static CharacterMove _instance;
//    private Animator _animator;
//    private Vector2 move;

//    void Awake()
//    {
//        if (_instance != null && _instance != this)
//        {
//            Debug.Log("Дубликат объекта CharacterMove найден и уничтожен.");
//            Destroy(gameObject);
//            return;
//        }

//        _instance = this;
//        //DontDestroyOnLoad(gameObject);
//    }

//    void Start()
//    {
//        _animator = GetComponent<Animator>() ?? GetComponentInChildren<Animator>();

//        if (_animator == null)
//        {
//            Debug.LogError("Animator не найден на объекте " + gameObject.name);
//            return;
//        }

//        _animator.SetFloat("X", 0);
//        _animator.SetFloat("Y", 0);
//    }

//    void Update()
//    {
//        if (this == null || !gameObject.activeInHierarchy) return;

//        // Считываем движение из клавиш. Проверка на null для Keyboard.current
//        if (Keyboard.current != null)
//        {
//            move = GetMove();
//        }
//    }

//    void FixedUpdate()
//    {
//        if (this == null || !gameObject.activeInHierarchy) return;

//        if (_animator == null)
//        {
//            Debug.LogWarning("Animator равен null на объекте " + gameObject.name);
//            return;
//        }

//        if (move != Vector2.zero)
//        {
//            _animator.SetFloat("X", move.x);
//            _animator.SetFloat("Y", move.y);
//            transform.Translate(move * 0.02f);
//        }
//        else
//        {
//            _animator.SetFloat("X", 0);
//            _animator.SetFloat("Y", 0);
//        }
//    }

//    private Vector2 GetMove()
//    {
//        Vector2 move = Vector2.zero;

//        if (Keyboard.current != null)
//        {
//            if (Keyboard.current.upArrowKey.isPressed) move += new Vector2(0, 1);
//            if (Keyboard.current.downArrowKey.isPressed) move += new Vector2(0, -1);
//            if (Keyboard.current.leftArrowKey.isPressed) move += new Vector2(-1, 0);
//            if (Keyboard.current.rightArrowKey.isPressed) move += new Vector2(1, 0);
//        }

//        return move == Vector2.zero ? move : move.normalized;
//    }
//}