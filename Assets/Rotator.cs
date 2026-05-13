using UnityEngine;

[RequireComponent(typeof(Turret_Detector))]
public class Rotator : MonoBehaviour
{
    public Transform _target;
    public Turret_Detector turret_Detector;

    void Awake()
    {
        turret_Detector = GetComponent<Turret_Detector>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(_target == null) return;
        


    }
}
