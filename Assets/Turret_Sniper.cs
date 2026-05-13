using UnityEngine;
using System.Collections;


public class Turret_Sniper : Turret_Detector
{

    [Header("Shooting Settings")]
    public float projectileSpeed;
    public float shootInterval = 1f;
    float angle;
    float firingAngle;
    Vector3 targetDir;
    public Bullet bullet;

    bool isShooting;

    void Start()
    {
        isShooting = false;
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(_target == null) return;
        DetectionArea();
        StartShooting();

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

    void StartShooting()
    {
        if(isPlayerInRange && !isShooting)
        {
            isShooting = true;
            SniperShoot();
        }
    }

    void SniperShoot()
    {
        
        Debug.Log($"isShooting");
        var targetDir = (_target.position - transform.position).normalized;    
        StartCoroutine(ShootDelay(targetDir));
        
    }
    IEnumerator ShootDelay(Vector3 dir)
    {
        Bullet b = Instantiate(bullet, transform.position, Quaternion.identity);
        b.speed = projectileSpeed;
        b.direction = dir;
        yield return new WaitForSecondsRealtime(shootInterval);
        isShooting = false;
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
