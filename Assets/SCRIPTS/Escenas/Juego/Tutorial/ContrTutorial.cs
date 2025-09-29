using Prefabs.Deposito;
using UnityEngine;

namespace Escenas.Juego.Tutorial
{
    public class ContrTutorial : MonoBehaviour
    {
        public Player pj;
        public float tiempTuto = 15;
        public float tempo;

        public bool finalizado;

        private GameManager _gm;
        private bool _iniciado;

        //------------------------------------------------------------------//

        // Use this for initialization
        private void Start()
        {
            _gm = GameObject.Find("GameMgr").GetComponent<GameManager>();

            pj.contrTuto = this;
        }

        // Update is called once per frame
        private void Update()
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() == pj)
                Finalizar();
        }

        //------------------------------------------------------------------//

        public void Iniciar()
        {
            pj.GetComponent<Frenado>().RestaurarVel();
            _iniciado = true;
        }

        public void Finalizar()
        {
            finalizado = true;
            _gm.FinTutorial(pj.idPlayer);
            pj.GetComponent<Frenado>().Frenar();
            pj.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            pj.VaciarInv();
        }
    }
}