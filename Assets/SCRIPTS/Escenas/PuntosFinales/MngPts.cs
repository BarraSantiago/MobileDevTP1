using UnityEngine;

namespace Escenas.PuntosFinales
{
    public class MngPts : MonoBehaviour
    {
        public float tiempEmpAnims = 2.5f;

        public Vector2[] dineroPos;
        public Vector2 dineroEsc;
        public GUISkin gsDinero;

        public Vector2 ganadorPos;
        public Vector2 ganadorEsc;
        public Texture2D[] ganadores;
        public GUISkin gsGanador;

        public GameObject fondo;

        public float tiempEspReiniciar = 10;


        public float tiempParpadeo = 0.7f;

        public bool activadoAnims;

        private readonly Visualizacion _viz = new();

        private int _indexGanador = 0;
        private bool _primerImaParp = true;
        private Rect _r;
        private float _tempo;
        private float _tempoParpadeo;

        //---------------------------------//

        // Use this for initialization
        private void Start()
        {
            SetGanador();
        }

        // Update is called once per frame
        private void Update()
        {
            //PARA JUGAR
            if (Input.GetKeyDown(KeyCode.KeypadEnter) ||
                Input.GetKeyDown(KeyCode.Return) ||
                Input.GetKeyDown(KeyCode.Mouse0))
                Application.LoadLevel(0);

            //REINICIAR
            if (Input.GetKeyDown(KeyCode.Mouse1) ||
                Input.GetKeyDown(KeyCode.Keypad0))
                Application.LoadLevel(Application.loadedLevel);

            //CIERRA LA APLICACION
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

            //CALIBRACION DEL KINECT
            if (Input.GetKeyDown(KeyCode.Backspace)) Application.LoadLevel(3);


            tiempEspReiniciar -= Time.deltaTime;
            if (tiempEspReiniciar <= 0) Application.LoadLevel(0);


            if (activadoAnims)
            {
                _tempoParpadeo += Time.deltaTime;

                if (_tempoParpadeo >= tiempParpadeo)
                {
                    _tempoParpadeo = 0;

                    if (_primerImaParp)
                    {
                        _primerImaParp = false;
                    }
                    else
                    {
                        _tempoParpadeo += 0.1f;
                        _primerImaParp = true;
                    }
                }
            }


            if (!activadoAnims)
            {
                _tempo += Time.deltaTime;
                if (_tempo >= tiempEmpAnims)
                {
                    _tempo = 0;
                    activadoAnims = true;
                }
            }
        }

        /*
    void OnGUI()
    {
        SetGUIGanador();
        SetGUIPerdedor();
        GUI.skin = null;
    }
    */

        private void OnGUI()
        {
            if (activadoAnims)
            {
                SetDinero();
                SetCartelGanador();
            }

            GUI.skin = null;
        }

        //---------------------------------//

        /*
    void SetGUIGanador()
    {
        GUI.skin = GS_Vict;

        R.width = ScoreEsc.x * Screen.width /100;
        R.height = ScoreEsc.y * Screen.height /100;

        R.x = ScorePos.x * Screen.width / 100;
        R.y = ScorePos.y * Screen.height / 100;

        if(DatosPartida.LadoGanadaor == DatosPartida.Lados.Der)
            R.x = (Screen.width) - R.x - R.width;

        GUI.Box(R, "GANADOR" + '\n' + "DINERO: " + DatosPartida.PtsGanador);

    }

    void SetGUIPerdedor()
    {
        GUI.skin = GS_Derr;

        R.width = ScoreEsc.x * Screen.width /100;
        R.height = ScoreEsc.y * Screen.height /100;

        R.x = ScorePos.x * Screen.width / 100;
        R.y = ScorePos.y * Screen.height / 100;

        if(DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq)
            R.x = (Screen.width) - R.x - R.width;

        GUI.Box(R, "PERDEDOR" + '\n' + "DINERO: " + DatosPartida.PtsPerdedor);
    }
    */

        private void SetGanador()
        {
            switch (DatosPartida.LadoGanadaor)
            {
                case DatosPartida.Lados.Der:

                    gsGanador.box.normal.background = ganadores[1];

                    break;

                case DatosPartida.Lados.Izq:

                    gsGanador.box.normal.background = ganadores[0];

                    break;
            }
        }

        private void SetDinero()
        {
            GUI.skin = gsDinero;

            _r.width = dineroEsc.x * Screen.width / 100;
            _r.height = dineroEsc.y * Screen.height / 100;


            //IZQUIERDA
            _r.x = dineroPos[0].x * Screen.width / 100;
            _r.y = dineroPos[0].y * Screen.height / 100;

            if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq) //izquierda
            {
                if (!_primerImaParp) //para que parpadee
                    GUI.Box(_r, "$" + _viz.PrepararNumeros(DatosPartida.PtsGanador));
            }
            else
            {
                GUI.Box(_r, "$" + _viz.PrepararNumeros(DatosPartida.PtsPerdedor));
            }


            //DERECHA
            _r.x = dineroPos[1].x * Screen.width / 100;
            _r.y = dineroPos[1].y * Screen.height / 100;

            if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Der) //derecha
            {
                if (!_primerImaParp) //para que parpadee
                    GUI.Box(_r, "$" + _viz.PrepararNumeros(DatosPartida.PtsGanador));
            }
            else
            {
                GUI.Box(_r, "$" + _viz.PrepararNumeros(DatosPartida.PtsPerdedor));
            }
        }

        private void SetCartelGanador()
        {
            GUI.skin = gsGanador;

            _r.width = ganadorEsc.x * Screen.width / 100;
            _r.height = ganadorEsc.y * Screen.height / 100;
            _r.x = ganadorPos.x * Screen.width / 100;
            _r.y = ganadorPos.y * Screen.height / 100;

            //if(PrimerImaParp)//para que parpadee
            GUI.Box(_r, "");
        }

        public void DesaparecerGUI()
        {
            activadoAnims = false;
            _tempo = -100;
        }
    }
}