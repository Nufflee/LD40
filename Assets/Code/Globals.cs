using UnityEngine;

public class Globals : MonoBehaviour
{
  public static GameObject Ground { get; private set; }

  private void Start()
  {
    Ground = GameObject.Find("Ground");
  }
}