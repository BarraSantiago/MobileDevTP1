using EscenaDescarga;
using UnityEngine;

namespace Escenas.Juego.Calibracion
{
    public class EstanteLlegada : ManejoPallets
    {
        public GameObject mano;
        public ContrCalibracion contrCalib;

        //-----------------------------------------------//

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        //--------------------------------------------------//

        public override bool Recibir(Pallet p)
        {
            p.portador = gameObject;
            base.Recibir(p);
            contrCalib.FinTutorial();

            return true;
        }
    }
}