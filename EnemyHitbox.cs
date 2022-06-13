using Random = System.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Interactable
{
    public int minDamage;
    public int maxDamage;
    public float pushForce;
    public Enemy numberOne;

    protected override void Start() {
        base.Start();
        minDamage = 5;
        maxDamage = 15;
        pushForce = 3.0f;
    }

    protected override void Update() {
        if (numberOne.isDead)
            boxCollider.enabled = false;
        base.Update();
    }

    protected override void OnCollide(Collider2D hit) {
        if (hit.name == "Player" && !numberOne.isDead) {
            // create and send damage
            Random rand = new Random();
            Damage dmg = new Damage();
            dmg.damageAmount = rand.Next(minDamage, maxDamage);
            dmg.origin = transform.position;
            dmg.push = pushForce;
            hit.SendMessage("ReceiveDamage", dmg);
        }
    }
}
