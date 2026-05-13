using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += move * (speed * Time.deltaTime);

        if(move.magnitude >1)
        {
            move.Normalize();
        }
    }
}
