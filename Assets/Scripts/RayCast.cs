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

        // ���C�̊J�n�_�ƏI���_��ݒ�
        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(1, ray.origin + ray.direction * RayDistance); // 100�̓��C�̒���

        // �����Ń��C�L���X�g���g�p���ĉ���������
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // �I�u�W�F�N�g�ɏՓ˂����ꍇ�A�Փ˒n�_�̍��W���擾
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

            // ���C�������Ƀq�b�g�����ꍇ�̏���
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
        // ���C�̎n�_����^�[�Q�b�g�ւ̃x�N�g��
        Vector3 vectorToTarget = point - ray.origin;

        // ���C�̕����x�N�g���ƃ^�[�Q�b�g�ւ̃x�N�g���̊O��
        Vector3 crossProduct = Vector3.Cross(ray.direction, vectorToTarget);

        // �O�ς̒����́A���C�ƃ^�[�Q�b�g�Ԃ̕��s�l�ӌ`�̖ʐ�
        // ���̖ʐς����C�̕����x�N�g���̒����i1�j�Ŋ���ƁA�ŒZ������������
        return crossProduct.magnitude / ray.direction.magnitude;
    }

    void DetermineRaySide(Ray ray)
    {
        Vector3 cameraRight = MainCamera.transform.right;
        Vector3 rayDirection = ray.direction;

        // �O�ς��v�Z
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