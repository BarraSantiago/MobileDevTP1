using UnityEngine;

public class Aceleracion : MonoBehaviour
{
    public Transform ManoDer;
    public Transform ManoIzq;

    public float AlturaMedia; //valor en eje Y que calibra el 0 de cada pedal

    public float SensAcel = 1;
    public float SensFren = 1;

    public Transform Camion; //lo que va a conducir

    //pedales
    public Transform PedalAcel;
    public Transform PedalFren;
    public float SensivPed = 1;
    private float Acelerado;
    private float DifDer;


    private float DifIzq;

    private float Frenado;
    private Vector3 PAclPosIni;
    private Vector3 PFrnPosIni;

    //---------------------------------------------------------//

    // Use this for initialization
    private void Start()
    {
        PAclPosIni = PedalAcel.localPosition;
        PFrnPosIni = PedalFren.localPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        DifDer = ManoDer.position.y - AlturaMedia;
        DifIzq = ManoIzq.position.y - AlturaMedia;

        //acelerar
        if (DifDer > 0)
        {
            Acelerado = DifDer * SensAcel * Time.deltaTime;

            Camion.position += Acelerado * Camion.forward;

            PedalAcel.localPosition = PAclPosIni - PedalAcel.forward * SensivPed * Acelerado;
        }

        //PedalFren.localPosition = PAclPosIni;
        //frenar
        if (DifIzq > 0)
        {
            Frenado = DifIzq * SensFren * Time.deltaTime;

            Camion.position -= Frenado * Camion.forward;

            PedalFren.localPosition = PFrnPosIni - PedalFren.forward * SensivPed * Frenado;
        }
        //PedalFren.localPosition = PFrnPosIni;
    }
}