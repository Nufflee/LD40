using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
  private GameObject housePrefab;

  // Use this for initialization
  public void Start()
  {
    housePrefab = Resources.Load<GameObject>("Prefabs/House");

    for (int i = 0; i < 10; i++)
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
    }
  }

  // Update is called once per frame
  public void Update()
  {
  }
}