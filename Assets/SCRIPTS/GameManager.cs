using System;
using System.Collections.Generic;
using Prefabs.Deposito;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum EstadoJuego
    {
        Calibrando,
        Jugando,
        Finalizado
    }
    //public static Player[] Jugadoers;

    public static GameManager Instancia;

    public float tiempoDeJuego = 60;
    public EstadoJuego estAct = EstadoJuego.Calibrando;

    public PlayerInfo playerInfo1;
    public PlayerInfo playerInfo2;

    public Player player1;
    public Player player2;

    //mueve los esqueletos para usar siempre los mismos
    public Transform esqueleto1;

    public Transform esqueleto2;

    //public Vector3[] PosEsqsCalib;
    public Vector3[] posEsqsCarrera;
    public Rect conteoPosEsc;
    public float conteoParaInicion = 3;
    public GUISkin gsConteoInicio;

    public Rect tiempoGUI;
    public GUISkin gsTiempoGUI;

    public float tiempEspMuestraPts = 3;

    //posiciones de los camiones dependientes del lado que les toco en la pantalla
    //la pos 0 es para la izquierda y la 1 para la derecha
    public Vector3[] posCamionesCarrera = new Vector3[2];

    //posiciones de los camiones para el tutorial
    public Vector3 posCamion1Tuto = Vector3.zero;
    public Vector3 posCamion2Tuto = Vector3.zero;

    //listas de GO que activa y desactiva por sub-escena
    //escena de calibracion
    public GameObject[] objsCalibracion1;

    public GameObject[] objsCalibracion2;

    //escena de tutorial
    public GameObject[] objsTuto1;

    public GameObject[] objsTuto2;

    //la pista de carreras
    public GameObject[] objsCarrera;

    private bool _conteoRedresivo = true;

    private bool _posSeteada;

    private Rect _r;
    //de las descargas se encarga el controlador de descargas

    //para saber que el los ultimos 5 o 10 segs se cambie de tamaño la font del tiempo
    //bool SeteadoNuevaFontSize = false;
    //int TamOrigFont = 75;
    //int TamNuevoFont = 75;

    /*
    //para el testing
    public float DistanciaRecorrida = 0;
    public float TiempoTranscurrido = 0;
    */

    private IList<int> _users;

    //--------------------------------------------------------//

    private void Awake()
    {
        Instancia = this;
    }

    private void Start()
    {
        IniciarCalibracion();

        //para testing
        //PosCamionesCarrera[0].x+=100;
        //PosCamionesCarrera[1].x+=100;
    }

    private void Update()
    {
        //REINICIAR
        if (Input.GetKey(KeyCode.Mouse1) &&
            Input.GetKey(KeyCode.Keypad0))
            Application.LoadLevel(Application.loadedLevel);

        //CIERRA LA APLICACION
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();


        switch (estAct)
        {
            case EstadoJuego.Calibrando:

                //SKIP EL TUTORIAL
                if (Input.GetKey(KeyCode.Mouse0) &&
                    Input.GetKey(KeyCode.Keypad0))
                    if (playerInfo1 != null && playerInfo2 != null)
                    {
                        FinCalibracion(0);
                        FinCalibracion(1);

                        FinTutorial(0);
                        FinTutorial(1);
                    }

                if (playerInfo1.pj == null && Input.GetKeyDown(KeyCode.W))
                {
                    playerInfo1 = new PlayerInfo(0, player1);
                    playerInfo1.ladoAct = Visualizacion.Lado.Izq;
                    SetPosicion(playerInfo1);
                }

                if (playerInfo2.pj == null && Input.GetKeyDown(KeyCode.UpArrow))
                {
                    playerInfo2 = new PlayerInfo(1, player2);
                    playerInfo2.ladoAct = Visualizacion.Lado.Der;
                    SetPosicion(playerInfo2);
                }

                //cuando los 2 pj terminaron los tutoriales empiesa la carrera
                if (playerInfo1.pj != null && playerInfo2.pj != null)
                    if (playerInfo1.finTuto2 && playerInfo2.finTuto2)
                        EmpezarCarrera();

                break;


            case EstadoJuego.Jugando:

                //SKIP LA CARRERA
                if (Input.GetKey(KeyCode.Mouse1) &&
                    Input.GetKey(KeyCode.Keypad0))
                    tiempoDeJuego = 0;

                if (tiempoDeJuego <= 0) FinalizarCarrera();

                /*
                //para testing
                TiempoTranscurrido += T.GetDT();
                DistanciaRecorrida += (Player1.transform.position - PosCamionesCarrera[0]).magnitude;
                */

                if (_conteoRedresivo)
                {
                    //se asegura de que los vehiculos se queden inmobiles
                    //Player1.rigidbody.velocity = Vector3.zero;
                    //Player2.rigidbody.velocity = Vector3.zero;

                    conteoParaInicion -= T.GetDT();
                    if (conteoParaInicion < 0)
                    {
                        EmpezarCarrera();
                        _conteoRedresivo = false;
                    }
                }
                else
                {
                    //baja el tiempo del juego
                    tiempoDeJuego -= T.GetDT();
                    if (tiempoDeJuego <= 0)
                    {
                        //termina el juego
                    }
                    /*
                    //otro tamaño
                    if(!SeteadoNuevaFontSize && TiempoDeJuego <= 5)
                    {
                        SeteadoNuevaFontSize = true;
                        GS_TiempoGUI.box.fontSize = TamNuevoFont;
                        GS_TiempoGUI.box.normal.textColor = Color.red;
                    }
                    */
                }

                break;


            case EstadoJuego.Finalizado:

                //nada de trakeo con kinect, solo se muestra el puntaje
                //tambien se puede hacer alguna animacion, es el tiempo previo a la muestra de pts

                tiempEspMuestraPts -= Time.deltaTime;
                if (tiempEspMuestraPts <= 0)
                    Application.LoadLevel(Application.loadedLevel + 1);

                break;
        }
    }

    private void OnGUI()
    {
        switch (estAct)
        {
            case EstadoJuego.Jugando:
                if (_conteoRedresivo)
                {
                    GUI.skin = gsConteoInicio;

                    _r.x = conteoPosEsc.x * Screen.width / 100;
                    _r.y = conteoPosEsc.y * Screen.height / 100;
                    _r.width = conteoPosEsc.width * Screen.width / 100;
                    _r.height = conteoPosEsc.height * Screen.height / 100;

                    if (conteoParaInicion > 1)
                        GUI.Box(_r, conteoParaInicion.ToString("0"));
                    else
                        GUI.Box(_r, "GO");
                }

                GUI.skin = gsTiempoGUI;
                _r.x = tiempoGUI.x * Screen.width / 100;
                _r.y = tiempoGUI.y * Screen.height / 100;
                _r.width = tiempoGUI.width * Screen.width / 100;
                _r.height = tiempoGUI.height * Screen.height / 100;
                GUI.Box(_r, tiempoDeJuego.ToString("00"));
                break;
        }

        GUI.skin = null;
    }

    //----------------------------------------------------------//

    public void IniciarCalibracion()
    {
        for (int i = 0; i < objsCalibracion1.Length; i++)
        {
            objsCalibracion1[i].SetActiveRecursively(true);
            objsCalibracion2[i].SetActiveRecursively(true);
        }

        for (int i = 0; i < objsTuto2.Length; i++)
        {
            objsTuto2[i].SetActiveRecursively(false);
            objsTuto1[i].SetActiveRecursively(false);
        }

        for (int i = 0; i < objsCarrera.Length; i++) objsCarrera[i].SetActiveRecursively(false);


        player1.CambiarACalibracion();
        player2.CambiarACalibracion();
    }

    /*
    public void CambiarADescarga(Player pj)
    {
        //en la escena de la pista, activa la camara y las demas propiedades
        //de la escena de descarga
    }

    public void CambiarAPista(Player pj)//de descarga ala pista de vuelta
    {
        //lo mismo pero al revez
    }
    */

    private void CambiarATutorial()
    {
        playerInfo1.finCalibrado = true;

        for (int i = 0; i < objsTuto1.Length; i++) objsTuto1[i].SetActiveRecursively(true);

        for (int i = 0; i < objsCalibracion1.Length; i++) objsCalibracion1[i].SetActiveRecursively(false);
        player1.GetComponent<Frenado>().Frenar();
        player1.CambiarATutorial();
        player1.gameObject.transform.position = posCamion1Tuto; //posiciona el camion
        player1.transform.forward = Vector3.forward;


        playerInfo2.finCalibrado = true;

        for (int i = 0; i < objsCalibracion2.Length; i++) objsCalibracion2[i].SetActiveRecursively(false);

        for (int i = 0; i < objsTuto2.Length; i++) objsTuto2[i].SetActiveRecursively(true);
        player2.GetComponent<Frenado>().Frenar();
        player2.gameObject.transform.position = posCamion2Tuto;
        player2.CambiarATutorial();
        player2.transform.forward = Vector3.forward;
    }

    private void EmpezarCarrera()
    {
        player1.GetComponent<Frenado>().RestaurarVel();
        player1.GetComponent<ControlDireccion>().habilitado = true;

        player2.GetComponent<Frenado>().RestaurarVel();
        player2.GetComponent<ControlDireccion>().habilitado = true;
    }

    private void FinalizarCarrera()
    {
        estAct = EstadoJuego.Finalizado;

        tiempoDeJuego = 0;

        if (player1.dinero > player2.dinero)
        {
            //lado que gano
            if (playerInfo1.ladoAct == Visualizacion.Lado.Der)
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
            else
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;

            //puntajes
            DatosPartida.PtsGanador = player1.dinero;
            DatosPartida.PtsPerdedor = player2.dinero;
        }
        else
        {
            //lado que gano
            if (playerInfo2.ladoAct == Visualizacion.Lado.Der)
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
            else
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;

            //puntajes
            DatosPartida.PtsGanador = player2.dinero;
            DatosPartida.PtsPerdedor = player1.dinero;
        }

        player1.GetComponent<Frenado>().Frenar();
        player2.GetComponent<Frenado>().Frenar();

        player1.contrDesc.FinDelJuego();
        player2.contrDesc.FinDelJuego();
    }

    /*
    public static ControladorDeDescarga GetContrDesc(int pjID)
    {
        switch (pjID)
        {
        case 1:
            return ContrDesc1;
            break;

        case 2:
            return ContrDesc2;
            break;
        }
        return null;
    }*/

    //se encarga de posicionar la camara derecha para el jugador que esta a la derecha y viseversa
    private void SetPosicion(PlayerInfo pjInf)
    {
        pjInf.pj.GetComponent<Visualizacion>().SetLado(pjInf.ladoAct);
        //en este momento, solo la primera vez, deberia setear la otra camara asi no se superponen
        pjInf.pj.contrCalib.IniciarTesteo();
        _posSeteada = true;


        if (pjInf.pj == player1)
        {
            if (pjInf.ladoAct == Visualizacion.Lado.Izq)
                player2.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Der);
            else
                player2.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Izq);
        }
        else
        {
            if (pjInf.ladoAct == Visualizacion.Lado.Izq)
                player1.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Der);
            else
                player1.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Izq);
        }
    }

    private void CambiarACarrera()
    {
        //Debug.Log("CambiarACarrera()");

        esqueleto1.transform.position = posEsqsCarrera[0];
        esqueleto2.transform.position = posEsqsCarrera[1];

        for (int i = 0; i < objsCarrera.Length; i++) objsCarrera[i].SetActiveRecursively(true);

        /*
        for(int i = 0; i < ObjsTuto1.Length; i++)
        {
            ObjsTuto1[i].SetActiveRecursively(false);
            ObjsTuto2[i].SetActiveRecursively(false);
        }
        */


        //desactivacion de la calibracion
        playerInfo1.finCalibrado = true;

        for (int i = 0; i < objsTuto1.Length; i++) objsTuto1[i].SetActiveRecursively(true);

        for (int i = 0; i < objsCalibracion1.Length; i++) objsCalibracion1[i].SetActiveRecursively(false);

        playerInfo2.finCalibrado = true;

        for (int i = 0; i < objsCalibracion2.Length; i++) objsCalibracion2[i].SetActiveRecursively(false);

        for (int i = 0; i < objsTuto2.Length; i++) objsTuto2[i].SetActiveRecursively(true);


        //posiciona los camiones dependiendo de que lado de la pantalla esten
        if (playerInfo1.ladoAct == Visualizacion.Lado.Izq)
        {
            player1.gameObject.transform.position = posCamionesCarrera[0];
            player2.gameObject.transform.position = posCamionesCarrera[1];
        }
        else
        {
            player1.gameObject.transform.position = posCamionesCarrera[1];
            player2.gameObject.transform.position = posCamionesCarrera[0];
        }

        player1.transform.forward = Vector3.forward;
        player1.GetComponent<Frenado>().Frenar();
        player1.CambiarAConduccion();

        player2.transform.forward = Vector3.forward;
        player2.GetComponent<Frenado>().Frenar();
        player2.CambiarAConduccion();

        //los deja andando
        player1.GetComponent<Frenado>().RestaurarVel();
        player2.GetComponent<Frenado>().RestaurarVel();
        //cancela la direccion
        player1.GetComponent<ControlDireccion>().habilitado = false;
        player2.GetComponent<ControlDireccion>().habilitado = false;
        //les de direccion
        player1.transform.forward = Vector3.forward;
        player2.transform.forward = Vector3.forward;

        estAct = EstadoJuego.Jugando;
    }

    public void FinTutorial(int playerID)
    {
        if (playerID == 0)
            playerInfo1.finTuto2 = true;
        else if (playerID == 1) playerInfo2.finTuto2 = true;

        if (playerInfo1.finTuto2 && playerInfo2.finTuto2) CambiarACarrera();
    }

    public void FinCalibracion(int playerID)
    {
        if (playerID == 0)
            playerInfo1.finTuto1 = true;
        else if (playerID == 1) playerInfo2.finTuto1 = true;

        if (playerInfo1.pj != null && playerInfo2.pj != null)
            if (playerInfo1.finTuto1 && playerInfo2.finTuto1)
                CambiarACarrera(); //CambiarATutorial();
    }


    [Serializable]
    public class PlayerInfo
    {
        public bool finCalibrado;
        public bool finTuto1;
        public bool finTuto2;

        public Visualizacion.Lado ladoAct;

        public int tipoDeInput = -1;

        public Player pj;

        public PlayerInfo(int tipoDeInput, Player pj)
        {
            this.tipoDeInput = tipoDeInput;
            this.pj = pj;
        }
    }
}