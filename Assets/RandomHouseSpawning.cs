using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomHouseSpawning : MonoBehaviour
{
  public int counter = 0;

  // Use this for initialization
  void Start()
  {
    counter = 0;

    for (int i = 0; i < Random.Range(40, 70); i++)
    {
      Spawn(Random.Range(1, 4));
    }
  }

  /*private IEnumerator SpawnRandomHouses()
  {
    yield return new WaitForSeconds(2f);
    while (counter < 20)
    {
      Spawn(Random.Range(1, 4));
      counter++;
      yield return new WaitForSeconds(1.5f);
    }

    yield return new WaitForSeconds(20f);
  }*/

  private void Spawn(int tier)
  {
    GameObject toSpawn = null;

    if (tier == 1)
    {
      toSpawn = Globals.Buildings.Tier1;
    }
    if (tier == 2)
    {
      toSpawn = Globals.Buildings.Tier2;
    }
    if (tier == 3)
    {
      toSpawn = Globals.Buildings.Tier3;
    }

    Bounds groundBounds = Globals.Ground.GetComponent<Renderer>().bounds;
    Bounds selectedBounds = toSpawn.GetAbsoluteBounds();

    List<Collider> colliders = Physics.OverlapSphere(toSpawn.GetAbsoluteBounds().center, toSpawn.GetAbsoluteBounds().extents.x > toSpawn.GetAbsoluteBounds().extents.z ? toSpawn.GetAbsoluteBounds().extents.x : toSpawn.GetAbsoluteBounds().extents.z, LayerMask.GetMask("House")).ToList();

    for (int i = 0; i < colliders.Count; i++)
    {
      if (colliders[i].gameObject == toSpawn.gameObject || colliders[i].transform.IsChildOf(toSpawn.transform))
      {
        colliders.RemoveAt(i);
      }
    }

    for (int i = 0; i < colliders.Count; i++)
    {
      if (colliders[i].gameObject.transform.IsChildOf(toSpawn.transform))
        colliders.RemoveAt(i);
    }

    Vector3 position = new Vector3(Random.Range(-groundBounds.extents.x + selectedBounds.extents.x, groundBounds.extents.x - selectedBounds.extents.x), 1, Random.Range(-groundBounds.extents.z + selectedBounds.extents.z, groundBounds.extents.z - selectedBounds.extents.z));

    bool overlapping = colliders.Count > 0;

    bool validPlacement = !overlapping && Mathf.Abs(toSpawn.transform.position.x) + selectedBounds.extents.x <= Mathf.Abs(groundBounds.extents.x) && Mathf.Abs(toSpawn.transform.position.z) + selectedBounds.extents.z <= Mathf.Abs(groundBounds.extents.z);

    if (!validPlacement)
    {
      for (int i = 0; i < 10000; i++)
      {
        position = new Vector3(Random.Range(-groundBounds.extents.x + selectedBounds.extents.x, groundBounds.extents.x - selectedBounds.extents.x), 1, Random.Range(-groundBounds.extents.z + selectedBounds.extents.z, groundBounds.extents.z - selectedBounds.extents.z));

        print(position);

        toSpawn.transform.position = position;

        colliders = Physics.OverlapSphere(toSpawn.GetAbsoluteBounds().center, toSpawn.GetAbsoluteBounds().extents.x > toSpawn.GetAbsoluteBounds().extents.z ? toSpawn.GetAbsoluteBounds().extents.x : toSpawn.GetAbsoluteBounds().extents.z, LayerMask.GetMask("House")).ToList();

        for (int j = 0; j < colliders.Count; j++)
        {
          if (colliders[j].gameObject == toSpawn.gameObject || colliders[j].transform.IsChildOf(toSpawn.transform))
          {
            colliders.RemoveAt(j);
          }
        }

        for (int j = 0; j < colliders.Count; j++)
        {
          if (colliders[j].gameObject.transform.IsChildOf(toSpawn.transform))
            colliders.RemoveAt(j);
        }

        overlapping = colliders.Count > 0;

        validPlacement = !overlapping && Mathf.Abs(toSpawn.transform.position.x) + selectedBounds.extents.x <= Mathf.Abs(groundBounds.extents.x) && Mathf.Abs(toSpawn.transform.position.z) + selectedBounds.extents.z <= Mathf.Abs(groundBounds.extents.z);

        if (validPlacement)
        {
          break;
        }
        else
        {
          print(validPlacement);
        }
      }
    }

    GameObject GO = (GameObject) Instantiate(toSpawn, position, Quaternion.identity);
    GO.transform.SetParent(Globals.Ground.transform);
    GO.transform.eulerAngles = new Vector3(0, 90 * Random.Range(0, 5), 0);
    Color color = Random.ColorHSV();

    foreach (Renderer renderer in GO.GetComponentsInChildren<Renderer>())
    {
      foreach (Material material in renderer.materials)
      {
        if (material.name.Contains("HouseBase"))
        {
          material.color = color;
        }
      }
    }

    Destroy(GO.GetComponent<Building>());
  }
}