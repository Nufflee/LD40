﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHouseSpawning : MonoBehaviour {

	public int counter = 0;

	// Use this for initialization
	void Start () {
		counter = 0;
		StartCoroutine(SpawnRandomHouses());
	}

	private IEnumerator SpawnRandomHouses()
	{
		yield return new WaitForSeconds (2f);
		while (counter < 20)
		{
			Spawn (Random.Range (1, 4));
			counter++;
			yield return new WaitForSeconds(1.5f);
		}

		yield return new WaitForSeconds(20f);

	}


	private void Spawn(int tier)
	{
		GameObject toSpawn = null;
		print (tier);
		if (tier == 1) {
			toSpawn = Globals.Buildings.Tier1;
		}
		if (tier == 2) {
			toSpawn = Globals.Buildings.Tier2;
		}
		if (tier == 3) {
			toSpawn = Globals.Buildings.Tier3;
		}


		Bounds groundBounds = Globals.Ground.GetComponent<Renderer>().bounds;
		Bounds houseBounds = toSpawn.GetAbsoluteBounds();

		Vector3 position = new Vector3(Random.Range(-groundBounds.extents.x + houseBounds.extents.x, groundBounds.extents.x - houseBounds.extents.x), 1, Random.Range(-groundBounds.extents.z + houseBounds.extents.z, groundBounds.extents.z - houseBounds.extents.z));

		if (Physics.OverlapBox(position, houseBounds.extents, Quaternion.identity, LayerMask.GetMask("House")).Length != 0)
		{
			for (int j = 0; j < 100000; j++)
			{
				position = new Vector3(Random.Range(-groundBounds.extents.x + houseBounds.extents.x, groundBounds.extents.x - houseBounds.extents.x), 1, Random.Range(-groundBounds.extents.z + houseBounds.extents.z, groundBounds.extents.z - houseBounds.extents.z));

				if (Physics.OverlapBox(position, houseBounds.extents, Quaternion.identity, LayerMask.GetMask("House")).Length == 0)
				{
					break;
				}
			}
		}

		GameObject GO = (GameObject) Instantiate(toSpawn, position, Quaternion.identity);
		Destroy (GO.GetComponent<Building> ());
	}
}