using EscenaDescarga;
using UnityEngine;

namespace Escenas.Juego.Calibracion
{
    public class EstantePartida : ManejoPallets
    {
        //public Cinta CintaReceptora;//cinta que debe recibir la bolsa
        public GameObject manoReceptora;
        //public Pallet.Valores Valor;

        private void OnTriggerEnter(Collider other)
        {
            ManejoPallets recept = other.GetComponent<ManejoPallets>();
            if (recept != null) Dar(recept);
        }

        //------------------------------------------------------------//

        public override void Dar(ManejoPallets receptor)
        {
            if (receptor.Recibir(_pallets[0])) _pallets.RemoveAt(0);
        }

        public override bool Recibir(Pallet pallet)
        {
            //pallet.CintaReceptora = CintaReceptora.gameObject;
            pallet.portador = gameObject;
            return base.Recibir(pallet);
        }
    }
}