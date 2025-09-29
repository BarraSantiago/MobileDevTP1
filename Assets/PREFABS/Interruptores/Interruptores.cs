using UnityEngine;

namespace Prefabs.Interruptores
{
	public class Interruptores : MonoBehaviour 
	{
		public string tagPlayer = "Player";
	
		public GameObject[] aActivar;
	
		public bool activado = false;

		// Use this for initialization
		void Start () 
		{
	
		}
	
		// Update is called once per frame
		void Update () 
		{
	
		}
	
		void OnTriggerEnter(Collider other) 
		{
			if(!activado)
			{
				if(other.tag == tagPlayer)
				{
					activado = true;
					print("activado interrutor");
					for(int i = 0; i < aActivar.Length; i++)
					{
						aActivar[i].SetActiveRecursively(true);
					}
				}
			}
		}
	}
}
