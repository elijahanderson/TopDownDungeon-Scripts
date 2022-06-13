using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public float triggerLength;  // if player is within this distance, enemy begins to chase them
    public float chaseLength;  // if player is outside of this distance, enemy stops chasing them

    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector2 playerPosition;
    private Vector2 startingPosition;
    private Vector2 currPosition;

    // hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start() {
        base.Start();
        playerTransform = GameManager.gameManagerInstance.player.transform;
        // the hitbox is technically a child of the main enemy
        startingPosition = new Vector2(transform.position.x, transform.position.y);
        hitbox = transform.Find("Hitbox").GetComponent<BoxCollider2D>();
        collidingWithPlayer = false;
    }

    private void FixedUpdate() {
        // check if player is in range (between startingPosition and chaseLength)
        if (!isDead) {
            currPosition = new Vector2(transform.position.x, transform.position.y);
            playerPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);
                if (Vector2.Distance(playerPosition, startingPosition) < chaseLength) {
                    if (Vector2.Distance(playerPosition, startingPosition) < triggerLength)
                        chasing = true;
                    // move towards player if chasing
                    if (chasing) {
                        UpdateMotor((playerPosition - currPosition).normalized);
                    } else {
                        UpdateMotor(startingPosition - currPosition);
                    }
                } else if (!(Mathf.Abs(startingPosition.x - currPosition.x) < 0.02f)) {
                    UpdateMotor(startingPosition - currPosition);
                    chasing = false;
                }

            // check if enemy is colliding with player
            boxCollider.OverlapCollider(filter, hits);
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i] != null && hits[i].tag == "Fighter" && hits[i].name == "Player") {
                    collidingWithPlayer = true;
                }
                hits[i] = null;
            }
        }
    }

    protected override void Die() {
        base.Die();
        GameManager.gameManagerInstance.experience += 1;
    }
}
