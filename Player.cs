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
    public int equipLoad;

    protected Vector2 target;
    protected RaycastHit2D hitX;
    protected RaycastHit2D hitY;

    // dash related variables
    protected float dashCooldown;
    protected float dashStaminaCost;
    protected float dashSpeed;
    protected float lastDash;
    protected bool isDashing;

    protected override void Start() {
        base.Start();
        constitution = GameManager.gameManagerInstance.constitution;
        strength = GameManager.gameManagerInstance.strength;
        intelligence = GameManager.gameManagerInstance.intelligence;
        spirituality = GameManager.gameManagerInstance.spirituality;
        endurance = GameManager.gameManagerInstance.endurance;
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
        moveSpeed = 0.75f + (endurance * 0.02f);
        GameManager.gameManagerInstance.playerMoveSpeed = moveSpeed;
        equipLoad = 20 + endurance;
        GameManager.gameManagerInstance.equipLoad = equipLoad;

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
        if (Input.GetKey(KeyCode.Space)
                                        && Time.time - lastDash >= dashCooldown
                                        && stamina > 0
                                        && Vector2.Distance(target, transform.position) > 0.01f)
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

    /*
        A bunch of setters for managing player's attributes and stats
    */
    // attributes
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
        equipLoad = endurance + 20 + endurance;
        GameManager.gameManagerInstance.equipLoad = equipLoad;
        staminaBar.UpdateContainerLength();
    }

    // stats
    private void SetMaxHitpoint(int nMaxHitpoint) {
        GameManager.gameManagerInstance.playerMaxHitpoint = nMaxHitpoint;
        maxHitpoint = nMaxHitpoint;
        healthBar.UpdateContainerLength();
    }
    private void SetHealthRegenRate(int nHealthRegenRate) {
        GameManager.gameManagerInstance.playerHealthRegenRate = nHealthRegenRate;
        healthRegenRate = nHealthRegenRate;
    }
    private void SetMaxMana(int nMaxMana) {
        GameManager.gameManagerInstance.playerMaxMana = nMaxMana;
        maxMana = nMaxMana;
        manaBar.UpdateContainerLength();
    }
    private void SetManaRegenRate(int nManaRegenRate) {
        GameManager.gameManagerInstance.playerManaRegenRate = nManaRegenRate;
        manaRegenRate = nManaRegenRate;
    }
    private void SetMaxStamina(int nMaxStamina) {
        GameManager.gameManagerInstance.playerMaxStamina = nMaxStamina;
        maxStamina = nMaxStamina;
        staminaBar.UpdateContainerLength();
    }
    private void SetStaminaRegenRate(int nStaminaRegenRate) {
        GameManager.gameManagerInstance.playerStaminaRegenRate = nStaminaRegenRate;
        staminaRegenRate = nStaminaRegenRate;
    }
    private void SetEquipLoad(int nEquipLoad) {
        GameManager.gameManagerInstance.equipLoad = nEquipLoad;
        equipLoad = nEquipLoad;
    }
    private void SetMoveSpeed(int nMoveSpeed) {
        GameManager.gameManagerInstance.playerMoveSpeed = nMoveSpeed;
        moveSpeed = nMoveSpeed;
    }

}
