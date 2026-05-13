using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{

    public GameObject player;
    public Vector3 direction;
    public float speed;
    public float getHitRange = 1f;
    void Start()
    {
        
    }

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position += direction * (speed * Time.deltaTime);
        HitPlayer();
    }

    void HitPlayer()
    {
        if(player == null) return;

        var dir = player.transform.position - transform.position;

        if(dir.magnitude <= getHitRange)
        {
            Debug.Log("Player Hit");
            int current = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(current);
        }
    }
}
