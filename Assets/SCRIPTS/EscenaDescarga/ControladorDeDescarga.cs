using System.Collections.Generic;
using Prefabs.Deposito;
using UnityEngine;

namespace EscenaDescarga
{
    public class ControladorDeDescarga : MonoBehaviour
    {
        public GameObject[] componentes; //todos los componentes que debe activar en esta escena

        public Player pj; //jugador

        public Pallet pEnMov;

        //las camaras que enciende y apaga
        public GameObject camaraConduccion;
        public GameObject camaraDescarga;

        //los prefab de los pallets
        public GameObject pallet1;
        public GameObject pallet2;
        public GameObject pallet3;


        public Estanteria est1;
        public Estanteria est2;
        public Estanteria est3;

        public Cinta cin2;

        public float bonus;


        public AnimMngDesc objAnimado;
        private MeshCollider _collCamion;

        private int _contador;

        private Deposito2 _dep;
        private List<Pallet.Valores> _ps = new();
        private float _tempoBonus;


        //--------------------------------------------------------------//

        // Use this for initialization
        private void Start()
        {
            for (int i = 0; i < componentes.Length; i++) componentes[i].SetActiveRecursively(false);

            _collCamion = pj.GetComponentInChildren<MeshCollider>();
            pj.SetContrDesc(this);
            if (objAnimado != null)
                objAnimado.contrDesc = this;
        }

        // Update is called once per frame
        private void Update()
        {
            //contador de tiempo
            if (pEnMov != null)
            {
                if (_tempoBonus > 0)
                {
                    bonus = _tempoBonus * (float)pEnMov.valor / pEnMov.tiempo;
                    _tempoBonus -= T.GetDT();
                }
                else
                {
                    bonus = 0;
                }
            }
        }

        //--------------------------------------------------------------//

        public void Activar(Deposito2 d)
        {
            _dep = d; //recibe el deposito para que sepa cuando dejarlo ir al camion
            camaraConduccion.SetActiveRecursively(false); //apaga la camara de conduccion

            //activa los componentes
            for (int i = 0; i < componentes.Length; i++) componentes[i].SetActiveRecursively(true);


            _collCamion.enabled = false;
            pj.CambiarADescarga();


            GameObject go;
            //asigna los pallets a las estanterias
            for (int i = 0; i < pj.bolasas.Length; i++)
                if (pj.bolasas[i] != null)
                {
                    _contador++;

                    switch (pj.bolasas[i].monto)
                    {
                        case Pallet.Valores.Valor1:
                            go = Instantiate(pallet1);
                            est1.Recibir(go.GetComponent<Pallet>());
                            break;

                        case Pallet.Valores.Valor2:
                            go = Instantiate(pallet2);
                            est2.Recibir(go.GetComponent<Pallet>());
                            break;

                        case Pallet.Valores.Valor3:
                            go = Instantiate(pallet3);
                            est3.Recibir(go.GetComponent<Pallet>());
                            break;
                    }
                }

            //animacion
            objAnimado.Entrar();
        }

        //cuando sale de un estante
        public void SalidaPallet(Pallet p)
        {
            pEnMov = p;
            _tempoBonus = p.tiempo;
            pj.SacarBolasa();
            //inicia el contador de tiempo para el bonus
        }

        //cuando llega a la cinta
        public void LlegadaPallet(Pallet p)
        {
            //termina el contador y suma los pts

            //termina la descarga
            pEnMov = null;
            _contador--;

            pj.dinero += (int)bonus;

            if (_contador <= 0)
                Finalizacion();
            else
                est2.EncenderAnim();
        }

        public void FinDelJuego()
        {
            //metodo llamado por el GameManager para avisar que se termino el juego

            //desactiva lo que da y recibe las bolsas para que no halla mas flujo de estas
            est2.enabled = false;
            cin2.enabled = false;
        }

        private void Finalizacion()
        {
            objAnimado.Salir();
        }

        public Pallet GetPalletEnMov()
        {
            return pEnMov;
        }

        public void FinAnimEntrada()
        {
            //avisa cuando termino la animacion para que prosiga el juego
            est2.EncenderAnim();
        }

        public void FinAnimSalida()
        {
            //avisa cuando termino la animacion para que prosiga el juego

            for (int i = 0; i < componentes.Length; i++) componentes[i].SetActiveRecursively(false);

            camaraConduccion.SetActiveRecursively(true);

            _collCamion.enabled = true;

            pj.CambiarAConduccion();

            _dep.Soltar();
        }
    }
}