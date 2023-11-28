using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorTest : MonoBehaviour
{
    public int Target_Type;
    public Vector3 MousePoint = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �}�E�X�|�W�V�������擾
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // �I�u�W�F�N�g�ɏՓ˂����ꍇ�A�Փ˒n�_�̍��W���擾
            MousePoint = hit.point;
        }
    }
}
