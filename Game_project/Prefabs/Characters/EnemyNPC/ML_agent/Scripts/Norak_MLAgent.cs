
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class Norak_MLAgent : Agent
{
    private Animator _animator;

    TrackCheckPoint trackCheckPoint;
    GameObject _player;
    CheckpointSingle checkpointSingle;

    Vector3 directionToPlayer;
    Vector3 NorakPosition;
    Quaternion NorakRot;
    
    float timer;
    bool playerIsCloseToTheEnemy = false;
    bool trigOnAttack;
    float dist;

    private List<CheckpointSingle> checkpointList;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        //checkpointSingle = GetComponent<CheckpointSingle>();
        trackCheckPoint = FindObjectOfType<TrackCheckPoint>();
        checkpointSingle = FindObjectOfType<CheckpointSingle>();
        _player = GameObject.Find("Paladin 1");

        NorakPosition = transform.localPosition;
        NorakRot = transform.localRotation;

    }

    private void Start()
    {
        trackCheckPoint.OnPlayerCorrectCheckpoint += TrackCheckpoint_OnCarCorrectCheckpoint;
        trackCheckPoint.OnPlayerWrongCheckpoint += TrackCheckpoint_OnCarWrongCheckpoint;

        trackCheckPoint.countCheck = 0;
    }

    void TrackCheckpoint_OnCarCorrectCheckpoint()// object sender, EventArgs e)
    {
        //Debug.Log("������� ���������!");
        AddReward(+1);
    }

    void TrackCheckpoint_OnCarWrongCheckpoint()
    {
        //Debug.Log("������� ��������� ������� !");
        AddReward(-1);
        //EndEpisode();
    }


    float DistanceBetweenPlayerAndEnemy()  
    {
        return Vector3.Distance(transform.position, _player.transform.position);
    } // ��������� ����� ������� � ������

    public override void OnEpisodeBegin() // ������ ������ �������
    {
        transform.localPosition = NorakPosition;
        trackCheckPoint.nextCheckpointSingleIndex = 0;
        transform.localRotation = NorakRot;
        }

    /// <summary>
    /// ��������� � �� �������. ��� ������ �� ����� �� ���������. ��� ����� ������ ������ 
    /// ����� �� ���������� ��� ��������, � ���� ��� �� ������, �� ����, �� ����. 
    /// ��, ��� �� ����� "������" � ��� ��, ��� �� ������� ����� CollectObservations().
    /// </summary>

    public override void CollectObservations(VectorSensor sensor)  //������� ����� ����������, ����� ������
    {
       // float dist = DistanceBetweenPlayerAndEnemy();
        
        //sensor.AddObservation(dist);
        //Debug.Log(DistanceBetweenPlayerAndEnemy());

        Vector3 CheckpointForward = trackCheckPoint.GetNextCheckpoint(trackCheckPoint.countCheck).transform.forward;
        float directionPoint = Vector3.Dot(transform.forward, CheckpointForward);
        sensor.AddObservation(directionPoint);

        Vector3 directionToCheckpoint = (trackCheckPoint.GetNextCheckpoint(trackCheckPoint.countCheck).transform.position - transform.position).normalized;
        
        sensor.AddObservation(directionToCheckpoint); // ��������� ����������� �� ����

        sensor.AddObservation(transform.localPosition); //3 
    }



    /// <summary>
    /// ��� ������: ����� �������������� ��������, ������� ��������� "������" ������.�� ���������� ��� �������� � �������(�������� ���, �������� � �.�.)
    ///  ����� ����� �������� ������� � ������� ���������� �������.
    /// </summary>
    // ���������� ����� ML agent ����� ��������� ��������
    public override void OnActionReceived(ActionBuffers actions)   // �������� �������� �� ������ � ��������� �� � ������. , ���� � ���� ������
    {

        float forwardAmount = 0f;
        float turnAmount = 0f;
        dist = DistanceBetweenPlayerAndEnemy();

        if (dist < 20.0f)
        {
            
            //playerIsCloseToTheEnemy = false;
            _animator.SetBool("isRunning", false);
            //follow foward palyer
            directionToPlayer = (_player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToPlayer), Time.deltaTime * 5f);

            timer += Time.deltaTime;
            if (timer > 2.0f)
            {
                if (!playerIsCloseToTheEnemy)
                {
                    GoTOPlayer();
                }
                if (dist < 2f)
                {
                    playerIsCloseToTheEnemy = true;
                    
                    if (!trigOnAttack)
                    {
                        
                        StartCoroutine(atack());
                        trigOnAttack = true;

                    }

                    AddReward(+2);
                }
                
            }
        }
        else if (dist >= 20.0f)
        {
            timer = 0f;
            //playerIsCloseToTheEnemy = true;
            _animator.SetBool("isRunning", true);

            switch (actions.DiscreteActions[0])
            {
                case 0: forwardAmount =  0f; break;
                case 1: forwardAmount = +1f; break;
                case 2: forwardAmount = -1f; break;
            }
            switch (actions.DiscreteActions[1])
            {
                case 0: turnAmount =  0f; break;
                case 1: turnAmount = +1f; break;
                case 2: turnAmount = -1f; break;
            }

            Vector3 directionToCheckpoint = (trackCheckPoint.GetNextCheckpoint(trackCheckPoint.countCheck).transform.position - transform.position).normalized;

            // ������� ������ �� ����������� � ���������
            transform.localPosition += directionToCheckpoint * 5f * forwardAmount * Time.deltaTime;

            // ������� ������� � ���������
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToCheckpoint), Time.deltaTime * 5f ); // 5f � �������� ��������
        }
    }

  void GoTOPlayer()
    {
        // ���� ����� � ������
        transform.localPosition += directionToPlayer * 5f * Time.deltaTime;      
    }

    IEnumerator atack()
    {
        _animator.SetTrigger("isAttack");
        Debug.Log("after 1 sec");
        yield return new WaitForSeconds(1);
        timer = 0f;
        Debug.Log("before 1 sec");
        
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {


        int forwardAction = 0;
        if (Input.GetKey(KeyCode.W)) forwardAction = 1;
        if (Input.GetKey(KeyCode.S)) forwardAction = 2;

        int turnAction = 0;
        if (Input.GetKey(KeyCode.D)) turnAction = 1;
        if (Input.GetKey(KeyCode.A)) turnAction = 2;

        ActionSegment<int> discreteAction = actionsOut.DiscreteActions;
        discreteAction[0] = forwardAction;
        discreteAction[1] = turnAction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CheckpointSingle>(out CheckpointSingle checkpointSingle))
        {

        }
        else if (other.TryGetComponent<Wall>(out Wall wall))
        {
            //backGround.material.color = Color.red;
            AddReward(-1f);

            Debug.Log("��������� �����");
            
            //trackCheckPoint.NorakThroughtCheckpoint(checkpoint);

        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            
            
            Debug.Log(timer);
            //Debug.Log("ctena");
            AddReward(-1f);
        }
    }
}
