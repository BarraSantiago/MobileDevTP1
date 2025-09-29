using EscenaDescarga;
using UnityEngine;

namespace Escenas.Juego.Calibracion
{
    public class ContrCalibracion : MonoBehaviour
    {
        //bool EnTutorial = false;

        public enum Estados
        {
            Calibrando,
            Tutorial,
            Finalizado
        }

        public Player pj;

        /*
    public string ManoIzqName = "Left Hand";
    public string ManoDerName = "Right Hand";

    bool StayIzq = false;
    bool StayDer = false;
    */
        /*
    public float TiempCalib = 3;
    float Tempo = 0;
    */
        public float tiempEspCalib = 3;
        public Estados estAct = Estados.Calibrando;

        public ManejoPallets partida;
        public ManejoPallets llegada;
        public Pallet p;
        public ManejoPallets palletsMover;

        private GameManager _gm;
        private float _tempo2;

        //----------------------------------------------------//

        // Use this for initialization
        private void Start()
        {
            /*
        renderer.enabled = false;
        collider.enabled = false;
        */
            palletsMover.enabled = false;
            pj.contrCalib = this;

            _gm = GameObject.Find("GameMgr").GetComponent<GameManager>();

            p.cintaReceptora = llegada.gameObject;
            partida.Recibir(p);

            SetActivComp(false);
        }

        // Update is called once per frame
        private void Update()
        {
            if (estAct == Estados.Tutorial)
                if (_tempo2 < tiempEspCalib)
                {
                    _tempo2 += T.GetDT();
                    if (_tempo2 > tiempEspCalib) SetActivComp(true);
                }

            /*
        if(Calibrado)
        {
            if(Tempo2 < TiempEspCalib)
            {
                Tempo2 += Time.deltaTime;
                if(Tempo2 > TiempEspCalib)
                {
                    PrenderVolante();
                }
            }

            if(VolanteEncendido)
            {
                if(StayIzq && StayDer)
                {
                    if(Tempo < TiempCalib)
                    {
                        Tempo += Time.deltaTime;
                        if(Tempo > TiempCalib)
                        {
                            FinCalibracion();
                        }
                    }
                }
            }
        }
        */
        }
        /*
    void OnTriggerStay(Collider coll)
    {
        if(coll.name == ManoIzqName)
            StayIzq = true;
        else if(coll.name == ManoDerName)
            StayDer = true;
    }

    void OnTriggerExit(Collider coll)
    {
        if(coll.name == ManoIzqName || coll.name == ManoDerName)
            Reiniciar();
    }
    */
        //----------------------------------------------------//
        /*
    void Reiniciar()
    {
        bool StayIzq = false;
        bool StayDer = false;
        Tempo = 0;
    }

    void PrenderVolante()
    {
        VolanteEncendido = true;
        renderer.enabled = true;
        collider.enabled = true;
    }
    */

        private void FinCalibracion()
        {
            /*
        Reiniciar();
        GM.CambiarATutorial(Pj.IdPlayer);
        */
        }

        public void IniciarTesteo()
        {
            estAct = Estados.Tutorial;
            palletsMover.enabled = true;
            //Reiniciar();
        }

        public void FinTutorial()
        {
            estAct = Estados.Finalizado;
            palletsMover.enabled = false;
            _gm.FinCalibracion(pj.idPlayer);
        }

        private void SetActivComp(bool estado)
        {
            if (partida.GetComponent<Renderer>() != null)
                partida.GetComponent<Renderer>().enabled = estado;
            partida.GetComponent<Collider>().enabled = estado;
            if (llegada.GetComponent<Renderer>() != null)
                llegada.GetComponent<Renderer>().enabled = estado;
            llegada.GetComponent<Collider>().enabled = estado;
            p.GetComponent<Renderer>().enabled = estado;
        }
    }
}