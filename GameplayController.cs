using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameplayController : MonoBehaviour
{

    [SerializeField] GameObject myPlayer;
    public List<Transform> spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        int i = Random.Range(0, spawnPoints.Count);

        PhotonNetwork.Instantiate(myPlayer.name, spawnPoints[i].position, spawnPoints[i].rotation, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
