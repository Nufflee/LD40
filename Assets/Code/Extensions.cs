using UnityEngine;

public static class Extensions
{
  public static Bounds GetAbsoluteBounds(this GameObject gameObject)
  {
    Bounds combinedBounds = new Bounds(gameObject.transform.position, Vector3.zero);

    Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();

    foreach (Renderer renderer in renderers)
    {
      combinedBounds.Encapsulate(renderer.bounds);
    }

    return combinedBounds;
  }
}