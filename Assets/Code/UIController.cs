using UnityEngine;

public class UIController : MonoBehaviour
{
  private GameObject tier1;
  private GameObject tier2;
  private GameObject tier3;

  private void Start()
  {
    tier1 = Globals.Buildings.Tier1;
    tier2 = Globals.Buildings.Tier2;
    tier3 = Globals.Buildings.Tier3;
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

  public void OnClickBulldoze()
  {
    Globals.PlacementManager.bulldozing = true;
  }
}