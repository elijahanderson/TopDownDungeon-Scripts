using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button settingsTab;
    public Button gameGuideTab;
    public Button exitGameTab;
    public Button exitGameButton;
    public GameObject settingsContainer;
    public GameObject gameGuideContainer;
    public GameObject exitGameContainer;

    private Canvas canvas;
    private Color activeTabColor;
    private Color inactiveTabColor;

    // Start is called before the first frame update
    private void Start()
    {
        canvas = GetComponent<Canvas>();
        settingsTab.onClick.AddListener(OnEnterSettingsTab);
        gameGuideTab.onClick.AddListener(OnEnterGameGuideTab);
        exitGameTab.onClick.AddListener(OnExitGameTab);
        exitGameButton.onClick.AddListener(OnExitGameButton);
        activeTabColor = settingsTab.GetComponent<Image>().color;
        inactiveTabColor = gameGuideTab.GetComponent<Image>().color;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !canvas.enabled) {
            canvas.enabled = true;
            Time.timeScale = 0;
        } else if ((Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Escape)) && canvas.enabled) {
            canvas.enabled = false;
            Time.timeScale = 1;
        }
    }

    private void OnEnterSettingsTab()
    {
        settingsContainer.SetActive(true);
        gameGuideContainer.SetActive(false);
        exitGameContainer.SetActive(false);
        settingsTab.GetComponent<Image>().color = activeTabColor;
        gameGuideTab.GetComponent<Image>().color = inactiveTabColor;
        exitGameTab.GetComponent<Image>().color = inactiveTabColor;
    }

    private void OnEnterGameGuideTab()
    {
        gameGuideContainer.SetActive(true);
        settingsContainer.SetActive(false);
        exitGameContainer.SetActive(false);
        gameGuideTab.GetComponent<Image>().color = activeTabColor;
        settingsTab.GetComponent<Image>().color = inactiveTabColor;
        exitGameTab.GetComponent<Image>().color = inactiveTabColor;
    }

    private void OnExitGameTab()
    {
        exitGameContainer.SetActive(true);
        settingsContainer.SetActive(false);
        gameGuideContainer.SetActive(false);
        exitGameTab.GetComponent<Image>().color = activeTabColor;
        settingsTab.GetComponent<Image>().color = inactiveTabColor;
        gameGuideTab.GetComponent<Image>().color = inactiveTabColor;
    }

    private void OnExitGameButton()
    {
        Application.Quit();
    }
}
