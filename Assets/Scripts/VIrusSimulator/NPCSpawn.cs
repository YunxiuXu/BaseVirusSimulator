using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ground�ű�
public class NPCSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject NPC;
    public static int day;// ����
    public uint TicksPerDay = 60;//һ�켸֡
    private int ticks = 0;

    public int initSNum = 100, initENum = 0, initINum = 0, initRNum = 0;//SEIR��ʼ����'
    private int isStart = 0;

    public static int[] currentNum = new int[100];//infetState��Ӧ������ ͵�� �����һ�ٸ�������  currentNum[0]����S���� currentNum[1]����E����

    public int[] tmp = new int[100];

    public float EinfectRate = 1, IinfecRate = 1;//Ǳ���ںͷ����ڵĸ�Ⱦ��
    public static float staticEinfectRate, staticIinfecRate;

    public float EDay = 1, IDay = 1, RDay = -1;//EǱ���� I������ R�����߱�����-1����Ч �������������������� �����������ڳ������
    public static float staticEDay, staticIDay, staticRDay;

    public static int isMutateOn = 1;//�Ƿ�ͻ��
    void Start()
    {
        day = -3;//������һ��ʱ�� ��Ȼ�ճ���ȫ��
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

        //����������پ�һ����������
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
