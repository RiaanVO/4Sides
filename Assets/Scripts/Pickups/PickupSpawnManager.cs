using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupSpawnManager : MonoBehaviour
{
  private List<Vector3> pickupSpawnPoints;

	public GameObject healthPickupPrefab;
  private HealthPickup healthPickup;

	public GameObject rapidFirePickupPrefab;
  private RapidFirePickupScript rapidFirePickup;

	public GameObject laserFirePickupPrefab;
	private LaserPickup laserPickup;

	public GameObject explosiveFirePickupPrefab;
  private ExplosiveFirePickupScript explosiveFirePickup;

	public bool useRapidFirePickup = false;
	public bool useLaserPickup = false;
	public bool useExplosivePickup = false;

	private int healthIndex = -1;
	private int rapidIndex = -1;
	private int laserIndex = -1;
	private int explosiveIndex = -1;
	private int numSpawnPoints;

	private int numberToSpawn = 1;

    // Use this for initialization
    void Start()
    {
        pickupSpawnPoints = GameObject.FindGameObjectsWithTag("PickupSpawnPoint").Select(o => o.transform.position).ToList();
		numSpawnPoints = pickupSpawnPoints.Count ();

		healthPickup = ((GameObject)Instantiate (healthPickupPrefab)).GetComponent<HealthPickup> ();

		if (useRapidFirePickup) {
			rapidFirePickup = ((GameObject)Instantiate (rapidFirePickupPrefab)).GetComponent<RapidFirePickupScript>();
			numberToSpawn++;
		}

		if (useLaserPickup) {
			laserPickup = ((GameObject)Instantiate (laserFirePickupPrefab)).GetComponent<LaserPickup>();
			numberToSpawn++;
		}

		if (useExplosivePickup) {
			explosiveFirePickup = ((GameObject)Instantiate (explosiveFirePickupPrefab)).GetComponent<ExplosiveFirePickupScript>();
			numberToSpawn++;
		}
    }

    public void SpawnPickup()
    {
		trySpawnHealth ();
		if (numberToSpawn <= pickupSpawnPoints.Count ()) {
			if (useRapidFirePickup)
				trySpawnRapid ();
			if (useLaserPickup)
				trySpawnLaser ();
			if (useExplosivePickup)
				trySpawnExplosive ();
		}
    }

	private void trySpawnHealth(){
		if (healthPickup.IsCollected ()) {
			int positionIndex = getUniqueSpawnPointIndex ();
			healthPickup.SpawnHealthPickup (pickupSpawnPoints.ElementAt (positionIndex));
			healthIndex = positionIndex;
		}
	}

	private void trySpawnRapid(){
		if (rapidFirePickup.IsCollected ()) {
			int positionIndex = getUniqueSpawnPointIndex ();
			rapidFirePickup.SpawnRapidFirePickup (pickupSpawnPoints.ElementAt (positionIndex));
			rapidIndex = positionIndex;
		}
	}


	private void trySpawnLaser(){
		if (laserPickup.IsCollected ()) {
			int positionIndex = getUniqueSpawnPointIndex ();
			laserPickup.SpawnLaserPickup (pickupSpawnPoints.ElementAt (positionIndex));
			laserIndex = positionIndex;
		}
	}

	private void trySpawnExplosive(){
		if (explosiveFirePickup.IsCollected ()) {
			int positionIndex = getUniqueSpawnPointIndex ();
			explosiveFirePickup.SpawnExplosiveFirePickup (pickupSpawnPoints.ElementAt (positionIndex));
			explosiveIndex = positionIndex;
		}
	}

	private int getUniqueSpawnPointIndex(){
    int numTimesAttempted = 0;
		int potentialIndex;
		do {
			potentialIndex = Random.Range (0, numSpawnPoints);
      numTimesAttempted ++;
      if(numTimesAttempted > 100){
        break;
      }
		} while (indexUsed (potentialIndex));
		return potentialIndex;
	}

	private bool indexUsed(int potential){
		return potential == healthIndex || potential == rapidIndex || potential == laserIndex || potential == explosiveIndex;
	}
}
