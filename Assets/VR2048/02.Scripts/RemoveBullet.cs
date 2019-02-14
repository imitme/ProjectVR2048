using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
	public float lifeTimeChanged = 50f; //게임끝날때 없애는 걸로!!!! 변경하기!!!!!! 드럼통 몇번 맞으면, 없어질거니까.
	public GameObject sparkEffect;

	private string collisionTagNameforDestroy = "BULLET";

	private void OnCollisionEnter(Collision coll)
	{
		if(coll.collider.tag==collisionTagNameforDestroy)
		{
			ShowEffect(coll);
			Destroy(coll.gameObject);
		}
	}

	private void ShowEffect(Collision coll)
	{
		ShowSparkEffect(coll);
	}

	private void ShowSparkEffect(Collision coll)
	{
		ContactPoint contact = coll.contacts[0];
		Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contact.normal);
		GameObject spark = Instantiate(sparkEffect, contact.point, rot);
		spark.transform.SetParent(this.transform);
		spark.GetComponent<DecalDestroyer>().lifeTime=lifeTimeChanged;
	}
}