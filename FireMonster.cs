using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMonster : Enemy
{
    protected override void Start() {
        base.Start();
        xSpeed = 0.25f;
        ySpeed = 0.25f;
        hitpoint = 30;
        maxHitpoint = 30;
        healthRegenRate = 0.0f; // no regen
        mana = 0.0f;
        maxMana = 0.0f;
        manaRegenRate = 0.0f; // no regen
        triggerLength = 1.0f;
        chaseLength = 20.0f;
    }
}
