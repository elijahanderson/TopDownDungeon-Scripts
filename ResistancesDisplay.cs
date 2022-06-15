using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResistancesDisplay : MonoBehaviour
{
    public Player player;

    protected Text flatDamageReductionText;
    protected Text physicalResistanceText;
    protected Text aetherResistanceText;
    protected Text timeResistanceText;
    protected float trackFlagDamageReducation;
    protected float trackPhysicalResistance;
    protected float trackAetherResistance;
    protected float trackTimeResistance;

    protected virtual void Start() {
        flatDamageReductionText = transform.Find("FlatDamageReductionText").GetComponent<Text>();
        physicalResistanceText = transform.Find("PhysicalResistanceText").GetComponent<Text>();
        aetherResistanceText = transform.Find("AetherResistanceText").GetComponent<Text>();
        timeResistanceText = transform.Find("TimeResistanceText").GetComponent<Text>();

        flatDamageReductionText.text = "Flat Damage\nReduction: " + (100 * player.flatDamageReduction).ToString() + "%";
        physicalResistanceText.text = "Physical\nResistance: " + (100 * player.physicalResistance).ToString() + "%";
        aetherResistanceText.text = "Aether\nResistance: " + (100 * player.aetherResistance).ToString() + "%";
        timeResistanceText.text = "Time\nResistance: " + (100 * player.timeResistance).ToString() + "%";

        trackFlagDamageReducation = player.flatDamageReduction;
        trackPhysicalResistance = player.physicalResistance;
        trackAetherResistance = player.aetherResistance;
        trackTimeResistance = player.timeResistance;
    }

    protected virtual void Update() {
        if (trackFlagDamageReducation < player.flatDamageReduction) {
            flatDamageReductionText.text = "Flat Damage\nReduction: " + (100 * player.flatDamageReduction).ToString() + "%";
            trackFlagDamageReducation = player.flatDamageReduction;
        }
        if (trackPhysicalResistance < player.physicalResistance) {
            physicalResistanceText.text = "Physical\nResistance: " + (100 * player.physicalResistance).ToString() + "%";
            trackPhysicalResistance = player.physicalResistance;
        }
        if (trackAetherResistance < player.aetherResistance) {
            aetherResistanceText.text = "Aether\nResistance: " + (100 * player.aetherResistance).ToString() + "%";
            trackAetherResistance = player.aetherResistance;
        }
        if (trackTimeResistance < player.timeResistance) {
            timeResistanceText.text = "Time\nResistance: " + (100 * player.timeResistance).ToString() + "%";
            trackTimeResistance = player.timeResistance;
        }
    }

}
