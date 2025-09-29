using System.Collections.Generic;
using UnityEngine;

namespace EscenaDescarga
{
    public class ManejoPallets : MonoBehaviour
    {
        public ControladorDeDescarga controlador;
        protected int _contador = 0;
        protected List<Pallet> _pallets = new();

        public virtual bool Recibir(Pallet pallet)
        {
            Debug.Log(gameObject.name + " / Recibir()");
            _pallets.Add(pallet);
            pallet.Pasaje();
            return true;
        }

        public bool Tenencia()
        {
            if (_pallets.Count != 0)
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