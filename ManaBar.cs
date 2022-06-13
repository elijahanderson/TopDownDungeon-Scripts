using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Player player;

    protected Text manaText;
    protected Image manaBarImage;
    protected RectTransform container;
    protected RectTransform manaBarRect;
    protected bool manaLengthIsSet;
    protected int containerLength;
    protected int manaInt;
    protected int maxManaInt;


    protected virtual void Start() {
        container = GetComponent<RectTransform>();
        manaBarRect = transform.Find("ManaBar").GetComponent<RectTransform>();
        manaBarImage = transform.Find("ManaBar").GetComponent<Image>();
        manaText = transform.Find("ManaText").GetComponent<Text>();
        maxManaInt = (int) player.maxMana;
    }

    protected virtual void Update() {
        // set container length only once
        if (!manaLengthIsSet) {
            container.offsetMax = new Vector2(-1750 + (int) player.maxMana, container.offsetMax.y);
            containerLength = (int) container.rect.width;
            manaLengthIsSet = true;
        }

        // set mana bar's length based on player's current and max mana
        int manaBarLengthCalc = (int) ((containerLength - 8) * ((float) player.mana / (float) player.maxMana));
        // if mana is negative, change the color & alpha
        if (manaBarLengthCalc < 0) {
            Color tempColor = manaBarImage.color;
            tempColor.a = 0.5f;
            manaBarImage.color = tempColor;
        }
        manaBarRect.offsetMax = new Vector2(manaBarLengthCalc - containerLength + 4, manaBarRect.offsetMax.y);

        manaInt = (int) player.mana;
        manaText.text = manaInt.ToString() + "/" + maxManaInt.ToString();
    }
}
