using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParabolicTrajectroy : MonoBehaviour
{
    public LineRenderer LineRenderer;
    public int resloution = 30;
    public float timeStep = 0.1f;


    public Transform launchPoint;
    public float myRoataion;
    public float launchPower;
    public float launchAngle;
    public float launchDirection;
    public float gravity = -9.8f;
    public GameObject projectilePrefabs;


    Vector3 CalculatePositionAtTime(float time)
    {
        float launchAngleRad = Mathf.Deg2Rad * launchAngle;
        float launchDirectioneRad = Mathf.Deg2Rad * launchDirection;

        float x = launchPower * time * Mathf.Cos(launchAngleRad) * Mathf.Cos(launchDirectioneRad);
        float y = launchPower * time * Mathf.Cos(launchAngleRad) * Mathf.Sin(launchDirectioneRad);
        float z = launchPower * time * Mathf.Cos(launchAngleRad) + 0.5f * gravity * time * time;

        //발사 위치를 기준으로 계산된 위치 반환
        return launchPoint.position + new Vector3(x, y, z);
    }

    
    void RenderTrajectory()
    {
        LineRenderer.positionCount = resloution;
        Vector3[] points = new Vector3[resloution];

        for (int i = 0; i < resloution; i++)
        {
            float t = i * timeStep;
            points[i] = CalculatePositionAtTime(t);
        }

        LineRenderer.SetPositions(points);
    }
    public void LaunchProgectile(GameObject _object)
    {
        GameObject temp = Instantiate(_object);
        temp.transform.position = launchPoint.position;
        temp.transform.rotation = launchPoint.rotation;

        //Rigidbody 컴포넌트를 가져옴
        Rigidbody rb = temp.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = temp.AddComponent<Rigidbody>();
        }
        if (rb != null)
        {
            rb.isKinematic = false;

            //발사 각도와 방량을 라디안으로 변환
            float launchAngleRad = Mathf.Deg2Rad * launchAngle;
            float launchDirectioneRad = Mathf.Deg2Rad * launchDirection;

            //초기 속도를 계산
            float initalVelocityX = launchPower * Mathf.Cos(launchAngleRad) * Mathf.Cos(launchDirectioneRad);
            float initalVelocityZ = launchPower * Mathf.Cos(launchAngleRad) * Mathf.Sin(launchDirectioneRad);
            float initalVelocityY = launchPower * Mathf.Sin(launchAngleRad);

            Vector3 initalVelocity = new Vector3(initalVelocityX, initalVelocityY, initalVelocityZ);

            //Rigidbody에 초기 속도를 적용
            rb.velocity = initalVelocity;
        }

    }
    void Update()
    {
        RenderTrajectory();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            LaunchProgectile(projectilePrefabs);
        }


    }
}
