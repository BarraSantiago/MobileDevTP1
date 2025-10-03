using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager Instancia;
    public float TiempoDeJuego = 10;

    public enum EstadoJuego { Calibrando, Jugando, Finalizado }
    public EstadoJuego EstAct = EstadoJuego.Calibrando;

    [SerializeField] private Player player1;
    [SerializeField] private Player player2;

    public static Action OnEndgame;

    bool ConteoRedresivo = true;
    public Rect ConteoPosEsc;
    public float ConteoParaInicion = 3;
    public Text ConteoInicio;
    public Text TiempoDeJuegoText;

    public float TiempEspMuestraPts = 3;

    [SerializeField] private Joystick j1;
    [SerializeField] private Joystick j2;

    [SerializeField] private GameObject boxes;
    [SerializeField] private GameObject taxis;


    //posiciones de los camiones dependientes del lado que les toco en la pantalla
    //la pos 0 es para la izquierda y la 1 para la derecha
    public Vector3[] PosCamionesCarrera = new Vector3[2];
    //posiciones de los camiones para el tutorial
    public Vector3 PosCamion1Tuto = Vector3.zero;
    public Vector3 PosCamion2Tuto = Vector3.zero;
    public Player Player1
    {
        get { return player1; }
    }

    public Player Player2
    {
        get { return player2; }
    }
    //listas de GO que activa y desactiva por sub-escena
    //escena de tutorial
    public GameObject[] ObjsCalibracion1;
    public GameObject[] ObjsCalibracion2;
    //la pista de carreras
    public GameObject[] ObjsCarrera;
    [SerializeField] DifficultyScriptableObject difficulty;
    [SerializeField] MultiplayerScriptableObject multiplayer;

    //--------------------------------------------------------//

    void Awake() {
        GameManager.Instancia = this;
    }

    IEnumerator Start() {
        yield return null;
#if !UNITY_ANDROID
        j1.transform.parent.gameObject.SetActive(false);
        if(multiplayer.isMultiplayer)
            j2.transform.parent.gameObject.SetActive(false);
#endif
        SetDifficulty();
        IniciarTutorial();
    }

    private void SetDifficulty()
    {
        if(difficulty.currentDifficulty == Difficulty.NORMAL)
        {
            boxes.SetActive(true);
        }
        else if (difficulty.currentDifficulty == Difficulty.HARD)
        {
            boxes.SetActive(true);
            taxis.SetActive(true);
        }
    }

    private void Update() {
        switch (EstAct) {
#if UNITY_ANDROID
        case EstadoJuego.Calibrando:

                if (j1.Vertical > 0.85f) {
                    Player1.Seleccionado = true;
                }

                if (multiplayer.isMultiplayer)
                {
                    if (j2.Vertical > 0.85f)
                    {
                        Player2.Seleccionado = true;
                    }
                }
                break;
#endif
#if !UNITY_ANDROID
            case EstadoJuego.Calibrando:

                if (Input.GetKeyDown(KeyCode.W)) {
                    Player1.Seleccionado = true;
                }

                if (multiplayer.isMultiplayer)
                {
                if (Input.GetKeyDown(KeyCode.UpArrow)) {
                    Player2.Seleccionado = true;
                }
                }

                break;
#endif

            case EstadoJuego.Jugando:

                if (TiempoDeJuego <= 0) {
                    FinalizarCarrera();
                }

                if (ConteoRedresivo) {
                    ConteoParaInicion -= T.GetDT();
                    if (ConteoParaInicion < 0) {
                        EmpezarCarrera();
                        ConteoRedresivo = false;
                    }
                }
                else {
                    //baja el tiempo del juego
                    TiempoDeJuego -= T.GetDT();
                }
                if (ConteoRedresivo) {
                    if (ConteoParaInicion > 1) {
                        ConteoInicio.text = ConteoParaInicion.ToString("0");
                    }
                    else {
                        ConteoInicio.text = "GO";
                    }
                }

                ConteoInicio.gameObject.SetActive(ConteoRedresivo);

                TiempoDeJuegoText.text = TiempoDeJuego.ToString("00");

                break;

            case EstadoJuego.Finalizado:

                //muestra el puntaje

                TiempEspMuestraPts -= Time.deltaTime;
                if (TiempEspMuestraPts <= 0)
                    OnEndgame();

                break;
        }

        TiempoDeJuegoText.transform.parent.gameObject.SetActive(EstAct == EstadoJuego.Jugando && !ConteoRedresivo);
    }

    //----------------------------------------------------------//

    public void IniciarTutorial() {
        for (int i = 0; i < ObjsCalibracion1.Length; i++) {
            ObjsCalibracion1[i].SetActive(true);
            if(multiplayer.isMultiplayer)
                ObjsCalibracion2[i].SetActive(true);
        }

        for (int i = 0; i < ObjsCarrera.Length; i++) {
            ObjsCarrera[i].SetActive(false);
        }

        Player1.CambiarATutorial();
        if(multiplayer.isMultiplayer)
            Player2.CambiarATutorial();

        TiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        ConteoInicio.gameObject.SetActive(false);
    }

    void EmpezarCarrera() {
        Player1.GetComponent<Frenado>().RestaurarVel();
        Player1.GetComponent<ControlDireccion>().Habilitado = true;

        if (multiplayer.isMultiplayer)
        {
            Player2.GetComponent<Frenado>().RestaurarVel();
            Player2.GetComponent<ControlDireccion>().Habilitado = true;
        }   
    }

    void FinalizarCarrera() {
        EstAct = GameManager.EstadoJuego.Finalizado;

        TiempoDeJuego = 0;

        MakePointsSnapshot();
        

        Player1.GetComponent<Frenado>().Frenar();
        if(multiplayer.isMultiplayer)
            Player2.GetComponent<Frenado>().Frenar();
        Player1.ContrDesc.FinDelJuego();
        if (multiplayer.isMultiplayer)
            Player2.ContrDesc.FinDelJuego();
    }

    void CambiarACarrera() {

        EstAct = GameManager.EstadoJuego.Jugando;

        for (int i = 0; i < ObjsCarrera.Length; i++) {
            ObjsCarrera[i].SetActive(true);
        }
        //desactivacion de la calibracion
        Player1.FinCalibrado = true;

        for (int i = 0; i < ObjsCalibracion1.Length; i++) {
            ObjsCalibracion1[i].SetActive(false);
        }
        if (multiplayer.isMultiplayer)
        {
            Player2.FinCalibrado = true;
            for (int i = 0; i < ObjsCalibracion2.Length; i++)
            {
                ObjsCalibracion2[i].SetActive(false);
            }
        }
        if (multiplayer.isMultiplayer)
        {
            if (Player1.LadoActual == Visualizacion.Lado.Izq)
            {
                Player1.gameObject.transform.position = PosCamionesCarrera[0];
                Player2.gameObject.transform.position = PosCamionesCarrera[1];
            }
            else
            {
                Player1.gameObject.transform.position = PosCamionesCarrera[1];
                Player2.gameObject.transform.position = PosCamionesCarrera[0];
            }
        }
        //posiciona los camiones dependiendo de que lado de la pantalla esten
        
        Player1.transform.forward = Vector3.forward;
        Player1.GetComponent<Frenado>().Frenar();
        Player1.CambiarAConduccion();

        if (multiplayer.isMultiplayer)
        {
            Player2.transform.forward = Vector3.forward;
            Player2.GetComponent<Frenado>().Frenar();
            Player2.CambiarAConduccion();
        }
        
        //los deja andando
        Player1.GetComponent<Frenado>().RestaurarVel();
        if (multiplayer.isMultiplayer)
            Player2.GetComponent<Frenado>().RestaurarVel();
        //cancela la direccion
        Player1.GetComponent<ControlDireccion>().Habilitado = false;
        if (multiplayer.isMultiplayer)
            Player2.GetComponent<ControlDireccion>().Habilitado = false;
        //les de direccion
        Player1.transform.forward = Vector3.forward;
        if (multiplayer.isMultiplayer)
            Player2.transform.forward = Vector3.forward;

        TiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        ConteoInicio.gameObject.SetActive(false);
    }

    public void FinCalibracion(int playerID) {
        if (playerID == 0) {
            Player1.FinTuto = true;

        }
        if (playerID == 1) {
            Player2.FinTuto = true;
        }

        if (multiplayer.isMultiplayer)
        {
            if (Player1.FinTuto && Player2.FinTuto)
                CambiarACarrera();
        }
        else
        {
            if (Player1.FinTuto)
            {
                CambiarACarrera();
            }
        }
    }

    void MakePointsSnapshot()
    {
        DatosPartida.player1Points = player1.Dinero;
        if(player2)
            DatosPartida.player2Points = player2.Dinero;
    }
}