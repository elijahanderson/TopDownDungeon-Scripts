using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Interactable
{
    public string scene;
    protected override void OnCollide(Collider2D hit) {
        if (hit.name == "Player") {
            // teleport Player to the specified scene
            GameManager.gameManagerInstance.SaveState();
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        }
    }
}
