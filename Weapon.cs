using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Interactable
{
    // damage
    public Player player;
    public int damagePoint;
    public float pushForce;
    public int weaponLevel;
    public int manaCost;

    private SpriteRenderer spriteRenderer;
    private Animator swingAnimation;

    // attacking
    private float cooldown;
    private float lastSwing;

    protected override void Start() {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        damagePoint = 10;
        pushForce = 5.0f;
        weaponLevel = 0;
        cooldown = 0.5f;
        lastSwing = Time.time;
        manaCost = 5;
        swingAnimation = GetComponent<Animator>();
    }

    protected override void Update() {
        base.Update();
        // check if allowed to swing
        if (Time.time - lastSwing >= cooldown) {
            if (Input.GetKey(KeyCode.Space)) {
                lastSwing = Time.time;
                player.mana -= manaCost;
                swingAnimation.SetTrigger("Swing");
            }
        }

    }

    protected override void OnCollide(Collider2D hit) {
        if (hit.name != "Player" && hit.tag == "Fighter" && !player.isDead) {
            // send damage to the enemy
            Damage dmg = new Damage();
            dmg.damageAmount = damagePoint;
            dmg.origin = transform.position;
            dmg.push = pushForce;
            hit.SendMessage("ReceiveDamage", dmg);
        }
    }
}
