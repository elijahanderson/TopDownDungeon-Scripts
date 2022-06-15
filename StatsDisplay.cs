using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    public Player player;

    protected Text hpRegenText;
    protected Text manaRegenText;
    protected Text staminaRegenText;
    protected Text moveSpeedText;
    protected Text dashCooldownText;
    protected Text dashCostText;
    protected Text dashSpeedText;
    protected Text levelText;
    protected Text goldText;
    protected float trackHPRegen;
    protected float trackManaRegen;
    protected float trackStaminaRegen;
    protected float trackMoveSpeed;
    protected float trackDashCooldown;
    protected float trackDashCost;
    protected float trackDashSpeed;
    protected int trackLevel;
    protected int trackGold;

    protected virtual void Start() {
        levelText = transform.Find("LevelText").GetComponent<Text>();
        goldText = transform.Find("GoldText").GetComponent<Text>();
        hpRegenText = transform.Find("HPRegenText").GetComponent<Text>();
        manaRegenText = transform.Find("ManaRegenText").GetComponent<Text>();
        staminaRegenText = transform.Find("StaminaRegenText").GetComponent<Text>();
        moveSpeedText = transform.Find("MoveSpeedText").GetComponent<Text>();
        dashCooldownText = transform.Find("DashCooldownText").GetComponent<Text>();
        dashCostText = transform.Find("DashCostText").GetComponent<Text>();
        dashSpeedText = transform.Find("DashSpeedText").GetComponent<Text>();

        levelText.text = "Level " + player.level.ToString();
        goldText.text = "Gold: " + player.totalGold.ToString();
        hpRegenText.text = "HP Regen Rate: " + player.healthRegenRate.ToString();
        manaRegenText.text = "Mana Regen Rate: " + player.manaRegenRate.ToString();
        staminaRegenText.text = "Stamina Regen Rate: " + player.staminaRegenRate.ToString();
        moveSpeedText.text = "Move Speed: " + player.moveSpeed.ToString();
        dashCooldownText.text = "Dash Cooldown Speed: " + player.dashCooldown.ToString();
        dashCostText.text = "Dash Stamina Cost: " + player.dashStaminaCost.ToString();
        dashSpeedText.text = "Dash Speed Multiplier: " + player.dashSpeed.ToString();

        trackLevel = player.level;
        trackGold = player.totalGold;
        trackHPRegen = player.healthRegenRate;
        trackManaRegen = player.manaRegenRate;
        trackStaminaRegen = player.staminaRegenRate;
        trackMoveSpeed = player.moveSpeed;
        trackDashCooldown = player.dashCooldown;
        trackDashCost = player.dashStaminaCost;
        trackDashSpeed = player.dashSpeed;
    }

    protected virtual void Update() {
        if (trackLevel < player.level) {
            levelText.text = "Level " + player.level.ToString();
            trackLevel = player.level;
        }
        if (trackGold < player.totalGold) {
            goldText.text = "Gold: " + player.totalGold.ToString();
            trackGold = player.totalGold;
        }
        if (trackHPRegen < player.healthRegenRate) {
            hpRegenText.text = "HP Regen Rate: " + player.healthRegenRate.ToString();
            trackHPRegen = player.healthRegenRate;
        }
        if (trackManaRegen < player.manaRegenRate) {
            manaRegenText.text = "Mana Regen Rate: " + player.manaRegenRate.ToString();
            trackManaRegen = player.manaRegenRate;
        }
        if (trackStaminaRegen < player.staminaRegenRate) {
            staminaRegenText.text = "Stamina Regen Rate: " + player.staminaRegenRate.ToString();
            trackStaminaRegen = player.staminaRegenRate;
        }
        if (trackMoveSpeed < player.moveSpeed) {
            moveSpeedText.text = "Move Speed: " + player.moveSpeed.ToString();
            trackMoveSpeed = player.moveSpeed;
        }
        if (trackDashCooldown < player.dashCooldown) {
            dashCooldownText.text = "Dash Cooldown Speed: " + player.dashCooldown.ToString();
            trackDashCooldown = player.dashCooldown;
        }
        if (trackDashCost < player.dashStaminaCost) {
            dashCostText.text = "Dash Stamina Cost: " + player.dashStaminaCost.ToString();
            trackDashCost = player.dashStaminaCost;
        }
        if (trackDashSpeed < player.dashSpeed) {
            dashSpeedText.text = "Dash Speed Multiplier: " + player.dashSpeed.ToString();
            trackDashSpeed = player.dashSpeed;
        }
    }

}
