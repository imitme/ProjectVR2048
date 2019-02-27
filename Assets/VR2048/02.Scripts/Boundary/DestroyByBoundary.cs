using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{
	private void OnTriggerExit(Collider other) {
		if (other.tag == "BARREL") {
			return;
		}

		Destroy(other.GetComponentInParent<Asteroid>().gameObject);
	}
}