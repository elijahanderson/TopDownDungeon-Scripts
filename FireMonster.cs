using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMonster : Enemy
{
    protected override void Start() {
        base.Start();
        moveSpeed = 0.25f;
        hitpoint = 30;
        maxHitpoint = 30;
        healthRegenRate = 0.0f; // no regen
        mana = 0.0f;
        maxMana = 0.0f;
        manaRegenRate = 0.0f;
        stamina = 10.0f;
        maxStamina = 10.0f;
        staminaRegenRate = 2.0f;
        triggerLength = 1.0f;
        chaseLength = 20.0f;
    }
}
