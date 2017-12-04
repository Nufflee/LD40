using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
  private GameObject tier1;
  private GameObject tier2;
  private GameObject tier3;

  private GameObject button;

  private bool green;

  private void Start()
  {
    tier1 = Globals.Buildings.Tier1;
    tier2 = Globals.Buildings.Tier2;
    tier3 = Globals.Buildings.Tier3;

    try
    {
      button = transform.Find("Panel/BulldozerButton").gameObject;
    }
    catch (NullReferenceException e)
    {
    }
  }

  private void Update()
  {
    try
    {
      if (Globals.PlacementManager.bulldozing && !button.GetComponent<Animation>().isPlaying && green == false)
      {
        button.gameObject.GetComponent<Animation>().Play("LerpToGreen");

        green = true;
      }
      else if (!Globals.PlacementManager.bulldozing && !button.GetComponent<Animation>().isPlaying && green)
      {
        button.gameObject.GetComponent<Animation>().Play("LerpFromGreen");

        green = false;
      }
    }
    catch (NullReferenceException e)
    {
    }
  }

  public void OnClickBuy(int tier)
  {
    Globals.PlacementManager.DeSelect();

    if (tier == 1)
    {
      Globals.PlacementManager.Select(Globals.Buildings.Tier1);
    }
    else if (tier == 2)
    {
      Globals.PlacementManager.Select(Globals.Buildings.Tier2);
    }
    else if (tier == 3)
    {
      Globals.PlacementManager.Select(Globals.Buildings.Tier3);
    }
  }

  public void OnClickBulldoze(GameObject button)
  {
    Globals.PlacementManager.DeSelect();
    Globals.PlacementManager.bulldozing = !Globals.PlacementManager.bulldozing;
  }
}