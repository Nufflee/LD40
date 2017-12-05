using UnityEngine;

public class Globals : MonoBehaviour
{
  private static GameObject ground;

  public static GameObject Ground
  {
    get { return ground != null ? ground : (ground = GameObject.Find("Ground")); }
  }

  private static PlacementManager placementManager;

  public static PlacementManager PlacementManager
  {
    get { return placementManager != null ? placementManager : (placementManager = new GameObject().AddComponent<PlacementManager>()); }
  }

  private static Buildings buildings;

  public static Buildings Buildings
  {
    get { return buildings != null ? buildings : (buildings = new GameObject().AddComponent<Buildings>()); }
  }

  private static Canvas worldSpaceCanvas;

  public static Canvas WorldSpaceCanvas
  {
    get { return worldSpaceCanvas != null ? worldSpaceCanvas : (worldSpaceCanvas = GameObject.Find("WorldSpaceCanvas").GetComponent<Canvas>()); }
  }

  private static HouseSpawner houseSpawner;

  public static HouseSpawner HouseSpawner
  {
    get { return houseSpawner != null ? houseSpawner : (houseSpawner = FindObjectOfType<HouseSpawner>()); }
  }

  private static MoneyManager moneyMananger;

  public static MoneyManager MoneyManager
  {
    get { return moneyMananger != null ? moneyMananger : (moneyMananger = FindObjectOfType<MoneyManager>()); }
  }

  private static AlertManager alertManager;

  public static AlertManager AlertManager
  {
    get { return alertManager != null ? alertManager : (alertManager = FindObjectOfType<AlertManager>()); }
  }

  private void Start()
  {
    DontDestroyOnLoad(this);
    DontDestroyOnLoad(MoneyManager.gameObject);
    DontDestroyOnLoad(PlacementManager.gameObject);

    ground = GameObject.Find("Ground");
    placementManager = FindObjectOfType<PlacementManager>();
    buildings = FindObjectOfType<Buildings>();
    worldSpaceCanvas = GameObject.Find("WorldSpaceCanvas").GetComponent<Canvas>();
    houseSpawner = FindObjectOfType<HouseSpawner>();
    moneyMananger = FindObjectOfType<MoneyManager>();
    alertManager = FindObjectOfType<AlertManager>();
  }
}