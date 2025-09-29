using Prefabs.Deposito;
using UnityEngine;

namespace Escenas.Juego.Tutorial
{
    public class ContrTutorial : MonoBehaviour
    {
        public Player Pj;
        public float TiempTuto = 15;
        public float Tempo;

        public bool Finalizado;

        private GameManager GM;
        private bool Iniciado;

        //------------------------------------------------------------------//

        // Use this for initialization
        private void Start()
        {
            GM = GameObject.Find("GameMgr").GetComponent<GameManager>();

            Pj.ContrTuto = this;
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
            if (other.GetComponent<Player>() == Pj)
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
            Finalizado = true;
            GM.FinTutorial(Pj.IdPlayer);
            Pj.GetComponent<Frenado>().Frenar();
            Pj.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            Pj.VaciarInv();
        }
    }
}