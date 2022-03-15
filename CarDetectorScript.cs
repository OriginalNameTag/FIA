using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class CarDetectorScript : MonoBehaviour {

	public float angle = 360;
	public bool ApplyThresholds, ApplyLimits;
	public float MinX, MaxX, MinY, MaxY;
	//private bool useAngle = true;

	public float output;

	//public Vector3 output2;
	public int numObjects;

	public GameObject closestCar;

	void Start()
	{
		output = 0;
		numObjects = 0;

	}

	void Update()
	{
		GameObject[] cars;
		// YOUR CODE HER
		cars = GetAllCars();
		
		float min = 0.0f;
		float distance = 0.0f;
		numObjects = cars.Length;
		foreach(GameObject car in cars ){

			distance = Vector3.Distance(car.transform.position ,transform.position);
			Debug.DrawLine (transform.position, car.transform.position, Color.red);
			if(min == 0.0f){
				min = distance;
				closestCar = car;
			}else if(distance<min){
				min = distance;
				closestCar = car;
			}
		}
		//output2 = closestCar.transform.position - transform.position;
		output = (1.0f /(min +1.0f));
	}

	public virtual float GetOutput() { throw new NotImplementedException(); }

	// Returns all "CarToFollow" tagged objects. The sensor angle is not taken into account.
	GameObject[] GetAllCars()
	{
		return GameObject.FindGameObjectsWithTag("CarToFollow");
	}

	GameObject getClosestCar()
	{
		return closestCar;

	}

	// YOUR CODE HERE

	GameObject[] GetVisibleCars()
	{
		ArrayList visiblecars = new ArrayList();
		float halfAngle = angle / 2.0f;

		GameObject[] cars = GameObject.FindGameObjectsWithTag ("CarToFollow");

		foreach (GameObject car in cars) {
			Vector3 toVector = (car.transform.position - transform.position);
			Vector3 forward = transform.forward;
			toVector.y = 0;
			forward.y = 0;
			float angleToTarget = Vector3.Angle (forward, toVector);

			if (angleToTarget <= halfAngle) {
				visiblecars.Add (car);
			}
		}

		return (GameObject[])visiblecars.ToArray(typeof(GameObject));
	}
	
}
