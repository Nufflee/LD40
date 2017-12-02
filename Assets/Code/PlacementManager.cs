using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementManager : MonoBehaviour
{
  private GameObject selected;
  private Material originalMaterial;
  private Shader tintShader;

  private void Start()
  {
    tintShader = Resources.Load<Shader>("Shaders/Tint");
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

      List<Collider> colliders = Physics.OverlapBox(selected.transform.position, selected.GetComponent<Renderer>().bounds.extents, Quaternion.identity, LayerMask.GetMask("House")).ToList();

      for (int i = 0; i < colliders.Count; i++)
      {
        if (colliders[i].gameObject == selected.gameObject)
        {
          colliders.RemoveAt(i);
        }
      }

      bool overlapping = colliders.Count != 0;

      Bounds groundBounds = Globals.Ground.GetComponent<Renderer>().bounds;
      Bounds selectedBounds = selected.GetComponent<Renderer>().bounds;

      bool validPlacement = !overlapping && Mathf.Abs(selected.transform.position.x) + selectedBounds.extents.x <= Mathf.Abs(groundBounds.extents.x) && Mathf.Abs(selected.transform.position.z) + selectedBounds.extents.z <= Mathf.Abs(groundBounds.extents.z);

      selected.GetComponent<Renderer>().material.shader = tintShader;

      selected.GetComponent<Renderer>().material.SetColor("_ColorTint", !validPlacement ? Utils.HexToColor("#e80e0e") : Utils.HexToColor("#13f039"));

      if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(-1))
      {
        if (validPlacement)
        {
          selected.GetComponent<Renderer>().material = originalMaterial;
          selected.GetComponent<Building>().selcted = false;

          Select(selected);
        }
      }

      if (Input.GetKeyDown(KeyCode.Escape))
      {
        DeSelect();
      }

      if (selected == null)
        return;

      selected.transform.position = new Vector3(hitInfo.point.x, selected.transform.position.y, hitInfo.point.z);
    }
  }

  public void Select(GameObject placeable)
  {
    originalMaterial = placeable.GetComponent<Renderer>().sharedMaterial;

    selected = Instantiate(placeable);

    float desiredY = Globals.Ground.transform.position.y + selected.transform.localScale.y / 2 + Globals.Ground.transform.localScale.y / 2;

    selected.transform.position = new Vector3(0, desiredY, 0);
    selected.name = "House";

    selected.GetComponent<Building>().selcted = true;
  }

  public void DeSelect()
  {
    Destroy(selected);
    selected = null;
  }
}