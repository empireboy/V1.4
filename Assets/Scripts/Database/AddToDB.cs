using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToDB : MonoBehaviour {

	void Start () {
        StartCoroutine(DoQuery());
	}
	
    IEnumerator DoQuery()
    {
        WWW request = new WWW("http://22198.hosts.ma-cloud.nl/bewijzenmap/p2.1/gpr/database/database.php?t_x=1&t_y=2&t_z=400&p_id=20");
        yield return request;
    }
}
