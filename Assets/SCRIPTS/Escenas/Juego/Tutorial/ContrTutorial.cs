using UnityEngine;
using System.Collections;

public class ContrTutorial : MonoBehaviour 
{
	public Player Pj;

	bool Iniciado = false;

	//------------------------------------------------------------------//

	// Use this for initialization
	void Start () 
	{
		GameObject.Find("GameMgr").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*
		if(Iniciado)
		{
			if(Tempo < TiempTuto)
			{
				Tempo += T.GetDT();
				if(Tempo >= TiempTuto)
				{
					Finalizar();
				}
			}
		}
		*/
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Player>() == Pj)
			Finalizar();
	}
	
	//------------------------------------------------------------------//
	
	public void Iniciar()
	{
		Pj.GetComponent<Frenado>().RestaurarVel();
		Iniciado = true;
	}
	
	public void Finalizar()
	{
		Pj.GetComponent<Frenado>().Frenar();
		Pj.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
		Pj.VaciarInv();
	}
}
