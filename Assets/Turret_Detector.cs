using System.Collections;
using UnityEngine;

public class Turret_Detector : MonoBehaviour
{
    [Header("Detection Settings")]
    public Transform _target;
    public float coneSize = 30;
    public float rangeDistance = 10;
    public bool isPlayerInRange;



    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(_target == null) return;
        DetectionArea();
    }

    void GetDistance(bool didhit, float distance)
    {
        if(didhit && distance <= rangeDistance)
        {
            isPlayerInRange = true;
            Debug.Log($"In Range of {gameObject.name}");
        }
        else
        {
            isPlayerInRange = false;
            Debug.Log("Not In Range");
        }
       
    }


    void DetectionArea()
    {
        var dir = _target.position - transform.position;

        var angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        var dot = Vector3.Dot(transform.forward.normalized, dir.normalized);

        // var hit = transform.eulerAngles.z <= angle+coneSize && transform.eulerAngles.z >= angle - coneSize;

        bool didhit = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y,angle)) <= coneSize;

        GetDistance(didhit, dir.magnitude);


        // transform.rotation = Quaternion.Slerp(transform.rotation,_target.rotation, Time.deltaTime);


        Debug.Log($"Turret: {gameObject.name}, Angle: {angle}, Dot: {Mathf.Acos(dot)}, Hit?: {didhit}" );
    }
}
