using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenSword : Weapon
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        minDamage = 5;
        maxDamage = 15;
        pushForce = 5.0f;
        weaponLevel = 0;
        cooldown = 0.5f;
        lastSwing = Time.time;
        staminaCost = 4;
        dmgType = "physical";
        // display label button
        if (!collected) {
            label = Instantiate(labelButtonPrefab, transform);
            label.GetComponentInChildren<TextMesh>().text = "Wooden Sword";
        }
    }
}
