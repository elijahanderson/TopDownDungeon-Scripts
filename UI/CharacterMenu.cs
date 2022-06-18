using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenu : MonoBehaviour
{
    private Canvas canvas;

    private void Start() {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !canvas.enabled) {
            canvas.enabled = true;
        } else if ((Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Escape)) && canvas.enabled) {
            canvas.enabled = false;
        }
    }
}
