using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBezierCurve : MonoBehaviour
{
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
    public LineRenderer lineRenderer;
    public Transform pointA; // �n�_A
    public Transform pointB; // ���ԓ_B
    public Transform pointBPrime; // �I�_B'
    public int numPoints = 50; // �J�[�u�Ɏg�p����|�C���g�̐�
    public Vector3[] positions; // �|�C���g�̈ʒu���i�[����z��
<<<<<<< Updated upstream
    public float lineWidth = 0.1f; // ���C�̑���
    public Color lineColor = Color.white; // ���C�̐F
=======
>>>>>>> Stashed changes

    void Start()
    {
        positions = new Vector3[numPoints];
<<<<<<< Updated upstream
        SetupLineRenderer();
=======
>>>>>>> Stashed changes
        DrawBezierCurve();
    }

    void Update()
    {
        DrawBezierCurve();
    }

<<<<<<< Updated upstream
    void SetupLineRenderer()
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
    }

=======
>>>>>>> Stashed changes
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
        // 2���x�W�F�Ȑ��̌v�Z��
        return Mathf.Pow(1 - t, 2) * p0 + 2 * (1 - t) * t * p1 + Mathf.Pow(t, 2) * p2;
    }
}
