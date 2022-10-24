using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float LEFT_BOUND = -9.6f;
    private const float RIGHT_BOUND = 9.6f;

    public KeyCode leftButton;
    public KeyCode rightButton;
    public float speed;
    public int movingFramesThreshold;

    private Rigidbody2D rigidbody2d;

    private int _moveDirection = 0;
    private int movingRemainingFrames = 0;
    public int MoveDirection
    {
        get => _moveDirection;
    }

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(movingRemainingFrames <= 0)
        {
            _moveDirection = 0;
        }
        else
        {
            movingRemainingFrames--;
        }

        if (Input.GetKey(leftButton) && transform.position.x > LEFT_BOUND)
        {
            Vector2 newPosition = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            rigidbody2d.MovePosition(newPosition);
            _moveDirection = -1;
            movingRemainingFrames = movingFramesThreshold;
        }
        
        if (Input.GetKey(rightButton) && transform.position.x < RIGHT_BOUND)
        {
            Vector2 newPosition = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            rigidbody2d.MovePosition(newPosition);
            _moveDirection = 1;
            movingRemainingFrames = movingFramesThreshold;
        }
    }
}
