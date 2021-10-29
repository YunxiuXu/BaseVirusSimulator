using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ground脚本
public class NPCSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject NPC;
    public static int day;// 天数
    public uint TicksPerDay = 60;//一天几帧
    private int ticks = 0;

    public int initSNum = 100, initENum = 0, initINum = 0, initRNum = 0;//SEIR初始数量'
    private int isStart = 0;

    public static int[] currentNum = new int[100];//infetState对应的数量 偷懒 这里放一百个够用了  currentNum[0]就是S人数 currentNum[1]就是E人数

    public int[] tmp = new int[100];

    public float EinfectRate = 1, IinfecRate = 1;//潜伏期和发病期的感染率
    public static float staticEinfectRate, staticIinfecRate;

    public float EDay = 1, IDay = 1, RDay = -1;//E潜伏期 I发病期 R是免疫保持期-1不生效 这两个用来在外面输入 下面两个用于程序调用
    public static float staticEDay, staticIDay, staticRDay;

    public static int isMutateOn = 1;//是否突变
    void Start()
    {
        day = -3;//给生成一段时间 不然刚出生全碰
        staticEDay = EDay;
        staticIDay = IDay;
        staticRDay = RDay;

        staticEinfectRate = EinfectRate;
        staticIinfecRate = IinfecRate;
    }

    // Update is called once per frame
    void Update()
    {
        ticks++;
        if (ticks > TicksPerDay) {
            ticks = 0;
            day++;

            tmp = currentNum;
        }

/*        if (currentCount < initSNum) {
            currentCount++;
            GameObject SNPC =  Instantiate(NPC, this.transform.position, this.transform.rotation);
            SNPC.name = "S";
        }*/

        //下面的数量少就一次性生成了
        if (isStart == 0) {


            for (int i = 0; i < initSNum; i++)
            {
                GameObject SNPC = Instantiate(NPC, this.transform.position, this.transform.rotation);
                SNPC.transform.parent = this.transform;
                SNPC.name = "S";
                SNPC.GetComponent<NPCMain>().infectState = 0;
            }

            for (int i = 0; i < initENum; i++)
            {
                GameObject ENPC = Instantiate(NPC, this.transform.position, this.transform.rotation);
                ENPC.transform.parent = this.transform;
                ENPC.name = "E";
                ENPC.GetComponent<NPCMain>().infectState = 1;
            }

            for (int i = 0; i < initINum; i++)
            {
                GameObject INPC = Instantiate(NPC, this.transform.position, this.transform.rotation);
                INPC.transform.parent = this.transform;
                INPC.name = "I";
                INPC.GetComponent<NPCMain>().infectState = 2;
            }

            for (int i = 0; i < initRNum; i++)
            {
                GameObject RNPC = Instantiate(NPC, this.transform.position, this.transform.rotation);
                RNPC.transform.parent = this.transform;
                RNPC.name = "R";
                RNPC.GetComponent<NPCMain>().infectState = 3;
            }

            isStart = 1;
        }
        

    }
}
