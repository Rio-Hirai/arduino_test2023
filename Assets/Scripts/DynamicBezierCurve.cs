using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBezierCurve : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform pointA; // 始点A
    public Transform pointB; // 中間点B
    public Transform pointBPrime; // 終点B'
    public int numPoints = 50; // カーブに使用するポイントの数
    public Vector3[] positions; // ポイントの位置を格納する配列
    public float lineWidth = 0.1f; // レイの太さ
    public Color lineColor = Color.white; // レイの色

    void Start()
    {
        positions = new Vector3[numPoints];
        SetupLineRenderer();
        DrawBezierCurve();
    }

    void Update()
    {
        DrawBezierCurve();
    }

    void SetupLineRenderer()
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
    }

    void DrawBezierCurve()
    {
        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (float)numPoints;
            positions[i] = CalculateQuadraticBezierPoint(t, pointA.position, pointB.position, pointBPrime.position);
        }

        lineRenderer.positionCount = numPoints;
        lineRenderer.SetPositions(positions);
    }

    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // 2次ベジェ曲線の計算式
        return Mathf.Pow(1 - t, 2) * p0 + 2 * (1 - t) * t * p1 + Mathf.Pow(t, 2) * p2;
    }
}
