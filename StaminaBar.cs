using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Player player;

    protected Text staminaText;
    protected Image staminaBarImage;
    protected RectTransform container;
    protected RectTransform staminaBarRect;
    protected bool staminaLengthIsSet;
    protected int containerLength;
    protected int staminaInt;
    protected int maxStaminaInt;


    protected virtual void Start() {
        container = GetComponent<RectTransform>();
        staminaBarRect = transform.Find("StaminaBar").GetComponent<RectTransform>();
        staminaBarImage = transform.Find("StaminaBar").GetComponent<Image>();
        staminaText = transform.Find("StaminaText").GetComponent<Text>();
    }

    protected virtual void Update() {
        // set container length only once
        if (!staminaLengthIsSet) {
            container.offsetMax = new Vector2(-1750 + (int) player.maxStamina, container.offsetMax.y);
            containerLength = (int) container.rect.width;
            staminaLengthIsSet = true;
        }

        // set stamina bar's length based on player's current and max stamina
        int staminaBarLengthCalc = (int) ((containerLength - 8) * Mathf.Abs(((float) player.stamina / (float) player.maxStamina)));
        // if stamina is negative, change the color & alpha
        if (player.stamina < 0) {
            Color tempColor = staminaBarImage.color;
            tempColor.a = 0.5f;
            staminaBarImage.color = tempColor;
        } else {
            Color tempColor = staminaBarImage.color;
            tempColor.a = 1.0f;
            staminaBarImage.color = tempColor;
        }
        staminaBarRect.offsetMax = new Vector2(staminaBarLengthCalc - containerLength + 4, staminaBarRect.offsetMax.y);

        maxStaminaInt = (int) player.maxStamina;
        staminaInt = (int) player.stamina;
        staminaText.text = staminaInt.ToString() + "/" + maxStaminaInt.ToString();
    }
}
