using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int coins;
    // Start is called before the first frame update
    void Start() {
        GetComponent<TMPro.TextMeshProUGUI>().text = "Coins: " + coins;
    }

    // Update is called once per frame
    void Update() {
        GetComponent<TMPro.TextMeshProUGUI>().text = "Coins: " + coins;
    }

    public bool HasEnoughCoins(int value) {
        return value <= coins;
    }

    public void AddCoins(int c) {
        coins += c;
    }

    public void DecreaseCoins(int c) {
        coins -= c;
    }
}
