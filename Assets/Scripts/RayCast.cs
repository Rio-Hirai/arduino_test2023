using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RayCast : MonoBehaviour
{
    [SerializeField] private MainManager Server;
    public Camera MainCamera;
    public Vector3 MousePoint = Vector3.zero;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Gradient _gradient;
    public int RayDistance;

    public GameObject Camera;
    public GameObject RayPoint;

    private void Start()
    {
        lineRenderer.colorGradient = _gradient;
    }

    private void Update()
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

        // レイの開始点と終了点を設定
        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(1, ray.origin + ray.direction * RayDistance); // 100はレイの長さ

        // ここでレイキャストを使用して何かをする
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // オブジェクトに衝突した場合、衝突地点の座標を取得
            MousePoint = hit.point;

            if (hit.collider.GetComponent<Target_para>().TargetType == 2)
            {
                Server.distance = DistanceFromRay(ray, hit.collider.transform.position);
            }
            else
            {
                Server.distance = Vector3.Distance(Camera.transform.position, RayPoint.transform.position);
            }
            // Server.Target_Type = hit.collider.GetComponent<Target_para>().TargetType;
            Server.hit = hit;
            DetermineRaySide(ray);

            // レイが何かにヒットした場合の処理
            Debug.Log("Hit: " + hit.collider.name);
        }
        else
        {
            MousePoint = Vector3.zero;
            Server.distance = 999;
        }
        RayPoint.transform.position = MousePoint;

    }

    float DistanceFromRay(Ray ray, Vector3 point)
    {
        // レイの始点からターゲットへのベクトル
        Vector3 vectorToTarget = point - ray.origin;

        // レイの方向ベクトルとターゲットへのベクトルの外積
        Vector3 crossProduct = Vector3.Cross(ray.direction, vectorToTarget);

        // 外積の長さは、レイとターゲット間の平行四辺形の面積
        // この面積をレイの方向ベクトルの長さ（1）で割ると、最短距離が得られる
        return crossProduct.magnitude / ray.direction.magnitude;
    }

    void DetermineRaySide(Ray ray)
    {
        Vector3 cameraRight = MainCamera.transform.right;
        Vector3 rayDirection = ray.direction;

        // 外積を計算
        float crossProductY = Vector3.Cross(cameraRight, rayDirection).y;

        if (crossProductY > 0)
        {
            Server.Target_direction = 1;
        }
        else
        {
            Server.Target_direction = -1;
        }
    }
}