using EscenaDescarga;
using Escenas.Juego.Calibracion;
using Escenas.Juego.Tutorial;
using Prefabs.Bolsas;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Estados
    {
        EnDescarga,
        EnConduccion,
        EnCalibracion,
        EnTutorial
    }

    public int dinero;
    public int idPlayer;

    public Bolsa[] bolasas;
    public string tagBolsas = "";
    public Estados estAct = Estados.EnConduccion;

    public bool enConduccion = true;
    public bool enDescarga;

    public ControladorDeDescarga contrDesc;
    public ContrCalibracion contrCalib;
    public ContrTutorial contrTuto;
    private int _cantBolsAct;

    private Visualizacion _miVisualizacion;

    //------------------------------------------------------------------//

    // Use this for initialization
    private void Start()
    {
        for (int i = 0; i < bolasas.Length; i++)
            bolasas[i] = null;

        _miVisualizacion = GetComponent<Visualizacion>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //------------------------------------------------------------------//

    public bool AgregarBolsa(Bolsa b)
    {
        if (_cantBolsAct + 1 <= bolasas.Length)
        {
            bolasas[_cantBolsAct] = b;
            _cantBolsAct++;
            dinero += (int)b.monto;
            b.Desaparecer();
            return true;
        }

        return false;
    }

    public void VaciarInv()
    {
        for (int i = 0; i < bolasas.Length; i++)
            bolasas[i] = null;

        _cantBolsAct = 0;
    }

    public bool ConBolasas()
    {
        for (int i = 0; i < bolasas.Length; i++)
            if (bolasas[i] != null)
                return true;

        return false;
    }

    public void SetContrDesc(ControladorDeDescarga contr)
    {
        contrDesc = contr;
    }

    public ControladorDeDescarga GetContr()
    {
        return contrDesc;
    }

    public void CambiarACalibracion()
    {
        _miVisualizacion.CambiarACalibracion();
        estAct = Estados.EnCalibracion;
    }

    public void CambiarATutorial()
    {
        _miVisualizacion.CambiarATutorial();
        estAct = Estados.EnTutorial;
        contrTuto.Iniciar();
    }

    public void CambiarAConduccion()
    {
        _miVisualizacion.CambiarAConduccion();
        estAct = Estados.EnConduccion;
    }

    public void CambiarADescarga()
    {
        _miVisualizacion.CambiarADescarga();
        estAct = Estados.EnDescarga;
    }

    public void SacarBolasa()
    {
        for (int i = 0; i < bolasas.Length; i++)
            if (bolasas[i] != null)
            {
                bolasas[i] = null;
                return;
            }
    }
}