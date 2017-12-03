using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementManager : MonoBehaviour
{
  private GameObject selected;

  private TextMeshProUGUI invalidPlacementText;

  public int tier1BuildingCount;
  public int tier2BuildingCount;
  public int tier3BuildingCount;

  public bool bulldozing;

  private void Start()
  {
    invalidPlacementText = GameObject.Find("InvalidPlacementText").GetComponent<TextMeshProUGUI>();
  }

  private void Update()
  {
    RaycastHit hitInfo;

    if (bulldozing)
    {
      if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, LayerMask.GetMask("House")))
      {
        if (Input.GetMouseButtonDown(0))
        {
          if (hitInfo.collider.transform.root.GetComponent<Building>() != null)
          {
            hitInfo.collider.transform.root.GetComponent<Building>().Collapse();
          }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
          bulldozing = false;
        }
      }
    }

    if (selected == null)
      return;

    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, LayerMask.GetMask("Ground")))
    {
      if (hitInfo.collider == null || hitInfo.normal.z < 0)
        return;

      List<Collider> colliders = Physics.OverlapBox(selected.transform.position, selected.GetAbsoluteBounds().extents, Quaternion.identity, LayerMask.GetMask("House")).ToList();

      for (int i = 0; i < colliders.Count; i++)
      {
        if (colliders[i].gameObject == selected.gameObject || colliders[i].transform.IsChildOf(selected.transform))
        {
          colliders.RemoveAt(i);
        }
      }

      for (int i = 0; i < colliders.Count; i++)
      {
        if (colliders[i].gameObject.transform.IsChildOf(selected.transform))
          colliders.RemoveAt(i);
      }

      bool overlapping = colliders.Count > 0;

      Bounds groundBounds = Globals.Ground.GetComponent<Renderer>().bounds;
      Bounds selectedBounds = selected.GetAbsoluteBounds();

      bool validPlacement = !overlapping && Mathf.Abs(selected.transform.position.x) + selectedBounds.extents.x <= Mathf.Abs(groundBounds.extents.x) && Mathf.Abs(selected.transform.position.z) + selectedBounds.extents.z <= Mathf.Abs(groundBounds.extents.z);

      if (Input.GetKeyDown(KeyCode.R))
      {
        selected.transform.Rotate(new Vector3(0, 1, 0), 90.0f);
      }

      if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(-1))
      {
        if (validPlacement)
        {
          if (selected.GetComponent<Building>().tier == 1)
          {
            tier1BuildingCount++;
          }

          if (selected.GetComponent<Building>().tier == 2)
          {
            tier2BuildingCount++;
          }

          if (selected.GetComponent<Building>().tier == 3)
          {
            tier3BuildingCount++;
          }

          selected.GetComponent<Building>().selected = false;

          Select(selected);
        }
        else
        {
          StartCoroutine(ShowInvalidPlacementText());
        }
      }

      if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
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
    bulldozing = false;

    selected = Instantiate(placeable);

    float desiredY = Globals.Ground.transform.position.y + selected.GetAbsoluteBounds().extents.y / 2;

    selected.transform.position = new Vector3(0, desiredY, 0);
    selected.name = "House";

    Color color = Random.ColorHSV();

    foreach (Renderer renderer in selected.GetComponentsInChildren<Renderer>())
    {
      foreach (Material material in renderer.materials)
      {
        if (material.name.Contains("HouseBase"))
        {
          material.color = color;
        }
      }
    }

    selected.GetComponent<Building>().selected = true;
  }

  public void DeSelect()
  {
    Destroy(selected);
    selected = null;
  }

  private IEnumerator ShowInvalidPlacementText()
  {
    while (invalidPlacementText.GetComponent<TextMeshProUGUI>().color.a <= 1)
    {
      invalidPlacementText.GetComponent<TextMeshProUGUI>().color += new Color(0.0f, 0.0f, 0.0f, 0.1f);

      yield return null;
    }

    yield return new WaitForSeconds(0.5f);

    while (invalidPlacementText.GetComponent<TextMeshProUGUI>().color.a > 0)
    {
      invalidPlacementText.GetComponent<TextMeshProUGUI>().color -= new Color(0.0f, 0.0f, 0.0f, 0.1f);

      yield return null;
    }
  }
}