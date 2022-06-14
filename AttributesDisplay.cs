using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributesDisplay : MonoBehaviour
{
    public Player player;

    protected Text constitutionText;
    protected Text strengthText;
    protected Text intelligenceText;
    protected Text spiritualityText;
    protected Text enduranceText;
    protected int trackConstitution;
    protected int trackStrength;
    protected int trackIntelligence;
    protected int trackSpirituality;
    protected int trackEndurance;

    protected virtual void Start() {
        constitutionText = transform.Find("ConstitutionText").GetComponent<Text>();
        strengthText = transform.Find("StrengthText").GetComponent<Text>();
        intelligenceText = transform.Find("IntelligenceText").GetComponent<Text>();
        spiritualityText = transform.Find("SpiritualityText").GetComponent<Text>();
        enduranceText = transform.Find("EnduranceText").GetComponent<Text>();

        constitutionText.text = "Constitution: " + player.constitution.ToString();
        strengthText.text = "Strength: " + player.strength.ToString();
        intelligenceText.text = "Intelligence: " + player.intelligence.ToString();
        spiritualityText.text = "Spirituality: " + player.spirituality.ToString();
        enduranceText.text = "Endurance: " + player.endurance.ToString();

        trackConstitution = player.constitution;
        trackStrength = player.strength;
        trackIntelligence = player.intelligence;
        trackSpirituality = player.spirituality;
        trackEndurance = player.endurance;
    }

    protected virtual void Update() {
        if (trackConstitution < player.constitution) {
            constitutionText.text = "Constitution: " + player.constitution.ToString();
            trackConstitution = player.constitution;
        }
        if (trackStrength < player.strength) {
            strengthText.text = "Strength: " + player.strength.ToString();
            trackStrength = player.strength;
        }
        if (trackIntelligence < player.intelligence) {
            intelligenceText.text = "Intelligence: " + player.intelligence.ToString();
            trackIntelligence = player.intelligence;
        }
        if (trackSpirituality < player.spirituality) {
            spiritualityText.text = "Spirituality: " + player.spirituality.ToString();
            trackSpirituality = player.spirituality;
        }
        if (trackEndurance < player.endurance) {
            enduranceText.text = "Endurance: " + player.endurance.ToString();
            trackEndurance = player.endurance;
        }
    }

}
