using System.Collections.Generic;
using UnityEngine;

namespace EscenaDescarga
{
    public class ManejoPallets : MonoBehaviour
    {
        public ControladorDeDescarga Controlador;
        protected int Contador = 0;
        protected List<Pallet> Pallets = new();

        public virtual bool Recibir(Pallet pallet)
        {
            Debug.Log(gameObject.name + " / Recibir()");
            Pallets.Add(pallet);
            pallet.Pasaje();
            return true;
        }

        public bool Tenencia()
        {
            if (Pallets.Count != 0)
                return true;
            return false;

            /*
        if(Pallets.Count > Contador)
            return true;
        else
            return false;
            */
        }

        public virtual void Dar(ManejoPallets receptor)
        {
            //es el encargado de decidir si le da o no la bolsa
        }
    }
}