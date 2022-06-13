using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Player player;
    public GameObject healthTextPrefab;  // text display

    protected Text healthText;
    protected RectTransform container;
    protected RectTransform healthBar;
    protected bool hpLengthIsSet;
    protected int containerLength;
    protected int hitpointInt;
    protected int maxHitpointInt;


    protected virtual void Start() {
        container = GetComponent<RectTransform>();
        healthBar = transform.Find("HealthBar").GetComponent<RectTransform>();
        healthText = transform.Find("HealthText").GetComponent<Text>();
    }

    protected virtual void Update() {
        // set health bar's length based on player's current and max health
        if (!hpLengthIsSet) {
            container.offsetMax = new Vector2(-1750 + (int) player.maxHitpoint, container.offsetMax.y);
            containerLength = (int) container.rect.width;
            hpLengthIsSet = true;
        }

        int healthBarLengthCalc = (int) ((containerLength - 8) * ((float) player.hitpoint / (float) player.maxHitpoint));
        if (player.hitpoint <= 0)
            healthBar.gameObject.SetActive(false);
        else
            healthBar.offsetMax = new Vector2(healthBarLengthCalc - containerLength + 4, healthBar.offsetMax.y);

        hitpointInt = (int) player.hitpoint;
        maxHitpointInt = (int) player.maxHitpoint;
        healthText.text = hitpointInt.ToString() + "/" + maxHitpointInt.ToString();
    }
}
