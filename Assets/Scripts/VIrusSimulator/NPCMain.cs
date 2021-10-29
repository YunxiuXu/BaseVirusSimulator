using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMain : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    public GameObject plane;
    //public float ChangeDestTime = 180;//

    public int infectState = 0;//0�׸� 1Ǳ�� 2��Ⱦ 3����
    private int lastInfectState = 0, infectDay = -1;//infecDay����Ⱦ���� �����Զ�������һ�׶� -1������
    Vector3 destination;

    public int isIsolate = 0;
    public GameObject isolateZone;
    private float initSpeed;
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        initSpeed = agent.speed;
        agent.SetDestination(GenerateRandomPosition(5, 5));
        destination = GenerateRandomPosition(plane.transform.localScale.x, plane.transform.localScale.z) + plane.transform.position;
        agent.SetDestination(destination);
        changeColorAndName(-1, infectState);

        if (infectState == 1 || infectState == 2)
            infectDay = 0;
    }

    // Update is called once per frame
    //int count = 0;
    void Update()
    {
        if (infectState == 2 && isIsolate == 1)
            agent.SetDestination(isolateZone.transform.position);

        if (Vector3.Distance(this.transform.position, destination) < 0.3f)
        {
            agent.speed = initSpeed;//�ָ���ʼ�ٶ�
            destination = GenerateRandomPosition(plane.transform.localScale.x, plane.transform.localScale.z) + plane.transform.position;
            agent.SetDestination(destination);

        }
        changeState();
        /*        if (count > ChangeDestTime)
                {
                    agent.SetDestination(GenerateRandomPosition(plane.transform.localScale.x, plane.transform.localScale.z));
                    count = 0;
                }

                count++;*/

    }

    //Plane��5*5��
    Vector3 GenerateRandomPosition(float xScale, float zScale) {
        float x = Random.Range(-xScale * 5f, xScale * 5f);
        float z = Random.Range(-zScale * 5f, zScale * 5f);

        return new Vector3(x, 0, z);
    }

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "NPC" && NPCSpawn.day > 0) //day>��ʱ��ſ�ʼ��Ⱦ ���⿪ʼ��ʱ��
        {
            if (infectState == 2)
            {//����Ǵ�ȾԴ
                float rand = Random.Range(0, 1000);
                if(rand / 1000 < NPCSpawn.staticIinfecRate)
                    collider.gameObject.SendMessage("inf");
            }
            if (infectState == 1)
            {//����Ǵ�ȾԴ
                float rand = Random.Range(0, 1000);
                if (rand / 1000 < NPCSpawn.staticEinfectRate)
                    collider.gameObject.SendMessage("inf");
            }

        }
    }

    void inf() {
        if (infectState == 0 || infectState == 1) {
            infectState++;
            changeColorAndName(lastInfectState, infectState);
            infectDay = NPCSpawn.day;
        }
            
    }

    void changeState() { //Ǳ���ͷ����䵽��һ��״̬
        if (infectDay == -1)
            return;
        if (infectState == 1) {//
            if (infectDay + NPCSpawn.staticEDay <= NPCSpawn.day) {
                infectState++;
                changeColorAndName(lastInfectState, infectState);
                infectDay = NPCSpawn.day;
                return;
            }

        }
        else if (infectState == 2)
        {//
            if (infectDay + NPCSpawn.staticIDay <= NPCSpawn.day)
            {
                infectState++;
                changeColorAndName(lastInfectState, infectState);
                infectDay = NPCSpawn.day;
                return;
            }

        }
        else if (infectState == 3 && NPCSpawn.staticRDay != -1)//����������ʧ��
        {//
            if (infectDay + NPCSpawn.staticRDay <= NPCSpawn.day)
            {
                infectState = 0;
                changeColorAndName(lastInfectState, infectState);
                infectDay = -1;
                return;
            }

        }
    }

    void changeColorAndName(int lastInfState, int newInfectState){
        if (infectState == 0)
            this.gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", new Color(0, 159f/255f, 11f/ 255f));
        else if (infectState == 1)
            this.gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", new Color(227f / 255f, 127f / 255f, 0f / 255f));
        else if (infectState == 2)
            this.gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
        else if (infectState == 3)
            this.gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", new Color(0 / 255f, 149f / 255f, 226f / 255f));

        if (infectState == 0)
            this.name = "S";
        else if (infectState == 1)
            this.name = "E";
        else if (infectState == 2)
            this.name = "I";
        else if (infectState == 3)
            this.name = "R";

        if(lastInfState != -1)//��ʼ ֻ�Ӳ���
            NPCSpawn.currentNum[lastInfState]--;
        NPCSpawn.currentNum[newInfectState]++;
        lastInfectState = newInfectState;

        if (isIsolate == 1)
        {//�������������
            if (infectState == 2)
            {//����Ƿ��� ���ܵ�������
                agent.speed = 15;//ȥ�������ٶ�
                destination = isolateZone.transform.position;
                agent.SetDestination(isolateZone.transform.position);
            }
            else
            {
                destination = GenerateRandomPosition(plane.transform.localScale.x, plane.transform.localScale.z) + plane.transform.position;
                agent.SetDestination(destination);

            }

            if (infectState == 3)//��Ժ˲��Ҳ����
                agent.speed = 15;
        }
    }

}
