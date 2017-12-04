using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlacementManager : MonoBehaviour
{
  private GameObject selected;

  public int absoluteBuildingCount;

  public int tier1BuildingCount;
  public int tier2BuildingCount;
  public int tier3BuildingCount;

  public bool bulldozing;

  private int rotation = 90;

  public bool Selecting
  {
    get { return selected != null; }
  }

  private void Start()
  {
  }

  private void Update()
  {
    if (tier1BuildingCount + tier2BuildingCount + tier3BuildingCount <= 0 && absoluteBuildingCount > 0)
      SceneManager.LoadScene("EndStats");

    RaycastHit hitInfo;

    if (bulldozing)
    {
      if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
      {
        bulldozing = false;
      }

      if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, LayerMask.GetMask("House")))
      {
        if (Input.GetMouseButtonDown(0))
        {
          if (hitInfo.collider.transform.root.GetComponent<Building>() != null)
          {
            hitInfo.collider.transform.root.GetComponent<Building>().Collapse();

            int price = hitInfo.collider.transform.root.GetComponent<Building>().price;

            Globals.MoneyManager.ModifyMoney(price - (int) (price * 0.75f));
            print(price - (int) (price * 0.75f));
          }
        }
      }
    }

    if (selected == null)
      return;

    if (!EventSystem.current.IsPointerOverGameObject(-1))
    {
      if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, LayerMask.GetMask("Ground")))
      {
        if (hitInfo.collider == null || hitInfo.normal.z < 0)
          return;

        List<Collider> colliders = Physics.OverlapSphere(selected.GetAbsoluteBounds().center, selected.GetAbsoluteBounds().extents.x > selected.GetAbsoluteBounds().extents.z ? selected.GetAbsoluteBounds().extents.x : selected.GetAbsoluteBounds().extents.z, LayerMask.GetMask("House")).ToList();

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
          selected.transform.eulerAngles = new Vector3(0, rotation, 0);

          rotation += 90;

          if (rotation > 360)
          {
            rotation = 90;
          }
        }

        if (Input.GetMouseButtonDown(0))
        {
          if (validPlacement)
          {
            if (Globals.MoneyManager.ModifyMoney(-selected.GetComponent<Building>().price))
            {
              selected.GetComponent<Building>().selected = false;

              Select(selected);

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

              absoluteBuildingCount += selected.GetComponent<Building>().tier;
            }
          }
          else
          {
            Globals.AlertManager.Alert("Invalid placement location!");
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
}