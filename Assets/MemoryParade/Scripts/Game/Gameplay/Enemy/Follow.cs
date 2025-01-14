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

    // Длина луча для проверки препятствий
    public float raycastDistance = 0.5f;
    // Слой, на котором находятся стены
    private LayerMask wallLayer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        wallLayer = LayerMask.GetMask("Map");
    }


    void FixedUpdate()
    {
        // Направление движения врага к игроку
        Vector2 direction = (player.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, wallLayer);

        if (hit.collider == null) // Если препятствий нет
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed);
        }

        //transform.position = Vector2.MoveTowards(transform.position, player.position, speed);
        //var direction = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);

        if (direction.x > 0)
            sr.flipX = true;
        else sr.flipX = false;

        //if (Vector2.Distance(player.transform.position, animator.transform.position) < 0.1f)
        if (Vector2.Distance(player.position, transform.position) < 0.1f)
        {
            animator.SetBool("stay", true);
            canBattle = true;
            sr.flipX = true;
        }  
    }
}