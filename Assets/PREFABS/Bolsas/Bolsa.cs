using EscenaDescarga;
using UnityEngine;

namespace Prefabs.Bolsas
{
	public class Bolsa : MonoBehaviour
	{
		public Pallet.Valores monto;
		//public int IdPlayer = 0;
		public string tagPlayer = "";
		public Texture2D imagenInventario;
		Player _pj = null;
	
		bool _desapareciendo;
		public GameObject particulas;
		public float tiempParts = 2.5f;

		// Use this for initialization
		void Start () 
		{
			monto = Pallet.Valores.Valor2;
		
		
			if(particulas != null)
				particulas.SetActive(false);
			
		}
	
		// Update is called once per frame
		void Update ()
		{
		
			if(_desapareciendo)
			{
				tiempParts -= Time.deltaTime;
				if(tiempParts <= 0)
				{
					GetComponent<Renderer>().enabled = true;
					GetComponent<Collider>().enabled = true;
				
					particulas.GetComponent<ParticleSystem>().Stop();
					gameObject.SetActive(false);
				}
			}
		
		}
	
		void OnTriggerEnter(Collider coll)
		{
			if(coll.tag == tagPlayer)
			{
				_pj = coll.GetComponent<Player>();
				//if(IdPlayer == Pj.IdPlayer)
				//{
				if(_pj.AgregarBolsa(this))
					Desaparecer();
				//}
			}
		}
	
		public void Desaparecer()
		{
			particulas.GetComponent<ParticleSystem>().Play();
			_desapareciendo = true;
		
			GetComponent<Renderer>().enabled = false;
			GetComponent<Collider>().enabled = false;
		
			if(particulas != null)
			{
				particulas.GetComponent<ParticleSystem>().Play();
			}
	
		}
	}
}
