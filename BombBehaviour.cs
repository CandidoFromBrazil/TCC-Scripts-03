using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    [SerializeField] float countdown = 2f;
    public int explosionSize = 2;

    [Header("Feedback")]
    [SerializeField] GameObject explosionEffect;

    [Header("Grid")]
    [SerializeField] SpawnGridCube grid;

    private void Start()
    {
        grid = FindObjectOfType<SpawnGridCube>();
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f)
        {
            ExplosionCentral();
            Explosion(explosionEffect, Vector3.forward);
            Explosion(explosionEffect, Vector3.back);
            Explosion(explosionEffect, Vector3.right);
            Explosion(explosionEffect, Vector3.left);

            Destroy(gameObject);
        }   
    }

    void ExplosionCentral()
    {
        //Explosão central
        GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
    }

    void Explosion(GameObject effect, Vector3 direction)
    {
        for (int i = 1; i < explosionSize; i++)
        {
            Vector3 explosionPos = transform.position + direction * grid.gridSpacingOffset * i;

            Debug.DrawRay(transform.position, explosionPos, Color.red);

            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, grid.gridSpacingOffset * i))
            {
                if(hit.transform.CompareTag("Player"))
                {
                    //Game Over
                }
                if(hit.transform.CompareTag("Blocks/Destructible"))
                {
                    Destroy(hit.transform.gameObject); 
                    i = explosionSize;
                }
                if(hit.transform.CompareTag("Blocks/Indestructible"))
                {
                    return;
                }
            }

            GameObject fx = Instantiate(effect, explosionPos, Quaternion.identity);
            fx.GetComponent<BoxCollider>().enabled = false;
            Destroy(fx, 2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Explosion"))
        {
            countdown = 0.2f;
        }
    }
}
