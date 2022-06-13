using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    protected Vector2 target;
    protected RaycastHit2D hitX;
    protected RaycastHit2D hitY;

    protected override void Start() {
        base.Start();
        xSpeed = 0.75f;
        ySpeed = 0.75f;
        hitpoint = 100.0f;
        maxHitpoint = 100.0f;
        healthRegenRate = 0.5f;
        mana = 50.0f;
        maxMana = 50.0f;
        manaRegenRate = 0.5f;
        target = transform.position;
    }

    protected virtual void Update() {
        // if mouse is clicked down, get its position and set it as the movement target
        if(Input.GetMouseButton(0) || Input.GetMouseButton(4)) {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    protected virtual void FixedUpdate() {
        // swap sprite direction
        if (target.x > transform.position.x) {
            transform.Find("Sprite").localScale = new Vector2(-1, 1);
            transform.Find("WeaponLeft").gameObject.SetActive(false);
            transform.Find("WeaponRight").gameObject.SetActive(true);
        }
        else if (target.x < transform.position.x) {
            transform.Find("Sprite").localScale = new Vector2(1, 1);
            transform.Find("WeaponRight").gameObject.SetActive(false);
            transform.Find("WeaponLeft").gameObject.SetActive(true);
        }

        // check for collisions
        hitY = Physics2D.BoxCast(transform.position,
                                boxCollider.size,
                                0,
                                new Vector2(0, target.y * Time.deltaTime * ySpeed),
                                Mathf.Abs(target.y * Time.deltaTime),
                                LayerMask.GetMask("Character", "Blocking"));
        hitX = Physics2D.BoxCast(transform.position,
                                boxCollider.size,
                                0,
                                new Vector2(target.x * Time.deltaTime * xSpeed, 0),
                                Mathf.Abs(target.x * Time.deltaTime),
                                LayerMask.GetMask("Character", "Blocking"));

        // if player hasn't reached target, move
        if (Vector2.Distance(target, transform.position) > 0.01f) {
            // transform.position = Vector2.MoveTowards(transform.position, target, xSpeed * Time.deltaTime);
            if (hitX.collider == null && hitY.collider == null)
                transform.position = Vector2.MoveTowards(transform.position, target, xSpeed * Time.deltaTime);
            else if (hitX.collider == null && hitY.collider != null)
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, 0), xSpeed * Time.deltaTime);
            else if (hitX.collider != null && hitY.collider == null)
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, target.y), xSpeed * Time.deltaTime);
        }
    }
}
