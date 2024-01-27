using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] [Range(1, 20)] float engageRange;
    [SerializeField] [Range(0.1f, 20)] float speed;
    [SerializeField] bool showCheckZones = false;

    private GameObject player;
    private Rigidbody2D enemyRigidbody;
    public bool enemyAgressive;

    [SerializeField] private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < engageRange)
        {
            enemyAgressive = true;
            enemyRigidbody.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime));
        }
        else
        {
            enemyAgressive = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (showCheckZones)
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, engageRange);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Game Over");
            gameManager.GameOver();
        }
    }
}
