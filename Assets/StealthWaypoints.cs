using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class StealthWaypoints : NetworkBehaviour{

    GameObject[] players;
    public GameObject[] waypoints = new GameObject[5];
    public int currentWaypoint = 0;
    public float speed = 2f;
    public float detectRate = 1f;

	// Use this for initialization
	void Start () {
	 
	}
	
	// Update is called once per frame
	void Update () {
        if (isServer)
        {
            players = GameObject.FindGameObjectsWithTag("player");
            foreach(GameObject g in players){
                if (Vector3.Angle(this.transform.forward, g.transform.position) < 50f)
                {
                    g.GetComponent<Player>().detectPerc += detectRate;
                }


            }
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, Time.deltaTime * speed);
            transform.LookAt(waypoints[currentWaypoint].transform.position);
            if (Vector3.Distance(this.transform.position, waypoints[currentWaypoint].transform.position) < 0.01f)
            {
                if (waypoints.Length == currentWaypoint + 1)
                {
                    currentWaypoint = 0;
                }
                else
                {
                    currentWaypoint++;
                }
            }
        }
	}
}
