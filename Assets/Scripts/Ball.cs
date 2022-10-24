using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    public float angle = -90;
    public float speed;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 direction = (Vector3)(Quaternion.Euler(0, 0, angle) * Vector3.right);
        Vector2 newPosition = transform.position + speed * Time.deltaTime * direction;
        rigidbody2d.MovePosition(newPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        if(tag.Equals("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(player.MoveDirection == -1)
            {
                angle = Random.Range(105f, 150f);
            }
            else if(player.MoveDirection == 1)
            {
                angle = Random.Range(15f, 60f);
            }
            else
            {
                angle = -angle;
            }
        }
        if (tag.Equals("Tile"))
        {
            angle = -angle;
            Destroy(collision.gameObject);
        }
    }
}
