using UnityEngine;
using System.Collections;

public class JuegoEscMgr : MonoBehaviour 
{
	bool JuegoFinalizado = false;
	public float TiempoEsperaFin = 25;
	float Tempo = 0;
	
	bool JuegoIniciado = false;
	public float TiempoEsperaInicio = 120;

	void Update () 
	{
		if(JuegoFinalizado)
		{
			Tempo += Time.deltaTime;
			if(Tempo > TiempoEsperaFin)
			{
				Tempo = 0;
				Application.LoadLevel(0);
			}
		}
		
		if(!JuegoIniciado)
		{
			if(Tempo > TiempoEsperaInicio)
			{
				Application.LoadLevel(0);
			}
		}		
		
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
	
	//---------------------------------------------------//
	
	public void JuegoFinalizar()
	{
		JuegoFinalizado = true;
	}
	
	public void JuegoIniciar()
	{
		JuegoIniciado = true;
	}
}
