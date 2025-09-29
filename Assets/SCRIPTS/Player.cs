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

    public int Dinero;
    public int IdPlayer;

    public Bolsa[] Bolasas;
    public string TagBolsas = "";
    public Estados EstAct = Estados.EnConduccion;

    public bool EnConduccion = true;
    public bool EnDescarga;

    public ControladorDeDescarga ContrDesc;
    public ContrCalibracion ContrCalib;
    public ContrTutorial ContrTuto;
    private int CantBolsAct;

    private Visualizacion MiVisualizacion;

    //------------------------------------------------------------------//

    // Use this for initialization
    private void Start()
    {
        for (int i = 0; i < Bolasas.Length; i++)
            Bolasas[i] = null;

        MiVisualizacion = GetComponent<Visualizacion>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //------------------------------------------------------------------//

    public bool AgregarBolsa(Bolsa b)
    {
        if (CantBolsAct + 1 <= Bolasas.Length)
        {
            Bolasas[CantBolsAct] = b;
            CantBolsAct++;
            Dinero += (int)b.Monto;
            b.Desaparecer();
            return true;
        }

        return false;
    }

    public void VaciarInv()
    {
        for (int i = 0; i < Bolasas.Length; i++)
            Bolasas[i] = null;

        CantBolsAct = 0;
    }

    public bool ConBolasas()
    {
        for (int i = 0; i < Bolasas.Length; i++)
            if (Bolasas[i] != null)
                return true;

        return false;
    }

    public void SetContrDesc(ControladorDeDescarga contr)
    {
        ContrDesc = contr;
    }

    public ControladorDeDescarga GetContr()
    {
        return ContrDesc;
    }

    public void CambiarACalibracion()
    {
        MiVisualizacion.CambiarACalibracion();
        EstAct = Estados.EnCalibracion;
    }

    public void CambiarATutorial()
    {
        MiVisualizacion.CambiarATutorial();
        EstAct = Estados.EnTutorial;
        ContrTuto.Iniciar();
    }

    public void CambiarAConduccion()
    {
        MiVisualizacion.CambiarAConduccion();
        EstAct = Estados.EnConduccion;
    }

    public void CambiarADescarga()
    {
        MiVisualizacion.CambiarADescarga();
        EstAct = Estados.EnDescarga;
    }

    public void SacarBolasa()
    {
        for (int i = 0; i < Bolasas.Length; i++)
            if (Bolasas[i] != null)
            {
                Bolasas[i] = null;
                return;
            }
    }
}