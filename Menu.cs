using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private Canvas canvas;

    protected virtual void Start() {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !canvas.enabled) {
            Debug.Log("Pause");
            canvas.enabled = true;
        } else if (Input.GetKeyDown(KeyCode.Escape) && canvas.enabled) {
            Debug.Log("Continue");
            canvas.enabled = false;
        }
    }
}
