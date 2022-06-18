using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;

    // Resources
    public List<Sprite> characterSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    public Player player;
    // player attributes
    public int constitution;  // each playerLevel adds 20 HP and 10% health regen
    public int strength;  // each playerLevel adds 1% damage reduction and 1% block chance
    public int intelligence;  // each playerLevel adds 10 mana and 5% mana regen
    public int spirituality;  // each playerLevel adds 10 mana, 5% mana regen, 10 HP, 5% hp regen to player's minions
    public int endurance;  // each playerLevel adds 10 stamina, 5% stamina regen, 1 max equip load, 2% movespeed
    // player stats
    public float playerMaxHitpoint;
    public float playerHealthRegenRate;
    public float playerMaxMana;
    public float playerManaRegenRate;
    public float playerMaxStamina;
    public float playerStaminaRegenRate;

    public float playerMoveSpeed;
    public float playerDashCooldown;
    public float playerDashStaminaCost;
    public float playerDashSpeed;

    public float playerFlatDamageReduction;
    public float playerPhysicalResistance;
    public float playerAetherResistance;
    public float playerTimeResistance;

    public int totalGold;
    public int experience;
    public int playerLevel;

    private void Awake() {
        // delete previous game manager instances if necessary
        if (GameManager.gameManagerInstance != null) {
            Destroy(gameObject);
            return;
        }
        gameManagerInstance = this;
        // get the load state
        SceneManager.sceneLoaded += LoadState;
        // to ensure instance persistence on scene switch
        DontDestroyOnLoad(gameObject);
    }

    // Save state logic
    public void SaveState() {
        string s = "";
        // attributes
        s += constitution.ToString() + "|";
        s += strength.ToString() + "|";
        s += intelligence.ToString() + "|";
        s += spirituality.ToString() + "|";
        s += endurance.ToString() + "|";
        // equipment and stats
        s += playerMaxHitpoint.ToString() + "|";
        s += playerHealthRegenRate.ToString() + "|";
        s += playerMaxMana.ToString() + "|";
        s += playerManaRegenRate.ToString() + "|";
        s += playerMaxStamina.ToString() + "|";
        s += playerStaminaRegenRate.ToString() + "|";

        s += playerMoveSpeed.ToString() + "|";
        s += playerDashCooldown.ToString() + "|";
        s += playerDashStaminaCost.ToString() + "|";
        s += playerDashSpeed.ToString() + "|";

        s += playerFlatDamageReduction.ToString() + "|";
        s += playerPhysicalResistance.ToString() + "|";
        s += playerAetherResistance.ToString() + "|";
        s += playerTimeResistance.ToString() + "|";

        s += totalGold.ToString() + "|";
        s += experience.ToString() + "|";
        s += playerLevel.ToString() + "|";

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode) {
        if(!PlayerPrefs.HasKey("SaveState"))
            return;
        // get save state info and set the instance variables accordingly
        string[] saveData = PlayerPrefs.GetString("SaveState").Split('|');
        constitution = int.Parse(saveData[0]);
        strength = int.Parse(saveData[1]);
        intelligence = int.Parse(saveData[2]);
        spirituality = int.Parse(saveData[3]);
        endurance = int.Parse(saveData[4]);

        playerMaxHitpoint = float.Parse(saveData[5]);
        playerHealthRegenRate = float.Parse(saveData[6]);
        playerMaxMana = float.Parse(saveData[7]);
        playerManaRegenRate = float.Parse(saveData[8]);
        playerMaxStamina = float.Parse(saveData[9]);
        playerStaminaRegenRate = float.Parse(saveData[10]);

        playerMoveSpeed = float.Parse(saveData[11]);
        playerDashCooldown = float.Parse(saveData[12]);
        playerDashStaminaCost = float.Parse(saveData[13]);
        playerDashSpeed = float.Parse(saveData[14]);

        playerFlatDamageReduction = float.Parse(saveData[15]);
        playerPhysicalResistance = float.Parse(saveData[16]);
        playerAetherResistance = float.Parse(saveData[17]);
        playerTimeResistance = float.Parse(saveData[18]);

        totalGold = int.Parse(saveData[19]);
        experience = int.Parse(saveData[20]);
        playerLevel = int.Parse(saveData[21]);
    }
}
