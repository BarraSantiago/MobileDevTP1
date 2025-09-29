using UnityEngine;

namespace EscenaDescarga
{
    public class ManoRecept : ManejoPallets
    {
        public bool tengoPallet;

        private void FixedUpdate()
        {
            tengoPallet = Tenencia();
        }

        private void OnTriggerEnter(Collider other)
        {
            ManejoPallets recept = other.GetComponent<ManejoPallets>();
            if (recept != null) Dar(recept);
        }

        //---------------------------------------------------------//	

        public override bool Recibir(Pallet pallet)
        {
            if (!Tenencia())
            {
                pallet.portador = gameObject;
                base.Recibir(pallet);
                return true;
            }

            return false;
        }

        public override void Dar(ManejoPallets receptor)
        {
            //Debug.Log(gameObject.name+ " / Dar()");
            switch (receptor.tag)
            {
                case "Mano":
                    if (Tenencia())
                        //Debug.Log(gameObject.name+ " / Dar()"+" / Tenencia=true");
                        if (receptor.name == "Right Hand")
                            if (receptor.Recibir(_pallets[0]))
                                //Debug.Log(gameObject.name+ " / Dar()"+" / Tenencia=true"+" / receptor.Recibir(Pallets[0])=true");
                                _pallets.RemoveAt(0);
                    //Debug.Log("pallet entregado a Mano de Mano");
                    break;

                case "Cinta":
                    if (Tenencia())
                        if (receptor.Recibir(_pallets[0]))
                            _pallets.RemoveAt(0);
                    //Debug.Log("pallet entregado a Cinta de Mano");
                    break;

                case "Estante":
                    break;
            }
        }
    }
}