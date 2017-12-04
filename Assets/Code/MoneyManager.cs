using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
  private TextMeshProUGUI moneyText;

  private int money = 1000;
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
    moneyString = money.ToString();
    moneyString = "$" + moneyString;

    if (money >= 1000)
    {
      moneyString = moneyString.SubstringRange(0, moneyString.Length - 3);
      moneyString += "k";
    }

    if (money >= 1000000)
    {
      moneyString = moneyString.SubstringRange(0, moneyString.Length - 4);
      moneyString += "M";
    }

    if (money >= 100000000)
    {
      moneyString = moneyString.SubstringRange(0, moneyString.Length - 4);
      moneyString += "B";
    }

    moneyText.text = moneyString;
  }
}