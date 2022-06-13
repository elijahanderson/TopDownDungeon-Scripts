using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Interactable
{
    protected bool collected;

    protected override void OnCollide(Collider2D hit) {
        // if player collides with a collectable, then collect
        if (hit.name == "Player")
            OnCollect();
    }

    protected virtual void OnCollect() {
        if (!collected)
            collected = true;
    }
}
