﻿using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
  private TextMeshProUGUI moneyText;

  private int money = 500000;
  private string moneyString;

  public int moneyEarned;
  public int moneySpent;

  private void Start()
  {
    try
    {
      moneyText = transform.Find("Panel/MoneyText").GetComponent<TextMeshProUGUI>();
    }
    catch (NullReferenceException)
    {
    }
  }

  public bool ModifyMoney(int delta, bool popup = true)
  {
    if (money + delta < 0)
    {
      Globals.AlertManager.Alert("Not enough money!");

      return false;
    }

    if (delta > 0)
    {
      moneyEarned += delta;
    }
    else
    {
      moneySpent += Mathf.Abs(delta);
    }

    money += delta;

    return true;
  }

  private void Update()
  {
    if (moneyText == null)
      return;

    moneyString = "$";

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