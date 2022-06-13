using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    public GameObject dmgFloatingText;
    public float hitpoint;
    public float maxHitpoint;
    public float healthRegenRate;
    public float mana;
    public float maxMana;
    public float manaRegenRate;
    public bool isDead;

    // immunity
    protected float immuneTime;
    protected float lastImmune;

    // push
    public float pushRecoverySpeed;
    protected Vector2 pushDirection;

    private SpriteRenderer sprite;
    private Animator deathAnimation;
    private int updateInterval;
    private float nextUpdate;

    protected virtual void Start() {
        immuneTime = 1.0f;
        lastImmune = Time.time;
        pushRecoverySpeed = 0.2f;
        updateInterval = 1;
        nextUpdate = 0.0f;
        sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        deathAnimation = transform.Find("Sprite").GetComponent<Animator>();
        // periodic updating
        InvokeRepeating("UpdateEverySecond", 0, 1.0f);
    }

    protected virtual void UpdateEverySecond() {
        // apply health/mana regen once a second if needed
        float hitpointCalc = hitpoint + healthRegenRate;
        if (maxHitpoint < hitpointCalc)
            hitpoint += maxHitpoint - hitpoint;
        else if (maxHitpoint >= hitpointCalc)
            hitpoint = hitpointCalc;

        float manaCalc = mana + manaRegenRate;
        if (maxMana < manaCalc)
            mana += maxMana - mana;
        else if (maxMana >= manaCalc)
            mana = manaCalc;

        nextUpdate += updateInterval;
    }

    // damage control
    protected virtual void ReceiveDamage(Damage dmg) {
        // if not immune, receive damage
        if ((Time.time - lastImmune > immuneTime) && !isDead) {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;

            // show damage floating text
            showDmgFloatingText(dmgFloatingText, dmg.damageAmount);

            // calculate push direction based on the damage origin and the fighter's current position
            pushDirection = (new Vector2(transform.position.x, transform.position.y)
                            - new Vector2(dmg.origin.x, dmg.origin.y)).normalized
                            * dmg.push;

            if (hitpoint <= 0)
                Die();
        }
    }

    protected virtual void Die() {
        isDead = true;
        showDeathAnimation();
        Destroy(gameObject, 2);
    }

    protected virtual void showDmgFloatingText(GameObject dmgFloatingText, int dmgAmount) {
        if (dmgFloatingText) {
            GameObject txt = Instantiate(dmgFloatingText, transform);
            txt.GetComponentInChildren<TextMesh>().text = dmgAmount.ToString();
            Destroy(txt, 2);
        }
    }

    protected virtual void showDeathAnimation() {
        if (deathAnimation)
            deathAnimation.enabled = true;
    }
}
