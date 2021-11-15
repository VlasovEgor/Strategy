using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    public int Money;
    public Text MoneyText;
    private void Update()
    {
        MoneyText.text = "Δενεγ: " + Money;
    }
}
