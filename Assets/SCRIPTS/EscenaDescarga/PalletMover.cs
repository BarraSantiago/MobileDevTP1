using UnityEngine;

namespace EscenaDescarga
{
    public class PalletMover : ManejoPallets
    {
        public enum MoveType
        {
            Wasd,
            Arrows
        }

        public MoveType miInput;

        public ManejoPallets desde, hasta;
        private bool _segundoCompleto;

        private void Update()
        {
            switch (miInput)
            {
                case MoveType.Wasd:
                    if (!Tenencia() && desde.Tenencia() && Input.GetKeyDown(KeyCode.A)) PrimerPaso();
                    if (Tenencia() && Input.GetKeyDown(KeyCode.S)) SegundoPaso();
                    if (_segundoCompleto && Tenencia() && Input.GetKeyDown(KeyCode.D)) TercerPaso();
                    break;
                case MoveType.Arrows:
                    if (!Tenencia() && desde.Tenencia() && Input.GetKeyDown(KeyCode.LeftArrow)) PrimerPaso();
                    if (Tenencia() && Input.GetKeyDown(KeyCode.DownArrow)) SegundoPaso();
                    if (_segundoCompleto && Tenencia() && Input.GetKeyDown(KeyCode.RightArrow)) TercerPaso();
                    break;
            }
        }

        private void PrimerPaso()
        {
            desde.Dar(this);
            _segundoCompleto = false;
        }

        private void SegundoPaso()
        {
            _pallets[0].transform.position = transform.position;
            _segundoCompleto = true;
        }

        private void TercerPaso()
        {
            Dar(hasta);
            _segundoCompleto = false;
        }

        public override void Dar(ManejoPallets receptor)
        {
            if (Tenencia())
                if (receptor.Recibir(_pallets[0]))
                    _pallets.RemoveAt(0);
        }

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
    }
}