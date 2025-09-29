using EscenaDescarga;
using UnityEngine;

namespace Escenas.Juego.Calibracion
{
    public class EstanteLlegada : ManejoPallets
    {
        public GameObject Mano;
        public ContrCalibracion ContrCalib;

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
            p.Portador = gameObject;
            base.Recibir(p);
            ContrCalib.FinTutorial();

            return true;
        }
    }
}