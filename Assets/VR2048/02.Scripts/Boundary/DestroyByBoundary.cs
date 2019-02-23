using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{
	private void OnTriggerExit(Collider other) {
		Debug.Log("Boundary!!!");
		if (other.tag == "BARREL")
			return;

		Destroy(other.gameObject);
	}
}