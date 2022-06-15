using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    public GameObject dmgFloatingText;

    // stats
    public int level;
    public float hitpoint;
    public float maxHitpoint;
    public float healthRegenRate;
    public float mana;
    public float maxMana;
    public float manaRegenRate;
    public float stamina;
    public float maxStamina;
    public float staminaRegenRate;
    public bool isDead;
    // resistances
    public float flatDamageReduction;
    public float physicalResistance;
    public float aetherResistance;
    public float timeResistance;
    // dash related variables
    public float dashCooldown;
    public float dashStaminaCost;
    public float dashSpeed;

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
    }

    protected virtual void Update() {
        // apply health/mana regen once a second if needed
        float hitpointCalc = hitpoint + (healthRegenRate * Time.deltaTime);
        if (maxHitpoint < hitpointCalc)
            hitpoint += maxHitpoint - hitpoint;
        else if (maxHitpoint >= hitpointCalc)
            hitpoint = hitpointCalc;

        float manaCalc = mana + (manaRegenRate * Time.deltaTime);
        if (maxMana < manaCalc)
            mana += maxMana - mana;
        else if (maxMana >= manaCalc)
            mana = manaCalc;

        float staminaCalc = stamina + (staminaRegenRate * Time.deltaTime);
        if (maxStamina < staminaCalc)
            stamina += maxStamina - stamina;
        else if (maxStamina >= staminaCalc)
            stamina = staminaCalc;

        nextUpdate += updateInterval;
    }

    // damage control
    protected virtual void ReceiveDamage(Damage dmg) {
        // if not immune, receive damage
        if ((Time.time - lastImmune > immuneTime) && !isDead) {
            lastImmune = Time.time;
            // damage reduction
            if (flatDamageReduction != 0)
                dmg.damageAmount *= flatDamageReduction;
            if (dmg.damageType == "physical" && physicalResistance != 0)
                dmg.damageAmount *= physicalResistance;
            else if (dmg.damageType == "aether" && aetherResistance != 0)
                dmg.damageAmount *= aetherResistance;
            else if (dmg.damageType == "time" && timeResistance != 0)
                dmg.damageAmount *= timeResistance;

            hitpoint -= dmg.damageAmount;

            // show damage floating text
            showDmgFloatingText(dmgFloatingText, (int) dmg.damageAmount);

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
