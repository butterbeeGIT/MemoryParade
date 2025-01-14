using UnityEngine;
/// <summary>
/// Движение врага по направлению к игроку
/// </summary>
public class Follow : MonoBehaviour
{
    public float speed;
    private Transform player;
    private SpriteRenderer sr;
    private Animator animator;
    public bool canBattle = false;
    //зона видимости
    private float visibilityArea;

    // Длина луча для проверки препятствий
    public float raycastDistance = 0.5f;
    // Слой, на котором находятся стены
    private LayerMask wallLayer;

    void Awake()
    {
        speed = 0.01f;
        visibilityArea = 2f;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        wallLayer = LayerMask.GetMask("Map");
    }


    void FixedUpdate()
    {
        if (Vector2.Distance(player.position, transform.position) < visibilityArea)
        {
            Debug.Log("Персонаж обнаружен!");
            // Направление движения врага к игроку
            Vector2 direction = (player.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, wallLayer);

            if (hit.collider == null) // Если препятствий нет
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed);
            }

            if (direction.x > 0)
                sr.flipX = true;
            else sr.flipX = false;

            if (Vector2.Distance(player.position, transform.position) < 0.1f)
            {
                animator.SetBool("stay", true);
                canBattle = true;
                sr.flipX = true;
            }
        }
        else
        {
            Debug.Log("Персонаж покинул область!");
            //speed = 0f;
        }    
    }
}