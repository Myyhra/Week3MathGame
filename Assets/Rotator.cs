using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Transform _target;
    public float coneSize = 30;
    void Start()
    {
        
    }

    void Update()
    {
        if(_target == null) return;
        var dir = _target.position - transform.position;

        var angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        var dot = Vector3.Dot(transform.right.normalized, dir.normalized);

        // var hit = transform.eulerAngles.z <= angle+coneSize && transform.eulerAngles.z >= angle - coneSize;

        var didhit = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z,angle)) <= coneSize;


        // transform.rotation = Quaternion.Slerp(transform.rotation,_target.rotation, Time.deltaTime);


        Debug.Log($"Angle: {angle}, Dot: {Mathf.Acos(dot)}, Hit?: {didhit}" );

        

        transform.rotation = Quaternion.Euler(0,angle,0);


    }
}
