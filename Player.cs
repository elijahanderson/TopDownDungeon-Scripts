using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    protected Vector2 target;
    protected RaycastHit2D hitX;
    protected RaycastHit2D hitY;

    private float dashCooldown;
    private float dashStaminaCost;
    private float dashSpeed;
    private float lastDash;
    private bool isDashing;

    protected override void Start() {
        base.Start();
        moveSpeed = 0.75f;
        hitpoint = 100.0f;
        maxHitpoint = 100.0f;
        healthRegenRate = 2.0f;
        mana = 10.0f;
        maxMana = 10.0f;
        manaRegenRate = 0.5f;
        stamina = 10.0f;
        maxStamina = 10.0f;
        staminaRegenRate = 2.0f;
        dashCooldown = 1.0f;
        dashStaminaCost = 5.0f;
        dashSpeed = 2.0f;
        lastDash = Time.time;
        isDashing = false;
        target = transform.position;
    }

    protected override void Update() {
        base.Update();
        // if mouse is clicked down, get its position and set it as the movement target
        if ((Input.GetMouseButton(0) || Input.GetMouseButton(4)) && !isDashing)
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // dash mechanic
        if (Input.GetKey(KeyCode.Space) && Time.time - lastDash >= dashCooldown && stamina > 0)
            StartCoroutine("Dash");
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
                                new Vector2(0, target.y * Time.deltaTime * moveSpeed),
                                Mathf.Abs(target.y * Time.deltaTime),
                                LayerMask.GetMask("Character", "Blocking"));
        hitX = Physics2D.BoxCast(transform.position,
                                boxCollider.size,
                                0,
                                new Vector2(target.x * Time.deltaTime * moveSpeed, 0),
                                Mathf.Abs(target.x * Time.deltaTime),
                                LayerMask.GetMask("Character", "Blocking"));

        // if player hasn't reached target, move
        if (Vector2.Distance(target, transform.position) > 0.01f) {
            if (hitX.collider == null && hitY.collider == null)
                transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            else if (hitX.collider == null && hitY.collider != null)
                transform.position = Vector2.MoveTowards(transform.position,
                                                         new Vector2(target.x, transform.position.y),
                                                         moveSpeed * Time.deltaTime);
            else if (hitX.collider != null && hitY.collider == null)
                transform.position = Vector2.MoveTowards(transform.position,
                                                         new Vector2(transform.position.x, target.y),
                                                         moveSpeed * Time.deltaTime);
        }
    }

    private IEnumerator Dash() {
        moveSpeed *= dashSpeed;
        stamina -= dashStaminaCost;
        lastDash = Time.time;
        isDashing = true;
        yield return new WaitForSeconds(0.5f);
        moveSpeed /= dashSpeed;
        isDashing = false;
    }
}
