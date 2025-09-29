using UnityEngine;

namespace EscenaDescarga
{
    public class Pallet : MonoBehaviour
    {
        public enum Valores
        {
            Valor1 = 100000,
            Valor2 = 250000,
            Valor3 = 500000
        }

        public Valores valor;
        public float tiempo;
        public GameObject cintaReceptora;
        public GameObject portador;
        public float tiempEnCinta = 1.5f;
        public float tempoEnCinta;


        public float tiempSmoot = 0.3f;
        public bool enSmoot;
        private float _tempoSmoot;

        //----------------------------------------------//

        private void Start()
        {
            Pasaje();
        }

        private void LateUpdate()
        {
            if (!portador) return;
            if (enSmoot)
            {
                _tempoSmoot += T.GetDT();
                if (_tempoSmoot >= tiempSmoot)
                {
                    enSmoot = false;
                    _tempoSmoot = 0;
                }
                else
                {
                    if (portador.GetComponent<ManoRecept>())
                        transform.position = portador.transform.position - Vector3.up * 1.2f;
                    else
                        transform.position = Vector3.Lerp(transform.position, portador.transform.position, T.GetDT() * 10);
                }
            }
            else
            {
                if (portador.GetComponent<ManoRecept>())
                    transform.position = portador.transform.position - Vector3.up * 1.2f;
                else
                    transform.position = portador.transform.position;
            }
        }

        //----------------------------------------------//

        public float GetBonus()
        {
            if (tiempo > 0)
            {
                //calculo del bonus
            }

            return -1;
        }

        public void Pasaje()
        {
            enSmoot = true;
            _tempoSmoot = 0;
        }
    }
}