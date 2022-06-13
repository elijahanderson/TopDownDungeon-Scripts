using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector2 moveDelta;
    protected RaycastHit2D hit;
    protected float moveSpeed;

    protected override void Start() {
        base.Start();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector2 input) {
        // reset moveDelta
        moveDelta = new Vector2(input.x * moveSpeed, input.y * moveSpeed);

        // swap sprite direction
        if (moveDelta.x > 0) {
            transform.Find("Sprite").localScale = new Vector2(-1, 1);
        }
        else if (moveDelta.x < 0) {
            transform.Find("Sprite").localScale = new Vector2(1, 1);
        }

        // if there is a push, apply it
        moveDelta += pushDirection;
        // push recovery applied every frame
        pushDirection = Vector2.Lerp(pushDirection, Vector2.zero, pushRecoverySpeed);

        // Detect collisions with a raycast -- args: curr position,
        // collider size, angle, direction, distance, layers to check
        // Y Axis
        hit = Physics2D.BoxCast(transform.position,
                                boxCollider.size,
                                0,
                                new Vector2(0, moveDelta.y),
                                Mathf.Abs(moveDelta.y * Time.deltaTime),
                                LayerMask.GetMask("Character", "Blocking"));
        // move
        if (hit.collider == null)
            transform.Translate(new Vector2(0, moveDelta.y) * Time.deltaTime);

        // X Axis
        hit = Physics2D.BoxCast(transform.position,
                                boxCollider.size,
                                0,
                                new Vector2(moveDelta.x, 0),
                                Mathf.Abs(moveDelta.x * Time.deltaTime),
                                LayerMask.GetMask("Character", "Blocking"));
        // move if moveDelta is not zero
        if (hit.collider == null)
            transform.Translate(new Vector2(moveDelta.x, 0) * Time.deltaTime);
    }

    protected override void Die() {
        base.Die();
        boxCollider.enabled = false;
    }
}
