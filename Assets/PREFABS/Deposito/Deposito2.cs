using EscenaDescarga;
using UnityEngine;

namespace Prefabs.Deposito
{
	public class Deposito2 : MonoBehaviour 
	{
	
		Player _pjActual;
		public string playerTag = "Player";
		public bool vacio = true;
		public ControladorDeDescarga contr1;
		public ControladorDeDescarga contr2;
	
		Collider[] _pjColl;
	
		//----------------------------------------------//

		void Start () 
		{
			contr1 = GameObject.Find("ContrDesc1").GetComponent<ControladorDeDescarga>();
			contr2 = GameObject.Find("ContrDesc2").GetComponent<ControladorDeDescarga>();
		
			Physics.IgnoreLayerCollision(8,9,false);
		}
	
		// Update is called once per frame
		void Update () 
		{
			if(!vacio)
			{
				_pjActual.transform.position = transform.position;
				_pjActual.transform.forward = transform.forward;
			}
		}
	
		//----------------------------------------------//
	
		public void Soltar()
		{
			_pjActual.VaciarInv();
			_pjActual.GetComponent<Frenado>().RestaurarVel();
			_pjActual.GetComponent<Respawn>().Respawnear(transform.position,transform.forward);
		
			_pjActual.GetComponent<Rigidbody>().useGravity = true;
			for(int i = 0; i < _pjColl.Length; i++)
				_pjColl[i].enabled = true;
		
			Physics.IgnoreLayerCollision(8,9,false);
		
			_pjActual = null;
			vacio = true;
		
	
		}
	
		public void Entrar(Player pj)
		{
			if(pj.ConBolasas())
			{
			
				_pjActual = pj;
			
				_pjColl = _pjActual.GetComponentsInChildren<Collider>();
				for(int i = 0; i < _pjColl.Length; i++)
					_pjColl[i].enabled = false;
				_pjActual.GetComponent<Rigidbody>().useGravity = false;
			
				_pjActual.transform.position = transform.position;
				_pjActual.transform.forward = transform.forward;
			
				vacio = false;
			
				Physics.IgnoreLayerCollision(8,9,true);
			
				Entro();
			}
		}
	
		public void Entro()
		{		
			if(_pjActual.idPlayer == 0)
				contr1.Activar(this);
			else
				contr2.Activar(this);
		}
	}
}
