using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
  private GameObject selected;
  private Material originalMaterial;

  private void Start()
  {
    Select(Instantiate(Resources.Load<GameObject>("Prefabs/House")));
  }

  private void Update()
  {
    if (selected == null)
      return;

    RaycastHit hitInfo;

    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, LayerMask.GetMask("Ground")))
    {
      if (hitInfo.collider == null || hitInfo.normal.z < 0)
        return;

      List<Collider> colliders = Physics.OverlapBox(selected.transform.position, selected.GetComponent<Renderer>().bounds.extents / 2, Quaternion.identity, LayerMask.GetMask("House")).ToList();

      for (int i = 0; i < colliders.Count; i++)
      {
        if (colliders[i].gameObject == selected.gameObject)
        {
          colliders.RemoveAt(i);
        }
      }

      bool overlapping = colliders.Count != 0;

      selected.GetComponent<Renderer>().material.shader = Resources.Load<Shader>("Shaders/Tint");

      selected.GetComponent<Renderer>().material.SetColor("_ColorTint", overlapping ? Utils.HexToColor("#e80e0e") : Utils.HexToColor("#13f039"));

      if (Input.GetMouseButtonDown(0))
      {
        if (!overlapping)
        {
          selected.GetComponent<Renderer>().material = originalMaterial;

          selected = null;
        }
      }

      if (selected == null)
        return;

      selected.transform.position = new Vector3(Mathf.RoundToInt(hitInfo.point.x), 1, Mathf.RoundToInt(hitInfo.point.z));
    }
  }

  public void Select(GameObject placeable)
  {
    selected = placeable;
    originalMaterial = selected.GetComponent<Renderer>().material;
  }
}