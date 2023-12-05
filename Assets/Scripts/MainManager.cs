using System.Collections;
using System.Collections.Generic;
using Uduino;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MainManager : MonoBehaviour
{
    UduinoManager ud; // The instance of Uduino is initialized here

    [Range(0, 255)]
    public int blinkpower_11 = 100;
    [Range(0, 255)]
    public int blinkpower_10 = 100;
    [Range(0, 255)]
    public int blinkpower_9 = 100;
    [Range(0, 255)]
    public int blinkpower_6 = 100;

    [Range(97, 255)]
    public int balance = 100;
    public float distance = 0;

    public RaycastHit hit;
    public int Target_Type;
    public int Target_direction;

    public int basevalue;
    public int rebalance = 0;
    public bool reverse = false;

    public int maxvalue = 255;
    public int minvalue = 97;

    public float mindistance = 0;
    public float maxdistance = 0;
    public float maxdistance2 = 0; //Type2‚É‚¨‚¯‚éÅ‘å‹——£
    public float mindistance2 = 0; //Type2‚É‚¨‚¯‚éÅ¬‹——£

    // “à•”ˆ——p‚Ì•Ï”
    private float tmp_mindistance;  //ŒvZ‚É—p‚¢‚éÅ¬‹——£‚ÌˆêŠi”[—p
    private float tmp_maxdistance;  //ŒvZ‚É—p‚¢‚éÅ‘å‹——£‚ÌˆêŠi”[—p

    void Start()
    {
        tmp_maxdistance = maxdistance;
        tmp_mindistance = mindistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (hit.collider != null)
        {
            Target_Type = hit.collider.GetComponent<Target_para>().TargetType;
        }
        if (Target_Type == 1)
        {
            tmp_maxdistance = maxdistance;
            tmp_mindistance = mindistance;

            PowerSetting();
        }
        else if (Target_Type == 2)
        {
            maxdistance2 = hit.collider.transform.localScale.x / 2;
            mindistance2 = 0;
            tmp_maxdistance = hit.collider.transform.localScale.x / 2;
            tmp_mindistance = 0;

            PowerSetting();
        }

        blinkpower_11 = basevalue;
        blinkpower_10 = rebalance;
        blinkpower_9  = basevalue;
        blinkpower_6  = rebalance;
        //UduinoManager.Instance.analogWrite(11, basevalue);
        //UduinoManager.Instance.analogWrite(10, rebalance);
    }

    void PowerSetting()
    {
        basevalue = CalculateA(distance);
        if (basevalue > maxvalue)
        {
            basevalue = maxvalue;
        }
        else if (basevalue < minvalue)
        {
            basevalue = minvalue;
        }
        if (reverse)
        {
            rebalance = basevalue;
            basevalue = CalculateB(basevalue);
        }
        else
        {
            rebalance = CalculateB(basevalue);
        }

        if (distance == 999)
        {
            basevalue = minvalue;
            rebalance = minvalue;
        }
    }

    int CalculateA(float A)
    {
        float m = (maxvalue - minvalue) / (tmp_maxdistance - tmp_mindistance);
        return (int)((A - tmp_mindistance) * m + minvalue);
    }

    int CalculateB(int A)
    {
        return (maxvalue - A) + minvalue;
    }
}
