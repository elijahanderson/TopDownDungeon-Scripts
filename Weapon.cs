using Random = System.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Interactable
{
    // damage
    public Player player;
    public int minDamage;
    public int maxDamage;
    public float pushForce;
    public int weaponLevel;
    public int staminaCost;

    private SpriteRenderer spriteRenderer;
    private Animator swingAnimation;

    // attacking
    private float cooldown;
    private float lastSwing;

    protected override void Start() {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        minDamage = 5;
        maxDamage = 15;
        pushForce = 5.0f;
        weaponLevel = 0;
        cooldown = 0.5f;
        lastSwing = Time.time;
        staminaCost = 4;
        swingAnimation = GetComponent<Animator>();
    }

    protected override void Update() {
        base.Update();
        // swing if allowed to swing
        if (Input.GetKey(KeyCode.F) && player.stamina > 0 && Time.time - lastSwing >= cooldown) {
            lastSwing = Time.time;
            player.stamina -= staminaCost;
            swingAnimation.SetTrigger("Swing");
        }
    }

    protected override void OnCollide(Collider2D hit) {
        if (hit.name != "Player" && hit.tag == "Fighter" && !player.isDead) {
            // send damage to the enemy
            Random rand = new Random();
            Damage dmg = new Damage();
            dmg.damageAmount = rand.Next(minDamage, maxDamage);
            dmg.origin = transform.position;
            dmg.push = pushForce;
            hit.SendMessage("ReceiveDamage", dmg);
        }
    }
}
