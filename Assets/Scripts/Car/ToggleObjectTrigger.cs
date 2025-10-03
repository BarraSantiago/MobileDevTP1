using UnityEngine;

public class ToggleObjectTrigger : MonoBehaviour
{
	void Awake()
	{
		GetComponent<Renderer>().enabled = false;
	}

	void OnTriggerEnter()
	{
		GetComponent<Renderer>().enabled = true;
	}
	
	void OnTriggerExit()
	{
		GetComponent<Renderer>().enabled = false;
	}
}