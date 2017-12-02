using UnityEngine;

public class Globals : MonoBehaviour
{
  public static GameObject Ground { get; private set; }
  public static PlacementManager PlacementManager { get; private set; }
  public static Buildings Buildings { get; private set; }
  public static Canvas WorldSpaceCanvas { get; private set; }

  private void Awake()
  {
    Ground = GameObject.Find("Ground");
    PlacementManager = FindObjectOfType<PlacementManager>();
    Buildings = FindObjectOfType<Buildings>();
    WorldSpaceCanvas = GameObject.Find("WorldSpaceCanvas").GetComponent<Canvas>();
  }
}