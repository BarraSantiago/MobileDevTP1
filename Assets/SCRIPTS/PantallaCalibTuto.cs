using Escenas.Juego.Calibracion;
using UnityEngine;

public class PantallaCalibTuto : MonoBehaviour
{
    public Texture2D[] imagenesDelTuto;
    public float intervalo = 1.2f; //tiempo de cada cuanto cambia de imagen

    public Texture2D[] imagenesDeCalib;

    public Texture2D imaReady;

    public ContrCalibracion contrCalib;
    private int _enCursoCalib;
    private int _enCursoTuto;
    private float _tempoIntCalib;
    private float _tempoIntTuto;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        switch (contrCalib.estAct)
        {
            case ContrCalibracion.Estados.Calibrando:
                //pongase en posicion para iniciar
                _tempoIntCalib += T.GetDT();
                if (_tempoIntCalib >= intervalo)
                {
                    _tempoIntCalib = 0;
                    if (_enCursoCalib + 1 < imagenesDeCalib.Length)
                        _enCursoCalib++;
                    else
                        _enCursoCalib = 0;
                }

                GetComponent<Renderer>().material.mainTexture = imagenesDeCalib[_enCursoCalib];

                break;

            case ContrCalibracion.Estados.Tutorial:
                //tome la bolsa y depositela en el estante
                _tempoIntTuto += T.GetDT();
                if (_tempoIntTuto >= intervalo)
                {
                    _tempoIntTuto = 0;
                    if (_enCursoTuto + 1 < imagenesDelTuto.Length)
                        _enCursoTuto++;
                    else
                        _enCursoTuto = 0;
                }

                GetComponent<Renderer>().material.mainTexture = imagenesDelTuto[_enCursoTuto];

                break;

            case ContrCalibracion.Estados.Finalizado:
                //esperando al otro jugador		
                GetComponent<Renderer>().material.mainTexture = imaReady;

                break;
        }
    }
}