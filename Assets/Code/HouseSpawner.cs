using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
  private int score;
  private int tempScore;

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

  public void ModifyScore(int modifyBy)
  {
    tempScore += modifyBy;
  }

  private int tier1Chance, tier2Chance, tier3Chance;

  // Update is called once per frame
  public void Update()
  {
    int newScore = Globals.MoneyManager.moneyEarned / 2000 + tempScore + Globals.PlacementManager.tier1BuildingCount + Globals.PlacementManager.tier2BuildingCount * 2 + Globals.PlacementManager.tier3BuildingCount * 3; // get money and click success rate here

    if (newScore - score >= 10)
    {
      score = newScore;

      if (score > 50)
      {
        Spawn(Random.Range(1, 3));
      }
      else if (score > 150)
      {
        Spawn(Random.Range(1, 4));
      }
      else
      {
        Spawn(1);
      }
    }
  }

  private void Spawn(int tier)
  {
    Globals.MoneyManager.ModifyMoney(Random.Range(5000, 10000) * tier);

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

    if (Physics.OverlapSphere(houseBounds.center, houseBounds.extents.x > houseBounds.extents.z ? houseBounds.extents.x : houseBounds.extents.z, LayerMask.GetMask("House")).Length != 0)
    {
      for (int j = 0; j < 10000; j++)
      {
        position = new Vector3(Random.Range(-groundBounds.extents.x + houseBounds.extents.x, groundBounds.extents.x - houseBounds.extents.x), 1, Random.Range(-groundBounds.extents.z + houseBounds.extents.z, groundBounds.extents.z - houseBounds.extents.z));

        if (Physics.OverlapSphere(houseBounds.center, houseBounds.extents.x > houseBounds.extents.z ? houseBounds.extents.x : houseBounds.extents.z, LayerMask.GetMask("House")).Length == 0)
        {
          break;
        }
      }
    }

    Instantiate(toSpawn, position, Quaternion.identity);
  }
}