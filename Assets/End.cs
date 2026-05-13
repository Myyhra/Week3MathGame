using UnityEngine;

public class End : MonoBehaviour
{
    [SerializeField] Transform player;
    public GameObject winUI;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 distance = player.position - transform.position;

        if(distance.magnitude <= 1f)
        {
            winUI.SetActive(true);
        }
    }
}
