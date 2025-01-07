/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}*/
using UnityEngine;

public class Follow : MonoBehaviour
{
    public float speed = 2f;
    public Transform player;
    public SpriteRenderer sr;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed);
        var direction = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        if (direction.x > 0)
            sr.flipX = true;
        else sr.flipX = false;
    }
}