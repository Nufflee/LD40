using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
  private TextMeshProUGUI moneyText;

  public int money = 1000;
  private string moneyString;

  private void Start()
  {
    moneyText = transform.Find("Panel/MoneyText").GetComponent<TextMeshProUGUI>();
  }

  public void ModifyMoney(int delta)
  {
    money += delta;
  }

  private void Update()
  {
    moneyString = "$";
/*

    string decimals;

    if (moneyString.Length >= 4)
    {
      decimals = moneyString.SubstringRange(2, 4);
    }
    else if (moneyString.Length >= 5)
    {
      decimals = moneyString.SubstringRange(3, 5);
    }
    else
    {
      decimals = "00";
    }

    print(decimals);
*/
    if (money < 1000)
    {
      moneyString += money;
    }

    if (money >= 100000000)
    {
      moneyString += Math.Round(money / 1000000000.0f, 2).ToString(CultureInfo.InvariantCulture);
      moneyString += "B";
    }
    else if (money >= 1000000)
    {
      moneyString += Math.Round(money / 1000000.0f, 2).ToString(CultureInfo.InvariantCulture);
      moneyString += "M";
    }
    else if (money >= 1000)
    {
      moneyString += Math.Round(money / 1000.0f, 2).ToString(CultureInfo.InvariantCulture);
      moneyString += "k";
    }

    moneyText.text = moneyString;
  }
}