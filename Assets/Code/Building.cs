using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour
{
  public int price;
  public int tier;

  private float nextClickTime;

  private GameObject popup;

  /*private List<float> fs = new List<float>();#1#*/

  private void Start()
  {
    nextClickTime = Time.time + Random.Range(3.0f, 8.0f) - tier / 4.0f;
  }

  private void Update()
  {
    /* for (int i = 0; i < 1000; i++)
     {
       fs.Add(Random.Range(3.0f, 7.0f) - tier / 2.0f);
     }
 
     print(fs.Average());
 
     fs.Clear();*/

    if (Time.time >= nextClickTime)
    {
      StartCoroutine(ScaleDownCoroutine());
    }
  }

  private IEnumerator ScaleDownCoroutine()
  {
    popup.GetComponent<Animation>().Play("NoElectricityPopUpScaleDown");

    yield return new WaitForSeconds(popup.GetComponent<Animation>().GetClip("NoElectricityPopUpScaleDown").length);

    Destroy(popup);

    nextClickTime = Time.time + Random.Range(3.0f, 8.0f) - tier / 4.0f;

    popup = Instantiate(Globals.Buildings.NoElectricityPopUp, new Vector3(transform.position.x + 0.36f, transform.position.y + 2, transform.position.z), Quaternion.identity, Globals.WorldSpaceCanvas.transform);
  }
}