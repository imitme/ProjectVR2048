using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour
{
	public QUADTYPE quadType;
	public Transform cameraPos;

	private int perfectPoseControlPoints = 13;

	private int perfectControlPoints = 11;
	private int niceControlPoints = 7;
	private int controlPoints = 3;

	private float perfectPoseLimitDistance = 1.25f;
	private float perfectcontrolAngle = 4f;
	private float controlAngle = 10f;

	public void SendControlPoint(object[] _params) {
		int point = 0;

		Vector3 hitPos = (Vector3)_params[0];
		Vector3 hitNormal = (Vector3)_params[1];
		Vector3 firePos = (Vector3)_params[2];
		Vector3 incomeVector = (hitPos - firePos).normalized;

		HANDTYPE controlHand = (HANDTYPE)_params[3];
		QUADTYPE hitQuad = quadType;
		float poseAngle = Vector3.Angle(incomeVector, -hitNormal);
		float poseDistance = (firePos - cameraPos.position).magnitude;

		Debug.Log(" controlHand : " + controlHand + " poseAngle : " + poseAngle + "//" + " poseDistance : " + poseDistance);
		GameManager.Instance.controlPointText.text = string.Format(" ");
		bool isMoved = CheckIsCellMoved();
		if (isMoved) {
			point = CalcControlPoints(controlHand, hitQuad, poseAngle, poseDistance);
			GameManager.Instance.Score += point;
		}
	}

	private bool CheckIsCellMoved() {
		return GameManager.Instance.CheckIsCellMoved();
	}

	private int CalcControlPoints(HANDTYPE ctrlHand, QUADTYPE hitQuad, float poseAngle, float poseDistance) {
		int distancePoint = CalcPoseDistance(hitQuad, poseAngle, poseDistance);
		int posePoint = CalcPoseControl(ctrlHand, hitQuad, poseAngle);

		return distancePoint + posePoint;
	}

	private int CalcPoseDistance(QUADTYPE hitQuad, float poseAngle, float poseDistance) {
		int distancePoint = 0;

		if (hitQuad == QUADTYPE.UPQUAD) {
			if (poseDistance > perfectPoseLimitDistance - 0.5f) {
				GameManager.Instance.controlPointText.text += string.Format("Nice Big Pose!!\n");
				distancePoint = CalcPoseAngle(poseAngle);
				//GameManager.Instance.controlPointText.text += string.Format("NicePose!! DoublePoints!: + {0}points \n", distancePoint * 2);
				return distancePoint * 2;
			} else
				return CalcPoseAngle(poseAngle);
		} else {
			if (poseDistance > perfectPoseLimitDistance) {
				GameManager.Instance.controlPointText.text += string.Format("Nice Big Pose!!\n");
				distancePoint = CalcPoseAngle(poseAngle);
				//GameManager.Instance.controlPointText.text += string.Format("NicePose!! DoublePoints!: + {0}points \n", distancePoint * 2);
				return distancePoint * 2;
			} else
				return CalcPoseAngle(poseAngle);
		}
	}

	private int CalcPoseControl(HANDTYPE ctrlHand, QUADTYPE hitQuad, float poseAngle) {
		int anglePoint = 0;

		if (hitQuad == QUADTYPE.LEFTQUAD && ctrlHand == HANDTYPE.RIGHTHAND) {
			GameManager.Instance.controlPointText.text += string.Format("fantastic Pose!!\n");
			anglePoint = CalcPoseAngle(poseAngle);
			//GameManager.Instance.controlPointText.text += string.Format("PoseDoublePoints!!\n");
			return anglePoint * 2;
		} else if (hitQuad == QUADTYPE.RIGHTQUAD && ctrlHand == HANDTYPE.LEFTHAND) {
			GameManager.Instance.controlPointText.text += string.Format("fantastic Pose!!\n");
			anglePoint = CalcPoseAngle(poseAngle);
			//GameManager.Instance.controlPointText.text += string.Format("PoseDoublePoints!!\n");
			return anglePoint * 2;
		} else //if (hitQuad == QUADTYPE.UPQUAD || hitQuad == QUADTYPE.DOWNQUAD) {
			return CalcPoseAngle(poseAngle);
		//}
	}

	private int CalcPoseAngle(float poseAngle) {
		if (poseAngle < 1.0f) {
			GameManager.Instance.controlPointText.text += string.Format("Perfect Aim Control!! : 오차 {0} \n", (int)poseAngle);
			return perfectControlPoints;
		} else if (poseAngle < perfectcontrolAngle) {
			GameManager.Instance.controlPointText.text += string.Format("Nice Aim Control!! : 오차 {0} \n", (int)poseAngle);
			return niceControlPoints;
		} else if (poseAngle < controlAngle) {
			GameManager.Instance.controlPointText.text += string.Format("Need more Aim Control!! : 오차 {0} \n", (int)poseAngle);
			return controlPoints;
		} else
			return 0;
	}
}