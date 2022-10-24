using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    public float angle = -90;
    public float speed;
    public GameManager gameManager;
    public int noCollisionFramesThreshold = 2;
    private int _noCollisionFramesRemaining = 0;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(_noCollisionFramesRemaining > 0)
        {
            _noCollisionFramesRemaining--;
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = (Vector3)(Quaternion.Euler(0, 0, angle) * Vector3.right);
        Vector2 newPosition = transform.position + speed * Time.fixedDeltaTime * direction;
        rigidbody2d.MovePosition(newPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.MoveDirection == -1)
            {
                angle = Random.Range(110f, 130f);
            }
            else if (player.MoveDirection == 1)
            {
                angle = Random.Range(50f, 70f);
            }
            else
            {
                angle = -angle;
            }
        }
        else if (tag.Equals("Tile"))
        {
            if (_noCollisionFramesRemaining > 0) return;
            angle = -angle;
            Destroy(collision.gameObject);
            _noCollisionFramesRemaining = noCollisionFramesThreshold;
        }
        else if (tag.Equals("LoseCollider"))
        {
            gameManager.PlayerDied();
        }
        else if (tag.Equals("SideCollider"))
        {
            angle = 180 - angle;
        }
        else if (tag.Equals("UpCollider"))
        {
            angle = -angle;
        }
    }
}
