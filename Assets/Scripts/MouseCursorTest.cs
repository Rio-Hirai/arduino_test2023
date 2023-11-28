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
        // マウスポジションを取得
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // オブジェクトに衝突した場合、衝突地点の座標を取得
            MousePoint = hit.point;
        }
    }
}
