using Random = System.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collectable
{
    // damage
    public Player player;
    public Animator swingAnimation;
    public int minDamage;
    public int maxDamage;
    public float pushForce;
    public int weaponLevel;
    public int staminaCost;
    public string dmgType;
    public bool isEquipped;

    private SpriteRenderer spriteRenderer;

    // attacking
    protected float cooldown;
    protected float lastSwing;

    protected override void Start() {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        swingAnimation = GetComponent<Animator>();
        swingAnimation.enabled = false;
        isEquipped = false;
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
        if (hit.name != "Player" && hit.tag == "Fighter" && !player.isDead && isEquipped) {
            // send damage to the enemy
            Random rand = new Random();
            Damage dmg = new Damage();
            dmg.damageAmount = rand.Next(minDamage, maxDamage);
            dmg.origin = transform.position;
            dmg.push = pushForce;
            dmg.damageType = dmgType;
            hit.SendMessage("ReceiveDamage", dmg);
        } else if (hit.name == "Player") {
            // weapon not yet equpped by player -- collect it
            OnCollect();
        }
    }

    protected override void OnCollect()
    {
        // collect and add to player inventory
        if (!collected) {
            collected = true;
            player.CollectWeapon(this);
        }
    }
}
