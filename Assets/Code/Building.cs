﻿using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour
{
  public int price;
  public int tier;

  [HideInInspector] public bool selected;

  public bool overlapping;

  private float nextClickTime = -1;

  private GameObject popup;

  private bool scalingDown;
  private bool scalingUp;

  private int failed;

  private ParticleSystem collapsingParticleSystem;

  private ParticleSystem particleSystem;

  private bool actionPending;

  /*private List<float> fs = new List<float>();#1#*/

  private void Start()
  {
    collapsingParticleSystem = Resources.Load<ParticleSystem>("Prefabs/CollapsingParticleSystem");
  }

  private void Update()
  {
    print(failed);

    if (selected)
      return;

    if (nextClickTime == -1)
    {
      nextClickTime = Time.time + Random.Range(3.0f, 8.0f) - tier / 4.0f;
    }
    if (failed == 1)
    {
      Collapse();
    }

    RaycastHit hitInfo;

    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, LayerMask.GetMask("House")))
    {
      if (Input.GetMouseButtonDown(0) && hitInfo.transform.root == transform && !Globals.PlacementManager.bulldozing && !Globals.PlacementManager.Selecting)
      {
        if (actionPending)
        {
          StopAllCoroutines();

          failed--;

          Globals.HouseSpawner.ModifyScore(2);

          StartCoroutine(ScaleDownCoroutine());
        }
      }
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

      failed++;
      StartCoroutine(ScaleDownCoroutine());
    }

    if (Time.time >= nextClickTime + 3.0f && (!scalingDown && !scalingUp) && !popup.GetComponent<Animation>().isPlaying)
    {
      popup.GetComponent<Animation>().Play("PopUpWarning");
    }

    if (Time.time >= nextClickTime && popup == null)
    {
      popup = Instantiate(Globals.Buildings.NoElectricityPopUp, new Vector3(transform.position.x + 0.367f, transform.position.y + gameObject.GetAbsoluteBounds().extents.y + 2, transform.position.z), Quaternion.identity, Globals.WorldSpaceCanvas.transform);
      StartCoroutine(ScaleUpCoroutine());
    }
  }

  public void Collapse()
  {
    Globals.HouseSpawner.ModifyScore(-5);

    if (tier == 1)
    {
      Globals.PlacementManager.tier1BuildingCount--;
    }
    if (tier == 2)
    {
      Globals.PlacementManager.tier2BuildingCount--;
    }
    if (tier == 3)
    {
      Globals.PlacementManager.tier3BuildingCount--;
    }

    particleSystem = Instantiate(collapsingParticleSystem, transform.position, Quaternion.identity);
    ParticleSystem.EmissionModule emission = particleSystem.emission;
    emission.rateOverTimeMultiplier = 150.0f * tier;
    ParticleSystem.ShapeModule shape = particleSystem.shape;
    shape.scale = new Vector3(gameObject.GetAbsoluteBounds().extents.x, 0.3f, gameObject.GetAbsoluteBounds().extents.z);
    particleSystem.Play();

    StartCoroutine(CollapseCoroutine());
  }

  private IEnumerator CollapseCoroutine()
  {
    selected = true;

    if (popup != null)
      StartCoroutine(ScaleDownCoroutine());

    while (transform.position.y >= -1.5f - gameObject.GetAbsoluteBounds().extents.y)
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
    actionPending = true;
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
    actionPending = false;
    scalingDown = true;
    popup.GetComponent<Animation>().Play("ScaleDown");
    yield return new WaitForSeconds(popup.GetComponent<Animation>().GetClip("ScaleDown").length);
    Destroy(popup);
    popup = null;
    nextClickTime = Time.time + Random.Range(3.0f, 8.0f) - tier / 4.0f;
    scalingDown = false;

//popup = Instantiate(Globals.Buildings.NoElectricityPopUp, new Vector3(transform.position.x + 0.36f, transform.position.y + 2, transform.position.z), Quaternion.identity, Globals.WorldSpaceCanvas.transform);
  }

  private void OnCollisionEnter(Collision other)
  {
    overlapping = true;
  }

  private void OnCollisionExit(Collision other)
  {
    overlapping = false;
  }
}