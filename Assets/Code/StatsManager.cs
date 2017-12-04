using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class StatsManager : MonoBehaviour
{
  // Use this for initialization
  private void Start()
  {
    transform.Find("MoneyS").GetComponent<TextMeshProUGUI>().text = FormatMoney(Globals.MoneyManager.moneySpent);
    transform.Find("MoneyE").GetComponent<TextMeshProUGUI>().text = FormatMoney(Globals.MoneyManager.moneyEarned);
    transform.Find("MaxC").GetComponent<TextMeshProUGUI>().text = (Globals.PlacementManager.absoluteBuildingCount * Random.Range(1, 3)).ToString();
    transform.Find("MaxH").GetComponent<TextMeshProUGUI>().text = Globals.PlacementManager.absoluteBuildingCount.ToString();
  }

  public string FormatMoney(int money)
  {
    string moneyString = "$";

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
    else
    {
      moneyString += money.ToString();
    }

    return moneyString;
  }
}