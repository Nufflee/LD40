using UnityEngine;

public class Buildings : MonoBehaviour
{
  public GameObject Tier1 { get; private set; }
  public GameObject Tier2 { get; private set; }
  public GameObject Tier3 { get; private set; }

  public GameObject NoElectricityPopUp { get; private set; }

  private void Start()
  {
    Tier1 = Resources.Load<GameObject>("Prefabs/BuildingTier1");
    Tier2 = Resources.Load<GameObject>("Prefabs/BuildingTier2");
    Tier3 = Resources.Load<GameObject>("Prefabs/BuildingTier3");
    NoElectricityPopUp = Resources.Load<GameObject>("Prefabs/NoElectricityPopUp");
  }
}