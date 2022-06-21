using Random = System.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collectable
{
    // damage
    public Player player;
    public Animator swingAnimation;
    public GameObject labelButtonPrefab;
    public int minDamage;
    public int maxDamage;
    public float pushForce;
    public int weaponLevel;
    public int staminaCost;
    public string dmgType;
    public bool isEquipped;

    protected GameObject label;
    // attacking
    protected float cooldown;
    protected float lastSwing;

    protected override void Start() {
        base.Start();
        swingAnimation = GetComponent<Animator>();
        swingAnimation.enabled = false;
        isEquipped = false;
    }

    protected override void Update() {
        base.Update();
        // swing if allowed to swing
        if (Input.GetKey(KeyCode.F) && player.stamina > 0 && Time.time - lastSwing >= cooldown && isEquipped) {
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
        }
    }

    protected virtual void OnMouseDown()
    {
        // collect and add to player inventory
        if (!collected) {
            StartCoroutine("WaitForPlayer");
        }
    }

    protected virtual IEnumerator WaitForPlayer()
    {
        // wait one frame (for player.isMoving to be set to true)
        int f = 1;
        while (f > 0) {
            f--;
            yield return null;
        }
        yield return new WaitUntil(() => player.isMoving == false);
        OnCollect();
    }

    protected override void OnCollect()
    {
        collected = true;
        Destroy(label);
        player.CollectWeapon(this);
    }
}
