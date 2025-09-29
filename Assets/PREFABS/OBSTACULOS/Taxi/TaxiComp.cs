using UnityEngine;

namespace Prefabs.OBSTACULOS.Taxi
{
	/// <summary>
	/// basicamente lo que hace es que viaja en linea recta y ocacionalmente gira para un cosatado
	/// previamente verificado, tambien cuando llega al final del recorrido se reinicia en la pos. orig.
	/// </summary>
	public class TaxiComp : MonoBehaviour 
	{
		public string finTaxiTag = "FinTaxi";
		public string limiteTag = "Terreno";
	
		public float vel = 0;
	
		public Vector2 tiempCadaCuantoDoblaMaxMin = Vector2.zero;
	
		public float duracionGiro = 0;
		float _tempoDurGir = 0;
	
		public float alcanceVerif = 0;
	
		public string tagTerreno = "";
	
		public bool girando = false;
		Vector3 _rotIni;//pasa saber como volver a su posicion original
		Vector3 _posIni;//para saber donde reiniciar al taxi
	
		float _tiempEntreGiro = 0;
		float _tempoEntreGiro = 0;
	
		public float angDeGiro = 30;
		float _tiempPGiro = 1;//1 es el tiempo que tarda en llegar al otro quaternion
	
		RaycastHit _rh;
	
		bool _respawneando = false;
	
	
		enum Lado{Der, Izq}
	
		//-----------------------------------------------------------------//

		// Use this for initialization
		void Start () 
		{
			_tiempEntreGiro = (float) Random.Range(tiempCadaCuantoDoblaMaxMin.x, tiempCadaCuantoDoblaMaxMin.y);
			_rotIni = this.transform.localEulerAngles;
			_posIni = transform.position;
		}
	
		// Update is called once per frame
		void Update () 
		{
		
			if(_respawneando)
			{
				if(Medicion())
					Respawn();
			}
			else
			{
				if(girando)
				{
					_tempoDurGir += Time.deltaTime;
					if(_tempoDurGir > duracionGiro)
					{
						_tempoDurGir = 0;
						DejarDoblar();
					}
				}
				else
				{
					_tempoEntreGiro += Time.deltaTime;
					if(_tempoEntreGiro > _tiempEntreGiro)
					{
						_tempoEntreGiro = 0;
						Doblar();
					}
				}
			}
		
		
		} 
	
		void OnTriggerEnter(Collider coll)
		{
			if(coll.tag == finTaxiTag)
			{
				transform.position = _posIni;
				transform.localEulerAngles = _rotIni;
			}		
		}
	
		void OnCollisionEnter(Collision coll)
		{
			if(coll.transform.tag == limiteTag)
			{
				_respawneando = true;
			}
		}
	
		void FixedUpdate () 
		{
			this.transform.position += transform.forward * Time.fixedDeltaTime * vel;
		}
	
		//--------------------------------------------------------------------//
	
		bool VerificarCostado(Lado lado)
		{
			switch (lado)
			{
				case Lado.Der:
					if(Physics.Raycast(transform.position, transform.right, out _rh, alcanceVerif))
					{
						if(_rh.transform.tag == tagTerreno)
						{
							return false;
						}
					}
					break;
			
				case Lado.Izq:
					if(Physics.Raycast(transform.position, transform.right * (-1), out _rh, alcanceVerif))
					{
						if(_rh.transform.tag == tagTerreno)
						{
							return false;
						}
					}
					break;
			}
		
			return true;
		}	
	
		void Doblar()
		{
			girando = true;
			//escoje un lado
			Lado lado;
			if((int)Random.Range(0,2) == 0)
			{
				lado = TaxiComp.Lado.Izq;
				//verifica, si no da cambia a derecha
				if(!VerificarCostado(lado))
					lado = TaxiComp.Lado.Der;
			}
			else
			{
				lado = TaxiComp.Lado.Der;
				//verifica, si no da cambia a izq
				if(!VerificarCostado(lado))
					lado = TaxiComp.Lado.Izq;
			}
		
		
			if(lado == TaxiComp.Lado.Der)
			{
				Vector3 vaux = transform.localEulerAngles;
				vaux.y += angDeGiro;
				transform.localEulerAngles = vaux;
			}
			else
			{
				Vector3 vaux = transform.localEulerAngles;
				vaux.y -= angDeGiro;
				transform.localEulerAngles = vaux;
			}
		}
	
		void DejarDoblar()
		{
			girando = false;
			_tiempEntreGiro = (float) Random.Range(tiempCadaCuantoDoblaMaxMin.x, tiempCadaCuantoDoblaMaxMin.y);
		
			transform.localEulerAngles = _rotIni;
		}
	
		void Respawn()
		{
			_respawneando = false;
		
			transform.position = _posIni;
			transform.localEulerAngles = _rotIni;
		}
	
		bool Medicion()
		{
			float dist1 = (GameManager.Instancia.player1.transform.position - _posIni).magnitude;
			float dist2 = (GameManager.Instancia.player2.transform.position - _posIni).magnitude;
		
			if(dist1 > 4 && dist2 > 4)
				return true;
			else
				return false;
		}
	}
}
