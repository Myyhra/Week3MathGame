using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Turret_Shotgun : Turret_Detector
{
    [Header("Shotgun Settings")]
    public int numberOfProjectiles;
    public float projectileSpeed;
    public float shotgunSize;
    public Bullet bullet;

    bool isShooting;
    public float shootingInterval = 1f;
    private const float radius = 1f;
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    void Update()
    {
        if(_target == null) return;
        DetectionArea();
        Shoot();
    }

    void Shoot()
    {
        if(isPlayerInRange && !isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootDelay());
        }
    }

    void Shotgun()
    {
       Vector3 targetDir = (_target.position - transform.position).normalized;

       float centerTowardsPlayer = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;

       float startAngle = centerTowardsPlayer - shotgunSize / 2f;
       float angleStep = shotgunSize / (numberOfProjectiles - 1);

       for(int i =0; i < numberOfProjectiles; i++)
        {
            float currentAngle = startAngle + (angleStep * i);

            float rad = currentAngle * Mathf.Deg2Rad;

            Vector3 dir = new Vector3(Mathf.Sin(rad ), 0f, Mathf.Cos(rad)).normalized;
            Bullet b = Instantiate(bullet, transform.position + dir * radius, Quaternion.identity);
            b.direction = dir;
            b.speed = projectileSpeed;
        }

    }
    IEnumerator ShootDelay()
    {
        Shotgun();
        yield return new WaitForSecondsRealtime(shootingInterval);
        isShooting = false;
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
