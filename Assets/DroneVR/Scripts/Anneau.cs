using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anneau : MonoBehaviour
{

	private CheckPoint	checkpointParent;
	private Circuit		circuitParent;

    // Start is called before the first frame update
    void Start()
    {
		checkpointParent	= transform.parent.GetComponent<CheckPoint>();
		circuitParent		= transform.root.GetComponent<Circuit>();

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (checkpointParent.isNext && !checkpointParent.isFinished)
		{
			circuitParent.completeCheckpoint(checkpointParent);
		}
	}
	    
}
