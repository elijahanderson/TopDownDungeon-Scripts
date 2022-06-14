using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public Player player;

    protected Image xpBarImage;
    protected RectTransform container;
    protected RectTransform xpBarRect;
    protected bool xpLengthIsSet;
    protected int containerLength;
    protected int xpInt;
    protected int maxxpInt;
    protected int trackXP;


    protected virtual void Start() {
        container = GetComponent<RectTransform>();
        containerLength = (int) container.rect.width;
        xpBarRect = transform.Find("XPBar").GetComponent<RectTransform>();
        xpBarImage = transform.Find("XPBar").GetComponent<Image>();
        trackXP = GameManager.gameManagerInstance.experience;
        UpdateXPBarLength();
    }

    protected virtual void Update() {
        // set xp bar's length based on player's current and max xp
        if (trackXP < GameManager.gameManagerInstance.experience) {
            UpdateXPBarLength();
        }
    }

    protected virtual void UpdateXPBarLength() {
        int xpBarLengthCalc = (int) ((containerLength - 8) * Mathf.Abs(
                                            ((float) GameManager.gameManagerInstance.experience /
                                            (float) 100)));
        xpBarRect.offsetMax = new Vector2(xpBarLengthCalc - containerLength + 4, xpBarRect.offsetMax.y);
        trackXP = GameManager.gameManagerInstance.experience;
    }
}
