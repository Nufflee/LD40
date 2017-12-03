using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
  private GameObject tier1;
  private GameObject tier2;
  private GameObject tier3;

  private Image buttonImage;

  private bool green;

  private void Start()
  {
    tier1 = Globals.Buildings.Tier1;
    tier2 = Globals.Buildings.Tier2;
    tier3 = Globals.Buildings.Tier3;
  }

  private void Update()
  {
    if (Globals.PlacementManager.bulldozing && !buttonImage.gameObject.GetComponent<Animation>().isPlaying && green == false)
    {
      buttonImage.gameObject.GetComponent<Animation>().Play("LerpToGreen");

      green = true;
    }
    else if (!Globals.PlacementManager.bulldozing && !buttonImage.gameObject.GetComponent<Animation>().isPlaying && green)
    {
      buttonImage.gameObject.GetComponent<Animation>().Play("LerpFromGreen");

      green = false;
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

  public void OnClickBulldoze(Image buttonImage)
  {
    this.buttonImage = buttonImage;

    Globals.PlacementManager.DeSelect();
    Globals.PlacementManager.bulldozing = !Globals.PlacementManager.bulldozing;
  }
}