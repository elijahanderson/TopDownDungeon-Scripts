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
        s += totalGold.ToString() + "|";
        s += experience.ToString() + "|";

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode) {
        if(!PlayerPrefs.HasKey("SaveState"))
            return;
        // get save state info and set the instance variables accordingly
        string[] saveData = PlayerPrefs.GetString("SaveState").Split('|');
        totalGold = int.Parse(saveData[0]);
        experience = int.Parse(saveData[1]);
    }
}
