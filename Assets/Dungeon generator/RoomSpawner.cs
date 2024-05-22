using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 --> need room with bottom door
    // 2 --> need room with top door
    // 3 --> need room with left door
    // 4 --> need room with right door

    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent <RoomTemplates>();
        Invoke("Spawn",0.1f);
    }

    void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                // Need to spawn a room with a B door
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 2)
            {
                //Need to spawn a room with a T door
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 3)
            {
                //Need to spawn a room with a L door
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 4)
            {
                //Need to spawn a room with a R door
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
            }
            spawned = true;
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("SpawnPoint") && collision.GetComponent<RoomSpawner>().spawned == true)
        {
            Destroy(gameObject);
        }else if(collision.CompareTag("SpawnPoint") && collision.GetComponent<RoomSpawner>().spawned == false)
        {
            if (openingDirection >= collision.GetComponent<RoomSpawner>().openingDirection)
            {
                Destroy(gameObject);
                Debug.Log("JAG DÖDADE ETT KORSNINGSRUM!");
            }
        }else if (collision.CompareTag("OpenRoom"))
        {
            Destroy(gameObject);
        }
    }
}
