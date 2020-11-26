using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    private Transform player;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager == null) gameManager = GameManager.instance;

        if (!gameManager.isMagnetActive) return;

        if (Vector3.Distance(transform.position, player.position) < gameManager.magnetDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            transform.position += direction * gameManager.magnetSpeed;
        }
    }
}
