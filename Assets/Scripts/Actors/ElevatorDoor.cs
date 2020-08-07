using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    public ParticleSystem doorBreak;
    private int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
    
    public void OnEnemyDeath()
    {
        enemyCount--;
        if(enemyCount == 0)
        {
            Destroy(Instantiate(doorBreak.gameObject, transform.position, Quaternion.identity) as GameObject, doorBreak.main.startLifetimeMultiplier);
            Destroy(gameObject);
        }
    }
}
