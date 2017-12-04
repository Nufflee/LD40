using UnityEngine;

public class Globals : MonoBehaviour
{
  private static GameObject ground;

  public static GameObject Ground
  {
    get { return ground ?? (ground = GameObject.Find("Ground")); }
  }

  private static PlacementManager placementManager;

  public static PlacementManager PlacementManager
  {
    get { return placementManager ?? (placementManager = new GameObject().AddComponent<PlacementManager>()); }
  }

  private static Buildings buildings;

  public static Buildings Buildings
  {
    get { return buildings ?? (buildings = new GameObject().AddComponent<Buildings>()); }
  }

  private static Canvas worldSpaceCanvas;

  public static Canvas WorldSpaceCanvas
  {
    get { return worldSpaceCanvas ?? (worldSpaceCanvas = GameObject.Find("WorldSpaceCanvas").GetComponent<Canvas>()); }
  }

  private static HouseSpawner houseSpawner;

  public static HouseSpawner HouseSpawner
  {
    get { return houseSpawner ?? (houseSpawner = FindObjectOfType<HouseSpawner>()); }
  }

  private void Start()
  {
    ground = GameObject.Find("Ground");
    placementManager = FindObjectOfType<PlacementManager>();
    buildings = FindObjectOfType<Buildings>();
    worldSpaceCanvas = GameObject.Find("WorldSpaceCanvas").GetComponent<Canvas>();
    houseSpawner = FindObjectOfType<HouseSpawner>();
  }
}