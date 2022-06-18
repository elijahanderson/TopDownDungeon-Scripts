using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public ContactFilter2D filter;  // describes what player can interact with
    private Collider2D[] hits = new Collider2D[10];  // list of collidable objects that the player can interact with
    protected BoxCollider2D boxCollider;

    protected virtual void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Collision and interaction work
    protected virtual void Update() {
        // look for objects the player can interact with
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i] != null) {
                OnCollide(hits[i]);
            }
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D hit) {
    }
}
