using Random = System.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public GameObject floatingTextPrefab;
    public Sprite chestOpenEmpty;
    private static Random rand = new Random();
    private int goldAmount = rand.Next(5, 25);

    protected override void OnCollect() {
        if (!collected) {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = chestOpenEmpty;
            // adjust player gold amount
            GameManager.gameManagerInstance.totalGold += goldAmount;
            // show floating text
            if (floatingTextPrefab) {
                GameObject txt = Instantiate(floatingTextPrefab, transform);
                txt.GetComponentInChildren<TextMesh>().text = goldAmount.ToString() + " gold";
                Destroy(txt, 2);
            }
        }
    }
}
