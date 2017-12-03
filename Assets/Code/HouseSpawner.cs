using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
  private int score;

  // Use this for initialization
  public void Start()
  {
    /*for (int i = 0; i < 10; i++)
    {
      Bounds groundBounds = Globals.Ground.GetComponent<Renderer>().bounds;
      Bounds houseBounds = housePrefab.GetComponent<Renderer>().bounds;

      Vector3 position = new Vector3(Random.Range(-groundBounds.extents.x + houseBounds.extents.x, groundBounds.extents.x - houseBounds.extents.x), 1, Random.Range(-groundBounds.extents.z + houseBounds.extents.z, groundBounds.extents.z - houseBounds.extents.z));

      if (Physics.OverlapBox(position, houseBounds.extents * 2, Quaternion.identity, LayerMask.GetMask("House")).Length != 0)
      {
        for (int j = 0; j < 1000; j++)
        {
          position = new Vector3(Random.Range(-groundBounds.extents.x + houseBounds.extents.x, groundBounds.extents.x - houseBounds.extents.x), 1, Random.Range(-groundBounds.extents.z + houseBounds.extents.z, groundBounds.extents.z - houseBounds.extents.z));

          if (Physics.OverlapBox(position, houseBounds.extents * 2, Quaternion.identity, LayerMask.GetMask("House")).Length == 0)
          {
            break;
          }
        }
      }

      Instantiate(housePrefab, position, Quaternion.identity);
    }*/
  }

  private int tier1Chance, tier2Chance, tier3Chance;

  // Update is called once per frame
  public void Update()
  {
    if (Globals.PlacementManager == null)
    {
      Globals.PlacementManager = new GameObject().AddComponent<PlacementManager>();
    }

    int newScore = Globals.PlacementManager.tier1BuildingCount + Globals.PlacementManager.tier2BuildingCount * 2 + Globals.PlacementManager.tier3BuildingCount * 3; // get money and click success rate here

    if (newScore - score >= 10)
    {
      score = newScore;

      if (score <= 100)
      {
        tier1Chance = 80;
        tier2Chance = 15;
        tier3Chance = 5;
      }
      else if (score >= 1200)
      {
        tier1Chance = 0;
        tier2Chance = 0;
        tier3Chance = 100;
      }
      else if (score >= 1000)
      {
        tier1Chance = 0;
        tier2Chance = 20;
        tier3Chance = 80;
      }
      else if (score >= 700)
      {
        tier1Chance = 5;
        tier2Chance = 35;
        tier3Chance = 60;
      }
      else if (score >= 550)
      {
        tier1Chance = 10;
        tier2Chance = 50;
        tier3Chance = 40;
      }
      else if (score >= 400)
      {
        tier1Chance = 25;
        tier2Chance = 50;
        tier3Chance = 25;
      }
      else if (score >= 200)
      {
        tier1Chance = 50;
        tier2Chance = 40;
        tier3Chance = 10;
      }
      else if (score >= 100)
      {
        tier1Chance = 80;
        tier2Chance = 15;
        tier3Chance = 5;
      }

      int chance = Random.Range(0, 101);
      print(chance);

      if (chance <= tier1Chance && chance >= 0)
      {
        print(string.Format("Spawning tier 1 at {0}", chance));
        Spawn(1);
      }
      else
      {
        chance = Random.Range(0, 101);
        if (chance <= tier2Chance)
        {
          print(string.Format("Spawning tier 2 at {0}", chance));
          Spawn(2);
        }
        else
        {
          chance = Random.Range(0, 101);

          if (chance <= tier3Chance)
          {
            print(string.Format("Spawning tier 3 at {0}", chance));
            Spawn(3);
          }
        }
      }
    }
  }

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
    Bounds houseBounds = toSpawn.GetAbsoluteBounds();

    Vector3 position = new Vector3(Random.Range(-groundBounds.extents.x + houseBounds.extents.x, groundBounds.extents.x - houseBounds.extents.x), 1, Random.Range(-groundBounds.extents.z + houseBounds.extents.z, groundBounds.extents.z - houseBounds.extents.z));

    if (Physics.OverlapBox(position, houseBounds.extents * 2, Quaternion.identity, LayerMask.GetMask("House")).Length != 0)
    {
      for (int j = 0; j < 1000; j++)
      {
        position = new Vector3(Random.Range(-groundBounds.extents.x + houseBounds.extents.x, groundBounds.extents.x - houseBounds.extents.x), 1, Random.Range(-groundBounds.extents.z + houseBounds.extents.z, groundBounds.extents.z - houseBounds.extents.z));

        if (Physics.OverlapBox(position, houseBounds.extents * 2, Quaternion.identity, LayerMask.GetMask("House")).Length == 0)
        {
          break;
        }
      }
    }

    Instantiate(toSpawn, position, Quaternion.identity);
  }
}