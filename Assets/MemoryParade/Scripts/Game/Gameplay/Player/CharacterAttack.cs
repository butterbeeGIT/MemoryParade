
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAttack : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.LogError("Animator не найден на объекте " + gameObject.name);
        }
        //поставить персонажа в статичную позицию
        _animator.SetFloat("X", 0);
        _animator.SetFloat("Y", 0);

        //_animator.transform.position = new Vector3(_animator.transform.position.x + 1, (float)(_animator.transform.position.y + 0.37), 0);
    }

}