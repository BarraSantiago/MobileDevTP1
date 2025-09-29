using UnityEngine;

namespace EscenaDescarga
{
    public class Estanteria : ManejoPallets
    {
        public Cinta cintaReceptora; //cinta que debe recibir la bolsa
        public Pallet.Valores valor;
        public bool anim;


        //animacion de parpadeo
        public float intervalo = 0.7f;
        public float permanencia = 0.2f;
        public GameObject modelSuelo;
        public Color32 colorParpadeo;
        private float _animTempo;
        private Color32 _colorOrigModel;
        private PilaPalletMng _contenido;

        //--------------------------------//	

        private void Start()
        {
            _contenido = GetComponent<PilaPalletMng>();
            _colorOrigModel = modelSuelo.GetComponent<Renderer>().material.color;
        }

        private void Update()
        {
            //animacion de parpadeo
            if (anim)
            {
                _animTempo += T.GetDT();
                if (_animTempo > permanencia)
                    if (modelSuelo.GetComponent<Renderer>().material.color == colorParpadeo)
                    {
                        _animTempo = 0;
                        modelSuelo.GetComponent<Renderer>().material.color = _colorOrigModel;
                    }

                if (_animTempo > intervalo)
                    if (modelSuelo.GetComponent<Renderer>().material.color == _colorOrigModel)
                    {
                        _animTempo = 0;
                        modelSuelo.GetComponent<Renderer>().material.color = colorParpadeo;
                    }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            ManejoPallets recept = other.GetComponent<ManejoPallets>();
            if (recept != null) Dar(recept);
        }

        //------------------------------------------------------------//

        public override void Dar(ManejoPallets receptor)
        {
            if (Tenencia())
                if (controlador.GetPalletEnMov() == null)
                    if (receptor.Recibir(_pallets[0]))
                    {
                        //enciende la cinta y el indicador
                        //cambia la textura de cuantos pallet le queda
                        cintaReceptora.Encender();
                        controlador.SalidaPallet(_pallets[0]);
                        _pallets[0].GetComponent<Renderer>().enabled = true;
                        _pallets.RemoveAt(0);
                        _contenido.Sacar();
                        ApagarAnim();
                        //Debug.Log("pallet entregado a Mano de Estanteria");
                    }
        }

        public override bool Recibir(Pallet pallet)
        {
            pallet.cintaReceptora = cintaReceptora.gameObject;
            pallet.portador = gameObject;
            _contenido.Agregar();
            pallet.GetComponent<Renderer>().enabled = false;
            return base.Recibir(pallet);
        }

        public void ApagarAnim()
        {
            anim = false;
            modelSuelo.GetComponent<Renderer>().material.color = _colorOrigModel;
        }

        public void EncenderAnim()
        {
            anim = true;
            modelSuelo.GetComponent<Renderer>().material.color = _colorOrigModel;
        }
    }
}