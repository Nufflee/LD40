using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour
{
  public int price;
  public int tier;

  public bool selcted;

  private float nextClickTime = -1;

  private GameObject popup;

  private bool scalingDown;
  private bool scalingUp;

  private int failed;

  /*private List<float> fs = new List<float>();#1#*/

  private void Start()
  {
    nextClickTime = Time.time + 0.2f;
  }

  private void Update()
  {
    if (selcted)
      return;

    if (nextClickTime == -1)
    {
      nextClickTime = Time.time + Random.Range(3.0f, 8.0f) - tier / 4.0f;
    }

    if (failed >= 3)
      print("moving out!");

    /* for (int i = 0; i < 1000; i++)
     {
       fs.Add(Random.Range(3.0f, 7.0f) - tier / 2.0f);
     }
 
     print(fs.Average());
 
     fs.Clear();*/

    if (Time.time >= nextClickTime + 5.0f && (!scalingDown && !scalingUp))
    {
      //popup.GetComponent<Animation>().Stop();

      StartCoroutine(ScaleDownCoroutine());
    }

    if (Time.time >= nextClickTime + 3.0f && (!scalingDown && !scalingUp) && !popup.GetComponent<Animation>().isPlaying)
    {
      popup.GetComponent<Animation>().Play("NoElectricityPopUpWarning");
    }

    if (Time.time >= nextClickTime && popup == null)
    {
      popup = Instantiate(Globals.Buildings.NoElectricityPopUp, new Vector3(transform.position.x + 0.367f, transform.position.y + GetComponent<Renderer>().bounds.extents.y + 2, transform.position.z), Quaternion.identity, Globals.WorldSpaceCanvas.transform);
      StartCoroutine(ScaleUpCoroutine());
    }
  }

/*
  private IEnumerator RandomMoveCoroutine()
  {
    while (popup != null)
    {
      popup.transform.position = new Vector3(popup.transform.position.x + Random.Range(-0.06f, 0.06f), popup.transform.position.y, popup.transform.position.z);

      yield return null;
    }
  }
*/

  private IEnumerator ScaleUpCoroutine()
  {
    scalingUp = true;
    popup.GetComponent<Animation>().Play("NoElectricityPopUpScaleUp");
    yield return new WaitForSeconds(popup.GetComponent<Animation>().GetClip("NoElectricityPopUpScaleUp").length);
    scalingUp = false;
  }

  private IEnumerator ScaleDownCoroutine()
  {
    failed++;
    scalingDown = true;
    popup.GetComponent<Animation>().Play("NoElectricityPopUpScaleDown");
    yield return new WaitForSeconds(popup.GetComponent<Animation>().GetClip("NoElectricityPopUpScaleDown").length);
    Destroy(popup);
    popup = null;
    nextClickTime = Time.time + Random.Range(3.0f, 8.0f) - tier / 4.0f;
    scalingDown = false;

//popup = Instantiate(Globals.Buildings.NoElectricityPopUp, new Vector3(transform.position.x + 0.36f, transform.position.y + 2, transform.position.z), Quaternion.identity, Globals.WorldSpaceCanvas.transform);
  }

  private void OnMouseOver()
  {
    if (Input.GetMouseButtonDown(0))
    {
      if (popup != null)
      {
        StopAllCoroutines();

        failed--;

        StartCoroutine(ScaleDownCoroutine());
      }
    }
  }
}