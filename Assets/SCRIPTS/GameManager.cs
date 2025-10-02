using System;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia; // Kept original name

    [Header("Game Settings")] public float TiempoJuego = 60f; // Kept original name
    public GameMode gameMode = GameMode.TwoPlayer;

    [Header("Countdown Settings")] public float TiempoCuentaRegresiva = 3f; // Kept original name
    public Rect PosEnPantallaCuentaRegresiva; // Kept original name
    public GUISkin GUISkinConteo; // Kept original name

    [Header("Timer UI")] public Rect RectGUITiempo = new Rect(); // Kept original name
    public GUISkin GUISkinTiempo; // Kept original name

    [Header("Players")] public Player Jugador1; // Kept original name
    public Player Jugador2; // Kept original name

    [Header("Skeletons")] public Transform Esqueleto1; // Kept original name
    public Transform Esqueleto2; // Kept original name
    public Vector3[] PosicionesEsqueletoCarrera; // Kept original name

    [Header("Vehicle Positions")] public Vector3[] PosicionesVehiculoCarrera = new Vector3[2]; // Kept original name
    public Vector3 PosicionVehiculo1Tutorial = Vector3.zero; // Kept original name
    public Vector3 PosicionVehiculo2Tutorial = Vector3.zero; // Kept original name

    [Header("Scene Objects")] public GameObject[] ObjetosCalibracion1; // Kept original name
    public GameObject[] ObjetosCalibracion2; // Kept original name
    public GameObject[] ObjetosTutorial1; // Kept original name
    public GameObject[] ObjetosTutorial2; // Kept original name
    public GameObject[] ObjetosCarrera; // Kept original name

    [Serializable]
    public enum GameMode
    {
        OnePlayer,
        TwoPlayer
    }

    public enum GameState
    {
        Calibrating,
        Playing,
        Finished
    }

    private GameState currentState = GameState.Calibrating;
    private PlayerInfo playerInfo1;
    private PlayerInfo playerInfo2;
    private bool isCountingDown = true;
    private float currentCountdown;
    private float scoreDisplayTime = 3f;
    private Rect tempRect = new Rect();

    void Awake()
    {
        Instancia = this;
        InitializePlayers();
    }

    void Start()
    {
        StartCalibration();
        currentCountdown = TiempoCuentaRegresiva;
    }

    void Update()
    {
        HandleInputs();
        UpdateGameState();
    }

    void OnGUI()
    {
        switch (currentState)
        {
            case GameState.Playing:
                DrawCountdownOrTimer();
                break;
        }

        GUI.skin = null;
    }

    private void InitializePlayers()
    {
        playerInfo1 = new PlayerInfo(0, Jugador1);

        if (gameMode == GameMode.TwoPlayer)
        {
            playerInfo2 = new PlayerInfo(1, Jugador2);
        }
    }

    private void HandleInputs()
    {
        // Restart game
        if (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.Keypad0))
        {
            RestartGame();
        }

        // Quit application
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        HandleStateSpecificInputs();
    }

    private void HandleStateSpecificInputs()
    {
        switch (currentState)
        {
            case GameState.Calibrating:
                HandleCalibrationInputs();
                break;
            case GameState.Playing:
                HandleRaceInputs();
                break;
        }
    }

    private void HandleCalibrationInputs()
    {
        // Skip tutorial
        if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Keypad0))
        {
            SkipTutorial();
        }

        // Player 1 calibration
        if (playerInfo1.Player == null && Input.GetKeyDown(KeyCode.W))
        {
            playerInfo1 = new PlayerInfo(0, Jugador1);
            playerInfo1.ScreenSide = VisualizationSide.Left;
            SetPlayerPosition(playerInfo1);
        }

        // Player 2 calibration (only in two-player mode)
        if (gameMode == GameMode.TwoPlayer &&
            playerInfo2.Player == null &&
            Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerInfo2 = new PlayerInfo(1, Jugador2);
            playerInfo2.ScreenSide = VisualizationSide.Right;
            SetPlayerPosition(playerInfo2);
        }

        CheckCalibrationComplete();
    }

    private void HandleRaceInputs()
    {
        // Skip race
        if (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.Keypad0))
        {
            TiempoJuego = 0;
        }
    }

    private void UpdateGameState()
    {
        switch (currentState)
        {
            case GameState.Calibrating:
                break;
            case GameState.Playing:
                UpdateRace();
                break;
            case GameState.Finished:
                UpdateFinished();
                break;
        }
    }

    private void UpdateRace()
    {
        if (TiempoJuego <= 0)
        {
            EndRace();
            return;
        }

        if (isCountingDown)
        {
            UpdateCountdown();
        }
        else
        {
            TiempoJuego -= Time.deltaTime;
        }
    }

    private void UpdateCountdown()
    {
        currentCountdown -= Time.deltaTime;

        if (currentCountdown <= 0)
        {
            StartRace();
            isCountingDown = false;
        }
    }

    private void UpdateFinished()
    {
        scoreDisplayTime -= Time.deltaTime;
        if (scoreDisplayTime <= 0)
        {
            LoadNextLevel();
        }
    }

    private void CheckCalibrationComplete()
    {
        bool player1Ready = playerInfo1.Player != null && playerInfo1.TutorialComplete;
        bool player2Ready = gameMode == GameMode.OnePlayer ||
                            (playerInfo2.Player != null && playerInfo2.TutorialComplete);

        if (player1Ready && player2Ready)
        {
            StartRaceSequence();
        }
    }

    private void SkipTutorial()
    {
        if (playerInfo1?.Player != null)
        {
            FinCalibracion(0);
            FinTutorial(0);
        }

        if (gameMode == GameMode.TwoPlayer && playerInfo2?.Player != null)
        {
            FinCalibracion(1);
            FinTutorial(1);
        }
    }

    private void StartCalibration()
    {
        SetObjectsActive(ObjetosCalibracion1, true);
        SetObjectsActive(ObjetosTutorial1, false);
        SetObjectsActive(ObjetosCarrera, false);

        if (gameMode == GameMode.TwoPlayer)
        {
            SetObjectsActive(ObjetosCalibracion2, true);
            SetObjectsActive(ObjetosTutorial2, false);
        }

        Jugador1.CambiarACalibracion();
        if (gameMode == GameMode.TwoPlayer)
        {
            Jugador2.CambiarACalibracion();
        }
    }

    private void StartRaceSequence()
    {
        SwitchToRace();
    }

    private void SwitchToRace()
    {
        // Position skeletons
        if (PosicionesEsqueletoCarrera.Length >= 2)
        {
            Esqueleto1.transform.position = PosicionesEsqueletoCarrera[0];
            if (gameMode == GameMode.TwoPlayer)
            {
                Esqueleto2.transform.position = PosicionesEsqueletoCarrera[1];
            }
        }

        // Activate race objects
        SetObjectsActive(ObjetosCarrera, true);
        SetObjectsActive(ObjetosCalibracion1, false);
        SetObjectsActive(ObjetosTutorial1, false);

        if (gameMode == GameMode.TwoPlayer)
        {
            SetObjectsActive(ObjetosCalibracion2, false);
            SetObjectsActive(ObjetosTutorial2, false);
        }

        // Position vehicles
        PositionVehiclesForRace();

        // Setup players for racing
        SetupPlayerForRace(Jugador1);
        if (gameMode == GameMode.TwoPlayer)
        {
            SetupPlayerForRace(Jugador2);
        }

        currentState = GameState.Playing;
        isCountingDown = true;
        currentCountdown = TiempoCuentaRegresiva;
    }

    private void PositionVehiclesForRace()
    {
        if (gameMode == GameMode.OnePlayer)
        {
            Jugador1.transform.position = PosicionesVehiculoCarrera[0];
            Jugador1.transform.forward = Vector3.forward;
        }
        else
        {
            bool player1Left = playerInfo1.ScreenSide == VisualizationSide.Left;
            Jugador1.transform.position = PosicionesVehiculoCarrera[player1Left ? 0 : 1];
            Jugador2.transform.position = PosicionesVehiculoCarrera[player1Left ? 1 : 0];

            Jugador1.transform.forward = Vector3.forward;
            Jugador2.transform.forward = Vector3.forward;
        }
    }

    private void SetupPlayerForRace(Player player)
    {
        Frenado braking = player.GetComponent<Frenado>();
        ControlDireccion steering = player.GetComponent<ControlDireccion>();

        braking?.Frenar();
        player.CambiarAConduccion();
        braking?.RestaurarVel();

        if (steering)
        {
            steering.enabled = false;
        }
    }

    private void StartRace()
    {
        ControlDireccion player1Steering = Jugador1.GetComponent<ControlDireccion>();
        if (player1Steering)
        {
            player1Steering.enabled = true;
        }

        if (gameMode == GameMode.TwoPlayer)
        {
            ControlDireccion player2Steering = Jugador2.GetComponent<ControlDireccion>();
            if (player2Steering)
            {
                player2Steering.enabled = true;
            }
        }
    }

    private void EndRace()
    {
        currentState = GameState.Finished;
        TiempoJuego = 0;

        // Determine winner and save results
        DetermineWinner();

        // Stop players
        Jugador1.GetComponent<Frenado>()?.Frenar();
        Jugador1.ContrDesc.FinDelJuego();

        if (gameMode == GameMode.TwoPlayer)
        {
            Jugador2.GetComponent<Frenado>()?.Frenar();
            Jugador2.ContrDesc.FinDelJuego();
        }
    }

    private void DetermineWinner()
    {
        if (gameMode == GameMode.OnePlayer)
        {
            // In single player, just save the score
            DatosPartida.PtsGanador = Jugador1.Dinero;
            DatosPartida.PtsPerdedor = 0;
        }
        else
        {
            // Two player mode
            if (Jugador1.Dinero > Jugador2.Dinero)
            {
                SetWinnerData(playerInfo1, Jugador1.Dinero, Jugador2.Dinero);
            }
            else
            {
                SetWinnerData(playerInfo2, Jugador2.Dinero, Jugador1.Dinero);
            }
        }
    }

    private void SetWinnerData(PlayerInfo winner, int winnerScore, int loserScore)
    {
        DatosPartida.LadoGanadaor = winner.ScreenSide == VisualizationSide.Right
            ? DatosPartida.Lados.Der
            : DatosPartida.Lados.Izq;
        DatosPartida.PtsGanador = winnerScore;
        DatosPartida.PtsPerdedor = loserScore;
    }

    private void DrawCountdownOrTimer()
    {
        if (isCountingDown)
        {
            DrawCountdown();
        }

        DrawTimer();
    }

    private void DrawCountdown()
    {
        GUI.skin = GUISkinConteo;

        tempRect.x = PosEnPantallaCuentaRegresiva.x * Screen.width / 100;
        tempRect.y = PosEnPantallaCuentaRegresiva.y * Screen.height / 100;
        tempRect.width = PosEnPantallaCuentaRegresiva.width * Screen.width / 100;
        tempRect.height = PosEnPantallaCuentaRegresiva.height * Screen.height / 100;

        string countdownText = currentCountdown > 1 ? currentCountdown.ToString("0") : "GO";
        GUI.Box(tempRect, countdownText);
    }

    private void DrawTimer()
    {
        GUI.skin = GUISkinTiempo;

        tempRect.x = RectGUITiempo.x * Screen.width / 100;
        tempRect.y = RectGUITiempo.y * Screen.height / 100;
        tempRect.width = RectGUITiempo.width * Screen.width / 100;
        tempRect.height = RectGUITiempo.height * Screen.height / 100;

        GUI.Box(tempRect, TiempoJuego.ToString("00"));
    }

    private void SetObjectsActive(GameObject[] objects, bool active)
    {
        if (objects == null) return;

        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(active);
            }
        }
    }

    private void SetPlayerPosition(PlayerInfo playerInfo)
    {
        Visualizacion visualization = playerInfo.Player.GetComponent<Visualizacion>();
        visualization?.SetLado(playerInfo.ScreenSide);
        playerInfo.Player.ContrCalib.IniciarTesteo();

        // Set the other player's side
        if (gameMode == GameMode.TwoPlayer)
        {
            Player otherPlayer = playerInfo.Player == Jugador1 ? Jugador2 : Jugador1;
            VisualizationSide otherSide = playerInfo.ScreenSide == VisualizationSide.Left
                ? VisualizationSide.Right
                : VisualizationSide.Left;
            otherPlayer.GetComponent<Visualizacion>()?.SetLado(otherSide);
        }
    }

    public void FinTutorial(int playerID)
    {
        if (playerID == 0)
        {
            playerInfo1.TutorialComplete = true;
        }
        else if (playerID == 1 && gameMode == GameMode.TwoPlayer)
        {
            playerInfo2.TutorialComplete = true;
        }

        CheckCalibrationComplete();
    }

    public void FinCalibracion(int playerID)
    {
        if (playerID == 0)
        {
            playerInfo1.CalibrationComplete = true;
        }
        else if (playerID == 1 && gameMode == GameMode.TwoPlayer)
        {
            playerInfo2.CalibrationComplete = true;
        }

        CheckCalibrationComplete();
    }

    private void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    private void LoadNextLevel()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public enum VisualizationSide
    {
        Left,
        Right
    }

    [System.Serializable]
    public class PlayerInfo
    {
        public PlayerInfo(int inputType, Player player)
        {
            InputType = inputType;
            Player = player;
        }

        public bool CalibrationComplete = false;
        public bool TutorialComplete = false;
        public VisualizationSide ScreenSide;
        public int InputType = -1;
        public Player Player;
    }
}