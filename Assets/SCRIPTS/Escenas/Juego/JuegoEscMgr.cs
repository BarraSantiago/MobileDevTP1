using UnityEngine;

namespace Escenas.Juego
{
    public class JuegoEscMgr : MonoBehaviour
    {
        public float
            tiempoEsperaFin =
                25; //tiempo que espera la aplicacion para volver al video introductorio desp de terminada la partida

        public float
            tiempoEsperaInicio =
                120; //tiempo que espera la aplicacion para volver al video introductorio desp de terminada la partida

        private bool _juegoFinalizado;

        private bool _juegoIniciado;
        private float _tempo;
        private float _tempo2;

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            if (_juegoFinalizado)
            {
                _tempo += Time.deltaTime;
                if (_tempo > tiempoEsperaFin)
                {
                    _tempo = 0;
                    Application.LoadLevel(0);
                }
            }

            if (!_juegoIniciado)
            {
                _tempo2 += Time.deltaTime;
                if (_tempo > tiempoEsperaInicio)
                {
                    _tempo2 = 0;
                    Application.LoadLevel(0);
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

            //reinicia
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                Application.LoadLevel(Application.loadedLevel);
        }

        //---------------------------------------------------//

        public void JuegoFinalizar()
        {
            _juegoFinalizado = true;
        }

        public void JuegoIniciar()
        {
            _juegoIniciado = true;
        }
    }
}