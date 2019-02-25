using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour
{
	public QUADTYPE quadType;
	public Transform cameraPos;

	private int perfectPoseControlPoints = 11;
	private int perfectControlPoints = 7;
	private int controlPoints = 3;
	private float perfectPoseLimitDistance = 0;
	private float perfectcontrolAngle = 5f;
	private float controlAngle = 15f;

	public void SendControlPoint(object[] _params) {
		int point = 0;

		Vector3 hitPos = (Vector3)_params[0];
		Vector3 hitNormal = (Vector3)_params[1];
		Vector3 firePos = (Vector3)_params[2];
		Vector3 incomeVector = (hitPos - firePos).normalized;

		HANDTYPE controlHand = (HANDTYPE)_params[3];
		float poseAngle = Vector3.Angle(incomeVector, -hitNormal);
		float poseDistance = (firePos - cameraPos.position).magnitude;

		Debug.Log(" controlHand : " + controlHand + " poseAngle : " + poseAngle + "//" + " poseDistance : " + poseDistance);

		point = CalcControlPoints(controlHand, poseAngle, poseDistance);
	}

	private int CalcControlPoints(HANDTYPE ctrlHand, float poseAngle, float poseDistance) {
		//CalcAngle()
		return 0;
	}
}