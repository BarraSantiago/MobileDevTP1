using EscenaDescarga;
using UnityEngine;

namespace Escenas.Juego.Calibracion
{
    public class EstantePartida : ManejoPallets
    {
        //public Cinta CintaReceptora;//cinta que debe recibir la bolsa
        public GameObject ManoReceptora;
        //public Pallet.Valores Valor;

        private void OnTriggerEnter(Collider other)
        {
            ManejoPallets recept = other.GetComponent<ManejoPallets>();
            if (recept != null) Dar(recept);
        }

        //------------------------------------------------------------//

        public override void Dar(ManejoPallets receptor)
        {
            if (receptor.Recibir(Pallets[0])) Pallets.RemoveAt(0);
        }

        public override bool Recibir(Pallet pallet)
        {
            //pallet.CintaReceptora = CintaReceptora.gameObject;
            pallet.Portador = gameObject;
            return base.Recibir(pallet);
        }
    }
}