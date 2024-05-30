using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerFSM : MonoBehaviour
{
    public CustomerState currentState;
    private Timer timer;
    private NavMeshAgent agent;
    public bool isMoveDone = false;


    public Transform target;

    //¿Âº“
    public Transform counter;
    public List<GameObject> targetPos = new List<GameObject>();
 

    public List<GameObject> myBox = new List<GameObject>();

    private static int nextpriority = 0;
    private static readonly object priorityLock = new object();

    public int boxesToPick = 5;
    private int boxesPicked = 0;
    void Start()
    {
        timer = new Timer();
        agent = GetComponent<NavMeshAgent>();
        AssignPriority();
        currentState = CustomerState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        timer.Updete(Time.deltaTime);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                isMoveDone = true;
            }
        }

        switch (currentState)
        {
            case CustomerState.Idle:
                Idle();
                break;
            case CustomerState.WalkingToShalf:
                WalkingToShalf();
                break;
            case CustomerState.PickingItem:
                PickingItem();
                break;
            case CustomerState.WalkingToCounter:
                WalkingToCounter();
                break;
            case CustomerState.PlacingItem:
                PlacingItem();
                break;
        }
    }

    void ChangeState(CustomerState nextState, float waitTime = 0.0f)
    {
        currentState = nextState;
        timer.Set(waitTime);
    }
    void Idle()
    {
        if (timer.IsFinished())
        {
            target = targetPos[Random.Range(0, targetPos.Count)].transform;
            MoveToTarget();
            ChangeState(CustomerState.WalkingToShalf, 2.0f);
        }
    }
    void WalkingToShalf()
    {
        if (timer.IsFinished() && isMoveDone)
        {
            ChangeState(CustomerState.PickingItem, 2.0f);
        }
    }
    void PickingItem()
    {
        if (timer.IsFinished())
        {
            if(boxesPicked < boxesToPick)
            {

                GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                myBox.Add(box);
                box.transform.parent = gameObject.transform;
                box.transform.localEulerAngles = Vector3.zero;
                box.transform.localPosition = new Vector3(0, boxesPicked * 2f, 0);

                boxesPicked++;
                timer.Set(0.5f);
            }

            else
            {
                target = counter;
                MoveToTarget();
                ChangeState(CustomerState.WalkingToCounter, 2.0f);
            }

        }
    }

    void WalkingToCounter()
    {
        if (timer.IsFinished())
        {
            ChangeState(CustomerState.PlacingItem, 2.0f);
        }
    }
    void PlacingItem()
    {
        if (timer.IsFinished())
        {
            if(myBox.Count != 0)
            {
                myBox[0].transform.position = counter.transform.position;
                myBox[0].transform.parent = counter.transform;
                myBox.RemoveAt(0);

                timer.Set(0.1f);
            }
            else
            {
                ChangeState(CustomerState.Idle, 2.0f);
            }

        }
    }
    void AssignPriority()
    {
        lock(priorityLock)
        {
            agent.avoidancePriority = nextpriority;
            nextpriority = (nextpriority + 1) % 100;
        }
    }
    void MoveToTarget()
    {
        isMoveDone = false;

        if(target != null)
        {
            agent.SetDestination(target.position);
        }
    }

}
