using UnityEngine;

namespace EscenaDescarga
{
    public class Cinta : ManejoPallets
    {
        public bool encendida; //lo que hace la animacion
        public float velocidad = 1;
        public GameObject mano;
        public float tiempo = 0.5f;

        //animacion de parpadeo
        public float intervalo = 0.7f;
        public float permanencia = 0.2f;
        public GameObject modelCinta;
        public Color32 colorParpadeo;
        private float _animTempo;
        private Color32 _colorOrigModel;
        private bool _conPallet;
        private Transform _objAct;
        private float _tempo;

        //------------------------------------------------------------//

        private void Start()
        {
            _colorOrigModel = modelCinta.GetComponent<Renderer>().material.color;
        }

        private void Update()
        {
            //animacion de parpadeo
            if (encendida)
            {
                _animTempo += T.GetDT();
                if (_animTempo > permanencia)
                    if (modelCinta.GetComponent<Renderer>().material.color == colorParpadeo)
                    {
                        _animTempo = 0;
                        modelCinta.GetComponent<Renderer>().material.color = _colorOrigModel;
                    }

                if (_animTempo > intervalo)
                    if (modelCinta.GetComponent<Renderer>().material.color == _colorOrigModel)
                    {
                        _animTempo = 0;
                        modelCinta.GetComponent<Renderer>().material.color = colorParpadeo;
                    }
            }

            //movimiento del pallet
            for (int i = 0; i < _pallets.Count; i++)
                if (_pallets[i].GetComponent<Renderer>().enabled)
                    if (!_pallets[i].GetComponent<Pallet>().enSmoot)
                    {
                        _pallets[i].GetComponent<Pallet>().enabled = false;
                        _pallets[i].tempoEnCinta += T.GetDT();

                        _pallets[i].transform.position += transform.right * velocidad * T.GetDT();
                        Vector3 vAux = _pallets[i].transform.localPosition;
                        vAux.y = 3.61f; //altura especifica
                        _pallets[i].transform.localPosition = vAux;

                        if (_pallets[i].tempoEnCinta >= _pallets[i].tiempEnCinta)
                        {
                            _pallets[i].tempoEnCinta = 0;
                            _objAct.gameObject.SetActiveRecursively(false);
                        }
                    }
        }

        private void OnTriggerEnter(Collider other)
        {
            ManejoPallets recept = other.GetComponent<ManejoPallets>();
            if (recept != null) Dar(recept);
        }


        //------------------------------------------------------------//

        public override bool Recibir(Pallet p)
        {
            _tempo = 0;
            controlador.LlegadaPallet(p);
            p.portador = gameObject;
            _conPallet = true;
            _objAct = p.transform;
            base.Recibir(p);
            //p.GetComponent<Pallet>().enabled = false;
            Apagar();

            return true;
        }

        public void Encender()
        {
            encendida = true;
            modelCinta.GetComponent<Renderer>().material.color = _colorOrigModel;
        }

        public void Apagar()
        {
            encendida = false;
            _conPallet = false;
            modelCinta.GetComponent<Renderer>().material.color = _colorOrigModel;
        }
    }
}