using UnityEngine;

namespace Escenas.PuntosFinales
{
    public class MngPts : MonoBehaviour
    {
        public float TiempEmpAnims = 2.5f;

        public Vector2[] DineroPos;
        public Vector2 DineroEsc;
        public GUISkin GS_Dinero;

        public Vector2 GanadorPos;
        public Vector2 GanadorEsc;
        public Texture2D[] Ganadores;
        public GUISkin GS_Ganador;

        public GameObject Fondo;

        public float TiempEspReiniciar = 10;


        public float TiempParpadeo = 0.7f;

        public bool ActivadoAnims;

        private readonly Visualizacion Viz = new();

        private int IndexGanador = 0;
        private bool PrimerImaParp = true;
        private Rect R;
        private float Tempo;
        private float TempoParpadeo;

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


            TiempEspReiniciar -= Time.deltaTime;
            if (TiempEspReiniciar <= 0) Application.LoadLevel(0);


            if (ActivadoAnims)
            {
                TempoParpadeo += Time.deltaTime;

                if (TempoParpadeo >= TiempParpadeo)
                {
                    TempoParpadeo = 0;

                    if (PrimerImaParp)
                    {
                        PrimerImaParp = false;
                    }
                    else
                    {
                        TempoParpadeo += 0.1f;
                        PrimerImaParp = true;
                    }
                }
            }


            if (!ActivadoAnims)
            {
                Tempo += Time.deltaTime;
                if (Tempo >= TiempEmpAnims)
                {
                    Tempo = 0;
                    ActivadoAnims = true;
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
            if (ActivadoAnims)
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

                    GS_Ganador.box.normal.background = Ganadores[1];

                    break;

                case DatosPartida.Lados.Izq:

                    GS_Ganador.box.normal.background = Ganadores[0];

                    break;
            }
        }

        private void SetDinero()
        {
            GUI.skin = GS_Dinero;

            R.width = DineroEsc.x * Screen.width / 100;
            R.height = DineroEsc.y * Screen.height / 100;


            //IZQUIERDA
            R.x = DineroPos[0].x * Screen.width / 100;
            R.y = DineroPos[0].y * Screen.height / 100;

            if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq) //izquierda
            {
                if (!PrimerImaParp) //para que parpadee
                    GUI.Box(R, "$" + Viz.PrepararNumeros(DatosPartida.PtsGanador));
            }
            else
            {
                GUI.Box(R, "$" + Viz.PrepararNumeros(DatosPartida.PtsPerdedor));
            }


            //DERECHA
            R.x = DineroPos[1].x * Screen.width / 100;
            R.y = DineroPos[1].y * Screen.height / 100;

            if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Der) //derecha
            {
                if (!PrimerImaParp) //para que parpadee
                    GUI.Box(R, "$" + Viz.PrepararNumeros(DatosPartida.PtsGanador));
            }
            else
            {
                GUI.Box(R, "$" + Viz.PrepararNumeros(DatosPartida.PtsPerdedor));
            }
        }

        private void SetCartelGanador()
        {
            GUI.skin = GS_Ganador;

            R.width = GanadorEsc.x * Screen.width / 100;
            R.height = GanadorEsc.y * Screen.height / 100;
            R.x = GanadorPos.x * Screen.width / 100;
            R.y = GanadorPos.y * Screen.height / 100;

            //if(PrimerImaParp)//para que parpadee
            GUI.Box(R, "");
        }

        public void DesaparecerGUI()
        {
            ActivadoAnims = false;
            Tempo = -100;
        }
    }
}