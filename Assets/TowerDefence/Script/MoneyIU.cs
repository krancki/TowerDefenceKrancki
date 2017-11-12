using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyIU : MonoBehaviour {

    public Text moneyText;


    private void Update()
    {
        moneyText.text = "$" + PlayerStats.Money.ToString();
    }
}
