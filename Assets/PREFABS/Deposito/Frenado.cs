using UnityEngine;

namespace Prefabs.Deposito
{
	public class Frenado : MonoBehaviour 
	{
		public float velEntrada = 0;
		public string tagDeposito = "Deposito";
	
		ControlDireccion _kInput;
	
	
		float _dagMax = 15f;
		float _dagIni = 1f;
		int _contador = 0;
		int _cantMensajes = 10;
		float _tiempFrenado = 0.5f;
		float _tempo = 0f;
	
		Vector3 _destino;
	
		public bool frenando = false;
		bool _reduciendoVel = false;
	
		//-----------------------------------------------------//
	
		// Use this for initialization
		void Start () 
		{
			//RestaurarVel();
			Frenar();
		}
	
		// Update is called once per frame
		void Update () 
		{
	
		}
	
		void FixedUpdate ()
		{
			if(frenando)
			{
				_tempo += T.GetFdt();
				if(_tempo >= (_tiempFrenado / _cantMensajes) * _contador)
				{
					_contador++;
					//gameObject.SendMessage("SetDragZ", (float) (DagMax / CantMensajes) * Contador);
				}
				if(_tempo >= _tiempFrenado)
				{
					//termino de frenar, que haga lo que quiera
				}
			}
		}
	
		void OnTriggerEnter(Collider other) 
		{
			if(other.tag == tagDeposito)
			{
				Deposito2 dep = other.GetComponent<Deposito2>();
				if(dep.vacio)
				{	
					if(this.GetComponent<Player>().ConBolasas())
					{
						dep.Entrar(this.GetComponent<Player>());
						_destino = other.transform.position;
						transform.forward = _destino - transform.position;
						Frenar();
					}				
				}
			}
		}
	
		//-----------------------------------------------------------//
	
		public void Frenar()
		{
			//Debug.Log(gameObject.name + "frena");
			GetComponent<ControlDireccion>().enabled = false;
			gameObject.GetComponent<CarController>().SetAcel(0);

			GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
		
			frenando = true;
		
			//gameObject.SendMessage("SetDragZ", 25f);
			_tempo = 0;
			_contador = 0;
		}
	
		public void RestaurarVel()
		{
			//Debug.Log(gameObject.name + "restaura la velociad");
			GetComponent<ControlDireccion>().enabled = true;
			gameObject.GetComponent<CarController>().SetAcel(1);
			frenando = false;
			_tempo = 0;
			_contador = 0;
			//gameObject.SendMessage("SetDragZ", 1f);
		}
	}
}
