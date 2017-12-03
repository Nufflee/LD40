using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour
{
  public int price;
  public int tier;

  public bool selected;

  private float nextClickTime = -1;

  private GameObject popup;

  private bool scalingDown;
  private bool scalingUp;

  private int failed;

  private ParticleSystem collapsingParticleSystem;

  private ParticleSystem particleSystem;

  /*private List<float> fs = new List<float>();#1#*/

  private void Start()
  {
    collapsingParticleSystem = Resources.Load<ParticleSystem>("Prefabs/CollapsingParticleSystem");
  }

  private void Update()
  {
    if (selected)
      return;

    if (nextClickTime == -1)
    {
      nextClickTime = Time.time + Random.Range(3.0f, 8.0f) - tier / 4.0f;
    }

    if (failed >= 3)
    {
      particleSystem = Instantiate(collapsingParticleSystem, transform.position, Quaternion.identity);
      ParticleSystem.EmissionModule emission = particleSystem.emission;
      emission.rateOverTimeMultiplier = 30.0f * tier;
      ParticleSystem.ShapeModule shape = particleSystem.shape;
      shape.scale = new Vector3(GetComponent<Renderer>().bounds.extents.x * 1.5f, 0.1f, GetComponent<Renderer>().bounds.extents.z * 1.5f);
      particleSystem.Play();

      StartCoroutine(CollapseCoroutine());
    }

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
      popup.GetComponent<Animation>().Play("PopUpWarning");
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

  private IEnumerator CollapseCoroutine()
  {
    selected = true;

    if (popup != null)
      StartCoroutine(ScaleDownCoroutine());

    while (transform.position.y >= -0.05f - GetComponent<Renderer>().bounds.extents.y)
    {
      transform.position -= new Vector3(0.0f, 0.03f, 0.0f);

      yield return null;
    }

    StartCoroutine(ScaleDownParticleSystemCoroutine());
  }

  private IEnumerator ScaleUpCoroutine()
  {
    scalingUp = true;
    popup.GetComponent<Animation>().Play("ScaleUp");
    yield return new WaitForSeconds(popup.GetComponent<Animation>().GetClip("ScaleUp").length);
    scalingUp = false;
  }

  private IEnumerator ScaleDownParticleSystemCoroutine()
  {
    particleSystem.GetComponent<Animation>().Play("ScaleDown");

    yield return new WaitForSeconds(particleSystem.GetComponent<Animation>().GetClip("ScaleDown").length);

    Destroy(particleSystem.gameObject);
    Destroy(gameObject);
  }

  private IEnumerator ScaleDownCoroutine()
  {
    failed++;
    scalingDown = true;
    popup.GetComponent<Animation>().Play("ScaleDown");
    yield return new WaitForSeconds(popup.GetComponent<Animation>().GetClip("ScaleDown").length);
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