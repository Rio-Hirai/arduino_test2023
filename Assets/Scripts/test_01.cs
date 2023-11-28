using System.Collections;
using System.Collections.Generic;
using Uduino;
using UnityEngine;

public class test_01 : MonoBehaviour
{
    UduinoManager ud; // The instance of Uduino is initialized here
    [Range(0, 5)]
    public float blinkSpeed = 1;
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
    [Range(1, 6)]
    public float distance = 0;

    public int rebalance = 0;
    public int basevalue;
    public bool reverse = false;

    public int maxvalue = 255;
    public int minvalue = 97;

    public float mindistance = 0;
    public float maxdistance = 0;

    void Start()
    {
        //StartCoroutine(BlinkLoop());
    }

    IEnumerator BlinkLoop()
    {
        while (true)
        {
            UduinoManager.Instance.analogWrite(11, blinkpower_11);
            yield return new WaitForSeconds(blinkSpeed);
            UduinoManager.Instance.analogWrite(11, blinkpower_11);
            yield return new WaitForSeconds(blinkSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //UduinoManager.Instance.analogWrite(11, blinkpower_11);
        //UduinoManager.Instance.analogWrite(10, blinkpower_10);
        //UduinoManager.Instance.analogWrite(9, blinkpower_9);
        //UduinoManager.Instance.analogWrite(6, blinkpower_6);

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
        UduinoManager.Instance.analogWrite(11, basevalue);
        UduinoManager.Instance.analogWrite(10, rebalance);
    }

    int CalculateA(float A)
    {
        float m = (maxvalue - minvalue) / (maxdistance - mindistance);
        return (int)((A - mindistance) * m + minvalue);
    }

    int CalculateB(int A)
    {
        return (maxvalue - A) + minvalue;
    }
}
