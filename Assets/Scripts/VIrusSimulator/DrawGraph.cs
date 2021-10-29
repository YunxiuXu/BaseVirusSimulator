using System.Collections;
using System.Collections.Generic;
using ToucanSystems;
using UnityEngine;
using UnityEngine.UI;

public class DrawGraph : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SmartChart;
    SmartChart sc;
    private List<Vector2> chartDataListS, chartDataListE, chartDataListI, chartDataListR;

    public int updateCount = 30;//����ͼ���Ƶ��;
    int count = 0;

    int myTicks = 0;//�Լ���ticks ����̫�� count̫�컹������� ticksƽ��һ��

    public Text r0;
    void Start()
    {
        sc = SmartChart.GetComponent<SmartChart>();
        chartDataListS = new List<Vector2>();
        chartDataListE = new List<Vector2>();
        chartDataListI = new List<Vector2>();
        chartDataListR = new List<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if(count > updateCount)
        {
            myTicks++;
            count = 0;

            
            sc.minXValue = 0;
            sc.maxXValue = myTicks;
            sc.minYValue = 0;
            sc.maxYValue = NPCSpawn.currentNum[0] + NPCSpawn.currentNum[1] + NPCSpawn.currentNum[2] + NPCSpawn.currentNum[3];
            

            chartDataListS.Add(new Vector2(myTicks, NPCSpawn.currentNum[0]));
            sc.chartData[0].data = chartDataListS.ToArray();

            chartDataListE.Add(new Vector2(myTicks, NPCSpawn.currentNum[1]));
            sc.chartData[1].data = chartDataListE.ToArray();

            if (chartDataListE.Count > 1)
            {
                float R0 = chartDataListE[chartDataListE.Count - 1].y / chartDataListE[chartDataListE.Count - 2].y;
                if (chartDataListE[chartDataListE.Count - 2].y == 0)
                    R0 = 0;
                r0.text = "R0=" + R0.ToString("#0.00");
            }
                



            chartDataListI.Add(new Vector2(myTicks, NPCSpawn.currentNum[2]));
            sc.chartData[2].data = chartDataListI.ToArray();

            chartDataListR.Add(new Vector2(myTicks, NPCSpawn.currentNum[3]));
            sc.chartData[3].data = chartDataListR.ToArray();
            sc.SetupValues(true);
            sc.UpdateChart();

        }
    }
}
