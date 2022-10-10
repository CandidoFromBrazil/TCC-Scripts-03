using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBomb : MonoBehaviour
{
    [SerializeField] GameObject prefabBomb;
    [SerializeField] float posSpawnY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, new Vector3(0, -1, 0), Color.red);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            InstantiateBomb();
        }
    }

    public void InstantiateBomb()
    {
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out RaycastHit hit, 0.2f))
        {
            GameObject objBomb = Instantiate(
                prefabBomb,
                new Vector3(
                    hit.transform.position.x,
                    hit.transform.position.y + posSpawnY,
                    hit.transform.position.z),
                Quaternion.identity);
        }
    }
}
