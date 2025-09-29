using UnityEngine;

namespace EscenaDescarga
{
    public class AnimMngDesc : MonoBehaviour
    {
        public string animEntrada = "Entrada";
        public string animSalida = "Salida";
        public ControladorDeDescarga contrDesc;

        public GameObject puertaAnimada;

        private AnimEnCurso _animAct = AnimEnCurso.Nada;

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
                Entrar();
            if (Input.GetKeyDown(KeyCode.X))
                Salir();

            switch (_animAct)
            {
                case AnimEnCurso.Entrada:

                    if (!GetComponent<Animation>().IsPlaying(animEntrada))
                    {
                        _animAct = AnimEnCurso.Nada;
                        contrDesc.FinAnimEntrada();
                        print("fin Anim Entrada");
                    }

                    break;

                case AnimEnCurso.Salida:

                    if (!GetComponent<Animation>().IsPlaying(animSalida))
                    {
                        _animAct = AnimEnCurso.Nada;
                        contrDesc.FinAnimSalida();
                        print("fin Anim Salida");
                    }

                    break;

                case AnimEnCurso.Nada:
                    break;
            }
        }

        public void Entrar()
        {
            _animAct = AnimEnCurso.Entrada;
            GetComponent<Animation>().Play(animEntrada);

            if (puertaAnimada != null)
            {
                puertaAnimada.GetComponent<Animation>()["AnimPuerta"].time = 0;
                puertaAnimada.GetComponent<Animation>()["AnimPuerta"].speed = 1;
                puertaAnimada.GetComponent<Animation>().Play("AnimPuerta");
            }
        }

        public void Salir()
        {
            _animAct = AnimEnCurso.Salida;
            GetComponent<Animation>().Play(animSalida);

            if (puertaAnimada != null)
            {
                puertaAnimada.GetComponent<Animation>()["AnimPuerta"].time =
                    puertaAnimada.GetComponent<Animation>()["AnimPuerta"].length;
                puertaAnimada.GetComponent<Animation>()["AnimPuerta"].speed = -1;
                puertaAnimada.GetComponent<Animation>().Play("AnimPuerta");
            }
        }

        private enum AnimEnCurso
        {
            Salida,
            Entrada,
            Nada
        }
    }
}