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
    public int constitution;  // each level adds 20 HP and 10% health regen
    public int strength;  // each level adds 1% damage reduction and 1% block chance
    public int intelligence;  // each level adds 10 mana and 5% mana regen
    public int spirituality;  // each level adds 10 mana, 5% mana regen, 10 HP, 5% hp regen to player's minions
    public int endurance;  // each level adds 10 stamina, 5% stamina regen, 1 max equip load, 2% movespeed
    // player equipment & stats
    public int equipLoad;
    public float playerMaxHitpoint;
    public float playerHealthRegenRate;
    public float playerMaxMana;
    public float playerManaRegenRate;
    public float playerMaxStamina;
    public float playerStaminaRegenRate;
    public float playerMoveSpeed;
    public int totalGold;
    public int experience;

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
        s += experience.ToString() + "|";
        s += experience.ToString() + "|";
        // equipment and stats
        s += equipLoad.ToString() + "|";
        s += playerMaxHitpoint.ToString() + "|";
        s += playerHealthRegenRate.ToString() + "|";
        s += playerMaxMana.ToString() + "|";
        s += playerManaRegenRate.ToString() + "|";
        s += playerMaxStamina.ToString() + "|";
        s += playerStaminaRegenRate.ToString() + "|";
        s += playerMoveSpeed.ToString() + "|";
        s += totalGold.ToString() + "|";
        s += experience.ToString() + "|";

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
        equipLoad = int.Parse(saveData[5]);
        playerMaxHitpoint = int.Parse(saveData[6]);
        playerHealthRegenRate = int.Parse(saveData[7]);
        playerMaxMana = int.Parse(saveData[8]);
        playerManaRegenRate = int.Parse(saveData[9]);
        playerMaxStamina = int.Parse(saveData[10]);
        playerStaminaRegenRate = int.Parse(saveData[11]);
        playerMoveSpeed = int.Parse(saveData[12]);
        totalGold = int.Parse(saveData[13]);
        experience = int.Parse(saveData[14]);
    }
}
