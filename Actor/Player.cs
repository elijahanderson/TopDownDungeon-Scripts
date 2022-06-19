using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    // for adjusting UI
    public HealthBar healthBar;
    public ManaBar manaBar;
    public StaminaBar staminaBar;

    // attributes
    public int constitution;
    public int strength;
    public int intelligence;
    public int spirituality;
    public int endurance;
    public int totalGold;

    protected Vector2 target;
    protected RaycastHit2D hitX;
    protected RaycastHit2D hitY;
    protected bool weaponEquipped;

    protected override void Start() {
        base.Start();
        // attributes
        constitution = GameManager.gameManagerInstance.constitution;
        strength = GameManager.gameManagerInstance.strength;
        intelligence = GameManager.gameManagerInstance.intelligence;
        spirituality = GameManager.gameManagerInstance.spirituality;
        endurance = GameManager.gameManagerInstance.endurance;

        // stats
        level = GameManager.gameManagerInstance.playerLevel;
        totalGold = GameManager.gameManagerInstance.totalGold;

        hitpoint = 100 + (constitution * 20);
        maxHitpoint = 100 + (constitution * 20);
        GameManager.gameManagerInstance.playerMaxHitpoint = maxHitpoint;

        healthRegenRate = 2.0f + (constitution * 0.10f);
        GameManager.gameManagerInstance.playerHealthRegenRate = healthRegenRate;

        mana = 10 + (intelligence * 10);
        maxMana = 10 + (intelligence * 10);
        GameManager.gameManagerInstance.playerMaxMana = maxMana;

        manaRegenRate = 0.5f + (intelligence * 0.05f);
        GameManager.gameManagerInstance.playerManaRegenRate = manaRegenRate;

        stamina = 10 + (endurance * 10);
        maxStamina = 10 + (endurance * 10);
        GameManager.gameManagerInstance.playerMaxStamina = maxStamina;

        staminaRegenRate = 2.0f + (endurance * 0.05f);
        GameManager.gameManagerInstance.playerStaminaRegenRate = staminaRegenRate;

        // movement
        moveSpeed = 0.75f + (endurance * 0.02f);
        GameManager.gameManagerInstance.playerMoveSpeed = moveSpeed;

        dashCooldown = 1.0f - (0.01f * endurance);
        GameManager.gameManagerInstance.playerDashCooldown = dashCooldown;

        dashStaminaCost = 5.0f - (0.1f * endurance);
        GameManager.gameManagerInstance.playerDashStaminaCost = dashStaminaCost;

        dashSpeed = 2.0f + (0.1f * endurance);
        GameManager.gameManagerInstance.playerDashSpeed = dashSpeed;

        // resistances
        flatDamageReduction = GameManager.gameManagerInstance.playerFlatDamageReduction;
        physicalResistance = GameManager.gameManagerInstance.playerPhysicalResistance;
        aetherResistance = GameManager.gameManagerInstance.playerAetherResistance;
        timeResistance = GameManager.gameManagerInstance.playerTimeResistance;

        lastDash = Time.time;
        isDashing = false;
        target = transform.position;
    }

    protected override void Update() {
        base.Update();
        // if mouse is clicked down, get its position and set it as the movement target
        if ((Input.GetMouseButton(0) || Input.GetMouseButton(4)) && !isDashing && Time.timeScale != 0)
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // dash mechanic
        if (Input.GetKey(KeyCode.Space)
                                        && Time.time - lastDash >= dashCooldown
                                        && stamina > 0
                                        && Vector2.Distance(target, transform.position) > 0.01f)
            StartCoroutine("Dash");
        // update player gold if needed
        if (totalGold < GameManager.gameManagerInstance.totalGold)
            totalGold = GameManager.gameManagerInstance.totalGold;
    }

    protected virtual void FixedUpdate() {
        // swap sprite direction
        if (target.x > transform.position.x) {
            transform.Find("Sprite").localScale = new Vector2(-1, 1);
            try {
                transform.Find("Weapon").GetChild(0).Find("WeaponLeft").gameObject.SetActive(false);
                transform.Find("Weapon").GetChild(0).Find("WeaponRight").gameObject.SetActive(true);
            } catch (UnityException e) {}
        }
        else if (target.x < transform.position.x) {
            transform.Find("Sprite").localScale = new Vector2(1, 1);
            try {
                transform.Find("Weapon").GetChild(0).Find("WeaponRight").gameObject.SetActive(false);
                transform.Find("Weapon").GetChild(0).Find("WeaponLeft").gameObject.SetActive(true);
            } catch (UnityException e) {}
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

    // for collecting items
    public void CollectWeapon(Weapon weapon)
    {
        // if player has no weapon, auto equip
        if (!weaponEquipped)
            EquipWeapon(weapon);
    }
    private void EquipWeapon(Weapon weapon)
    {
        weaponEquipped = true;
        weapon.transform.parent.parent = transform.Find("Weapon").transform;
        weapon.isEquipped = true;
        weapon.transform.parent.position = transform.position;
        weapon.swingAnimation.enabled = true;
    }


    // attributes setters
    private void SetConstitution(int nConstitution) {
        GameManager.gameManagerInstance.constitution = nConstitution;
        constitution = nConstitution;
        maxHitpoint = 100 + (constitution * 20);
        GameManager.gameManagerInstance.playerMaxHitpoint = maxHitpoint;
        hitpoint = maxHitpoint + 100 + (constitution * 20);
        healthRegenRate = 2.0f + (constitution * 0.10f);
        GameManager.gameManagerInstance.playerHealthRegenRate = healthRegenRate;
        healthBar.UpdateContainerLength();
    }
    private void SetStrength(int nStrength) {
        GameManager.gameManagerInstance.strength = nStrength;
        strength = nStrength;
    }
    private void SetIntelligence(int nIntelligence) {
        GameManager.gameManagerInstance.intelligence = nIntelligence;
        intelligence = nIntelligence;
        mana = intelligence + 10 + (intelligence * 10);
        maxMana = intelligence + 10 + (intelligence * 10);
        GameManager.gameManagerInstance.playerMaxMana = maxMana;
        manaRegenRate = intelligence + 0.5f + (intelligence * 0.05f);
        GameManager.gameManagerInstance.playerManaRegenRate = manaRegenRate;
        manaBar.UpdateContainerLength();
    }
    private void SetSpirituality(int nSpirituality) {
        GameManager.gameManagerInstance.spirituality = nSpirituality;
        spirituality = nSpirituality;
    }
    private void SetEndurance(int nEndurance) {
        GameManager.gameManagerInstance.endurance = nEndurance;
        endurance = nEndurance;
        stamina = endurance + 10 + (endurance * 10);
        maxStamina = endurance + 10 + (endurance * 10);
        GameManager.gameManagerInstance.playerMaxStamina = maxStamina;
        staminaRegenRate = endurance + 2.0f + (endurance * 0.05f);
        GameManager.gameManagerInstance.playerStaminaRegenRate = staminaRegenRate;
        moveSpeed = endurance + 0.75f + (endurance * 0.02f);
        GameManager.gameManagerInstance.playerMoveSpeed = moveSpeed;
        dashCooldown = 1.0f - (0.01f * endurance);
        GameManager.gameManagerInstance.playerDashCooldown = dashCooldown;
        dashStaminaCost = 5.0f - (0.1f * endurance);
        GameManager.gameManagerInstance.playerDashStaminaCost = dashStaminaCost;
        dashSpeed = 2.0f + (0.1f * endurance);
        GameManager.gameManagerInstance.playerDashSpeed = dashSpeed;
        staminaBar.UpdateContainerLength();
    }

    // stats setters
    private void SetLevel(int nLevel) {
        GameManager.gameManagerInstance.playerLevel = nLevel;
        level = nLevel;
    }
    private void SetGold(int nGold) {
        GameManager.gameManagerInstance.totalGold = nGold;
        totalGold = nGold;
    }
    private void SetMaxHitpoint(float nMaxHitpoint) {
        GameManager.gameManagerInstance.playerMaxHitpoint = nMaxHitpoint;
        maxHitpoint = nMaxHitpoint;
        healthBar.UpdateContainerLength();
    }
    private void SetHealthRegenRate(float nHealthRegenRate) {
        GameManager.gameManagerInstance.playerHealthRegenRate = nHealthRegenRate;
        healthRegenRate = nHealthRegenRate;
    }
    private void SetMaxMana(float nMaxMana) {
        GameManager.gameManagerInstance.playerMaxMana = nMaxMana;
        maxMana = nMaxMana;
        manaBar.UpdateContainerLength();
    }
    private void SetManaRegenRate(float nManaRegenRate) {
        GameManager.gameManagerInstance.playerManaRegenRate = nManaRegenRate;
        manaRegenRate = nManaRegenRate;
    }
    private void SetMaxStamina(float nMaxStamina) {
        GameManager.gameManagerInstance.playerMaxStamina = nMaxStamina;
        maxStamina = nMaxStamina;
        staminaBar.UpdateContainerLength();
    }
    private void SetStaminaRegenRate(float nStaminaRegenRate) {
        GameManager.gameManagerInstance.playerStaminaRegenRate = nStaminaRegenRate;
        staminaRegenRate = nStaminaRegenRate;
    }
    private void SetMoveSpeed(float nMoveSpeed) {
        GameManager.gameManagerInstance.playerMoveSpeed = nMoveSpeed;
        moveSpeed = nMoveSpeed;
    }
    private void SetFlatDamageReduction(float nFlatDamageReduction) {
        GameManager.gameManagerInstance.playerFlatDamageReduction = nFlatDamageReduction;
        flatDamageReduction = nFlatDamageReduction;
    }
    private void SetPhysicalResistance(float nPhysicalResistance) {
        GameManager.gameManagerInstance.playerPhysicalResistance = nPhysicalResistance;
        physicalResistance = nPhysicalResistance;
    }
    private void SetAetherResistance(float nAetherResistance) {
        GameManager.gameManagerInstance.playerAetherResistance = nAetherResistance;
        aetherResistance = nAetherResistance;
    }
    private void SetTimeResistance(float nTimeResistance) {
        GameManager.gameManagerInstance.playerTimeResistance = nTimeResistance;
        timeResistance = nTimeResistance;
    }
}
