using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Script : MonoBehaviour
{
    [SerializeField] 
    private GameObject player;
    
    private List<Vector3> TARGETS= new List<Vector3>{new Vector3(-12.5f, 9.5f, 0f), new Vector3(-7.5f, 9.5f, 0f), new Vector3(-7.5f, 13.5f, 0f), new Vector3(-12.5f, 13.5f, 0f)};
    private float DURATION_HORIZONTAL = 3;
    //private float DURATION_VERTICAL = 2;

    private Transform Target;
    private Vector3 StartPosition;
    private Vector3 EndPosition;
    private float StartTime;
    private float Duration;
    private int Direction;
    
    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
        EndPosition = TARGETS[0];
        StartPosition = player.transform.position;
        Direction = 0;
        Duration = DURATION_HORIZONTAL;

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, EndPosition) > 0.01f)
        { 
            player.transform.position = Vector3.Lerp(StartPosition, EndPosition, (Time.time-StartTime) / Duration);
        }
        
        if (Vector3.Distance(player.transform.position, EndPosition) <= 0.01f)
        { 
            player.transform.position = EndPosition;
            if (Direction != 3)
            {
                Direction++;
            }
            else
            {
                Direction = 0;
            }

            StartTime = Time.time;
            StartPosition = EndPosition;
            EndPosition = TARGETS[Direction];
            Debug.Log(EndPosition);
        }
        
        
    }
}
