using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

//#pragma warning disable 649
//namespace UnityStandardAssets.Utility
//{
	public class SmoothFollowOrg : MonoBehaviour
	{
		// The target we are following
		[SerializeField]
		private int CameraKind = 1;
		[SerializeField]
		private ChangeCharactor ChangeChar = null;
		[SerializeField]
		private Transform target = null;
		[SerializeField]
		private float target_distance = 1.0f;
		// The distance in the x-z plane to the target
		[SerializeField]
		private float distance = 10.0f;
		// the height we want the camera to be above the target
		[SerializeField]
		private float height = 5.0f;

		[SerializeField]
		private float rotationDamping;
		[SerializeField]
		private float heightDamping;

		// Use this for initialization
		void Start()
        {
            // 選択中のキャラクター番号
            int nSelCharactor = SettingManager.LoadSelectCharactor();
            //nSelCharactor = 29;
        
            // 正面カメラ
            if(CameraKind == 1)
            {
                float[,] cameraInfo = new float[32,3]{
                    {0.5f, 1.5f, 0.5f}, {0.5f, 1.5f, 0.5f}, {0.5f, 1.5f, 0.5f}, {0.5f, 1.5f, 0.5f}, {0.5f, 1.5f, 0.5f}, 
                    {0.5f, 2.5f, 0.8f}, {0.5f, 2.5f, 1.3f}, {0.5f, 2.5f, 1.3f}, {0.5f, 5.0f, 2.5f}, {0.5f, 5.0f, 2.5f}, 
                    {0.5f, 5.0f, 2.5f}, {0.5f, 5.0f, 3.0f}, {0.5f,15.0f,10.0f}, {0.5f,10.0f, 5.0f}, {0.5f, 5.0f, 2.5f}, 
                    {0.5f, 5.0f, 2.5f}, {0.5f, 8.0f, 3.5f}, {0.5f, 8.0f, 3.5f}, {0.5f, 2.5f, 1.3f}, {0.5f, 2.5f, 1.3f}, 
                    {0.5f, 5.0f, 2.5f}, {0.5f, 5.0f, 2.5f}, {0.5f, 5.0f, 2.5f}, {0.5f, 5.0f, 2.5f}, {0.5f, 2.5f, 1.3f}, 
                    {0.5f, 1.5f, 0.5f}, {0.5f, 1.5f, 0.5f}, {0.5f, 5.0f, 0.8f}, {0.5f, 5.0f, 0.8f}, {0.5f, 1.5f, 0.5f}, 
                    {0.5f, 1.5f, 0.5f}, {0.5f, 1.5f, 0.5f}
                };
                target_distance = cameraInfo[nSelCharactor, 0];
                distance = cameraInfo[nSelCharactor, 1];
                height = cameraInfo[nSelCharactor, 2];                
            }
            // 上部カメラ
            else if(CameraKind == 2)
            {
                float[,] cameraInfo = new float[32,3]{
                    {0.5f, 4.0f, 2.5f}, {0.5f, 4.0f, 2.5f}, {0.5f, 4.0f, 2.5f}, {0.5f, 4.0f, 2.5f}, {0.5f, 4.0f, 2.5f},
                    {0.5f, 4.0f, 2.5f}, {0.5f, 4.0f, 2.5f}, {0.5f, 4.0f, 2.5f}, {0.5f, 8.0f, 5.0f}, {0.5f, 8.0f, 5.0f},
                    {0.5f, 8.0f, 5.0f}, {0.5f, 8.0f, 5.0f}, {0.5f,25.0f,15.0f}, {0.5f,15.0f,10.0f}, {0.5f, 8.0f, 5.0f},
                    {0.5f, 8.0f, 5.0f}, {0.5f,10.0f, 5.0f}, {0.5f,10.0f, 5.0f}, {0.5f, 4.0f, 2.5f}, {0.5f, 4.0f, 2.5f},
                    {0.5f, 8.0f, 5.0f}, {0.5f, 8.0f, 5.0f}, {0.5f, 8.0f, 5.0f}, {0.5f, 8.0f, 5.0f}, {0.5f, 4.0f, 2.5f},
                    {0.5f, 4.0f, 2.5f}, {0.5f, 4.0f, 2.5f}, {0.5f, 8.0f, 2.5f}, {0.5f, 8.0f, 2.5f}, {0.5f, 4.0f, 2.5f},
                    {0.5f, 4.0f, 2.5f}, {0.5f, 4.0f, 2.5f}
                };
                target_distance = cameraInfo[nSelCharactor, 0];
                distance = cameraInfo[nSelCharactor, 1];
                height = cameraInfo[nSelCharactor, 2];                
                
            }

        }

		// Update is called once per frame
		void LateUpdate()
		{
            if (!target)
            {
                int nSelCharactor = SettingManager.LoadSelectCharactor();
                GameObject charactor = ChangeChar.GetCharactor(nSelCharactor);
                target = charactor.transform;
            }
            
			// Early out if we don't have a target
			if (!target)
				return;
            
			// Calculate the current rotation angles
			var wantedRotationAngle = target.eulerAngles.y;
			var wantedHeight = target.position.y + height;

			var currentRotationAngle = transform.eulerAngles.y;
			var currentHeight = transform.position.y;

			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

			// Damp the height
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			// Convert the angle into a rotation
			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= currentRotation * Vector3.forward * distance;

			// Set the height of the camera
			transform.position = new Vector3(transform.position.x, currentHeight , transform.position.z);
            
			// Always look at the target
			transform.LookAt(target);
            transform.position += currentRotation * Vector3.forward * distance * target_distance;
		}
        
	}
//}