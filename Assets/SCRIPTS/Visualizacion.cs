using EscenaDescarga;
using Escenas.Juego.Calibracion;
using UnityEngine;

/// <summary>
///     clase encargada de TODA la visualizacion
///     de cada player, todo aquello que corresconda a
///     cada seccion de la pantalla independientemente
/// </summary>
public class Visualizacion : MonoBehaviour
{
    public enum Lado
    {
        Izq,
        Der
    }

    public Lado ladoAct;

    //las distintas camaras
    public Camera camCalibracion;
    public Camera camConduccion;
    public Camera camDescarga;


    //EL DINERO QUE SE TIENE
    public Vector2[] dinPos;
    public Vector2 dinEsc = Vector2.zero;

    public GUISkin gsDin;

    //EL VOLANTE
    public Vector2[] volantePos;
    public float volanteEsc;

    public GUISkin gsVolante;


    //PARA EL INVENTARIO
    public Vector2[] fondoPos;
    public Vector2 fondoEsc = Vector2.zero;

    //public Vector2 SlotsEsc = Vector2.zero;
    //public Vector2 SlotPrimPos = Vector2.zero;
    //public Vector2 Separacion = Vector2.zero;

    //public int Fil = 0;
    //public int Col = 0;

    public Texture2D texturaVacia; //lo que aparece si no hay ninguna bolsa
    public Texture2D textFondo;

    public float parpadeo = 0.8f;
    public float tempParp;
    public bool primIma = true;

    public Texture2D[] textInvIzq;
    public Texture2D[] textInvDer;

    public GUISkin gsInv;

    //BONO DE DESCARGA
    public Vector2 bonusPos = Vector2.zero;
    public Vector2 bonusEsc = Vector2.zero;

    public Color32 colorFondoBolsa;
    public Vector2 colorFondoPos = Vector2.zero;
    public Vector2 colorFondoEsc = Vector2.zero;

    public Vector2 colorFondoFondoPos = Vector2.zero;
    public Vector2 colorFondoFondoEsc = Vector2.zero;

    public GUISkin gsFondoBonusColor;
    public GUISkin gsFondoFondoBonusColor;
    public GUISkin gsBonus;


    //CALIBRACION MAS TUTO BASICO
    public Vector2 readyPos = Vector2.zero;
    public Vector2 readyEsc = Vector2.zero;
    public Texture2D[] imagenesDelTuto;
    public float intervalo = 0.8f; //tiempo de cada cuanto cambia de imagen
    public Texture2D imaEnPosicion;
    public Texture2D imaReady;
    public GUISkin gsTutoCalib;

    //NUMERO DEL JUGADOR
    public Texture2D textNum1;
    public Texture2D textNum2;
    public GameObject techo;

    private ControlDireccion _direccion;
    private int _enCurso = -1;
    private Player _pj;


    private Rect _r;
    private float _tempoIntTuto;

    //------------------------------------------------------------------//

    // Use this for initialization
    private void Start()
    {
        _tempoIntTuto = intervalo;
        _direccion = GetComponent<ControlDireccion>();
        _pj = GetComponent<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnGUI()
    {
        switch (_pj.estAct)
        {
            case Player.Estados.EnConduccion:
                //inventario
                SetInv3();
                //contador de dinero
                SetDinero();
                //el volante
                SetVolante();
                break;


            case Player.Estados.EnDescarga:
                //inventario
                SetInv3();
                //el bonus
                SetBonus();
                //contador de dinero
                SetDinero();
                break;


            case Player.Estados.EnCalibracion:
                //SetCalibr();
                break;


            case Player.Estados.EnTutorial:
                SetInv3();
                SetTuto();
                SetVolante();
                break;
        }

        GUI.skin = null;
    }

    //--------------------------------------------------------//

    public void CambiarACalibracion()
    {
        camCalibracion.enabled = true;
        camConduccion.enabled = false;
        camDescarga.enabled = false;
    }

    public void CambiarATutorial()
    {
        camCalibracion.enabled = false;
        camConduccion.enabled = true;
        camDescarga.enabled = false;
    }

    public void CambiarAConduccion()
    {
        camCalibracion.enabled = false;
        camConduccion.enabled = true;
        camDescarga.enabled = false;
    }

    public void CambiarADescarga()
    {
        camCalibracion.enabled = false;
        camConduccion.enabled = false;
        camDescarga.enabled = true;
    }

    //---------//

    public void SetLado(Lado lado)
    {
        ladoAct = lado;

        Rect r = new Rect();
        r.width = camConduccion.rect.width;
        r.height = camConduccion.rect.height;
        r.y = camConduccion.rect.y;

        switch (lado)
        {
            case Lado.Der:
                r.x = 0.5f;
                break;


            case Lado.Izq:
                r.x = 0;
                break;
        }

        camCalibracion.rect = r;
        camConduccion.rect = r;
        camDescarga.rect = r;

        if (ladoAct == Lado.Izq)
            techo.GetComponent<Renderer>().material.mainTexture = textNum1;
        else
            techo.GetComponent<Renderer>().material.mainTexture = textNum2;
    }

    private void SetBonus()
    {
        if (_pj.contrDesc.pEnMov != null)
        {
            //el fondo
            GUI.skin = gsFondoFondoBonusColor;

            _r.width = colorFondoFondoEsc.x * Screen.width / 100;
            _r.height = colorFondoFondoEsc.y * Screen.height / 100;
            _r.x = colorFondoFondoPos.x * Screen.width / 100;
            _r.y = colorFondoFondoPos.y * Screen.height / 100;
            if (ladoAct == Lado.Der)
                _r.x += Screen.width / 2;
            GUI.Box(_r, "");


            //el fondo
            GUI.skin = gsFondoBonusColor;

            _r.width = colorFondoEsc.x * Screen.width / 100;
            _r.height = colorFondoEsc.y * Screen.height / 100 * (_pj.contrDesc.bonus / (int)Pallet.Valores.Valor2);
            _r.x = colorFondoPos.x * Screen.width / 100;
            _r.y = colorFondoPos.y * Screen.height / 100 - _r.height;
            if (ladoAct == Lado.Der)
                _r.x += Screen.width / 2;
            GUI.Box(_r, "");


            //la bolsa
            GUI.skin = gsBonus;

            _r.width = bonusEsc.x * Screen.width / 100;
            _r.height = _r.width / 2;
            _r.x = bonusPos.x * Screen.width / 100;
            _r.y = bonusPos.y * Screen.height / 100;
            if (ladoAct == Lado.Der)
                _r.x += Screen.width / 2;
            GUI.Box(_r, "     $" + _pj.contrDesc.bonus.ToString("0"));
        }
    }

    private void SetDinero()
    {
        GUI.skin = gsDin;

        _r.width = dinEsc.x * Screen.width / 100;
        _r.height = dinEsc.y * Screen.height / 100;
        _r.x = dinPos[0].x * Screen.width / 100;
        _r.y = dinPos[0].y * Screen.height / 100;
        if (ladoAct == Lado.Der)
            _r.x = dinPos[1].x * Screen.width / 100;
        //R.x = (Screen.width) - (Screen.width/2) - R.x;
        GUI.Box(_r, "$" + PrepararNumeros(_pj.dinero));
    }

    private void SetCalibr()
    {
        GUI.skin = gsTutoCalib;

        _r.width = readyEsc.x * Screen.width / 100;
        _r.height = readyEsc.y * Screen.height / 100;
        _r.x = readyPos.x * Screen.width / 100;
        _r.y = readyPos.y * Screen.height / 100;
        if (ladoAct == Lado.Der)
            _r.x = Screen.width - _r.x - _r.width;

        switch (_pj.contrCalib.estAct)
        {
            case ContrCalibracion.Estados.Calibrando:

                //pongase en posicion para iniciar
                gsTutoCalib.box.normal.background = imaEnPosicion;
                GUI.Box(_r, "");

                break;

            case ContrCalibracion.Estados.Tutorial:
                //tome la bolsa y depositela en el estante

                _tempoIntTuto += T.GetDT();
                if (_tempoIntTuto >= intervalo)
                {
                    _tempoIntTuto = 0;
                    if (_enCurso + 1 < imagenesDelTuto.Length)
                        _enCurso++;
                    else
                        _enCurso = 0;
                }

                gsTutoCalib.box.normal.background = imagenesDelTuto[_enCurso];

                GUI.Box(_r, "");

                break;

            case ContrCalibracion.Estados.Finalizado:
                //esperando al otro jugador		
                gsTutoCalib.box.normal.background = imaReady;
                GUI.Box(_r, "");

                break;
        }
    }

    private void SetTuto()
    {
        if (_pj.contrTuto.finalizado)
        {
            GUI.skin = gsTutoCalib;

            _r.width = readyEsc.x * Screen.width / 100;
            _r.height = readyEsc.y * Screen.height / 100;
            _r.x = readyPos.x * Screen.width / 100;
            _r.y = readyPos.y * Screen.height / 100;
            if (ladoAct == Lado.Der)
                _r.x = Screen.width - _r.x - _r.width;

            GUI.Box(_r, "ESPERANDO AL OTRO JUGADOR");
        }
    }

    /*
    void SetInv()
    {
        GUI.skin = GS_Inv;

        //fondo
        GS_Inv.box.normal.background = TextFondo;
        R.width = FondoEsc.x * Screen.width /100;
        R.height = FondoEsc.y * Screen.height /100;
        R.x = FondoPos.x * Screen.width /100;
        R.y = FondoPos.y * Screen.height /100;
        if(LadoAct == Visualizacion.Lado.Der)
            R.x = (Screen.width) - R.x - R.width;
        GUI.Box(R,"");

        //bolsas
        R.width = SlotsEsc.x * Screen.width /100;
        R.height = SlotsEsc.y * Screen.height /100;
        int contador = 0;
        for(int j = 0; j < Fil; j++)
        {
            for(int i = 0; i < Col; i++)
            {
                R.x = SlotPrimPos.x * Screen.width / 100 + Separacion.x * i * Screen.width / 100;
                R.y = SlotPrimPos.y * Screen.height / 100 + Separacion.y * j * Screen.height / 100;
                if(LadoAct == Visualizacion.Lado.Der)
                    R.x = (Screen.width) - R.x - R.width;

                if(contador < Pj.Bolasas.Length )//&& Pj.Bolasas[contador] != null)
                {
                    if(Pj.Bolasas[contador]!=null)
                        GS_Inv.box.normal.background = Pj.Bolasas[contador].ImagenInventario;
                    else
                        GS_Inv.box.normal.background = TexturaVacia;
                }
                else
                {
                    GS_Inv.box.normal.background = TexturaVacia;
                }
                GUI.Box(R,"");

                contador++;
            }
        }
    }
    */

    private void SetVolante()
    {
        GUI.skin = gsVolante;

        _r.width = volanteEsc * Screen.width / 100;
        _r.height = volanteEsc * Screen.width / 100;
        _r.x = volantePos[0].x * Screen.width / 100;
        _r.y = volantePos[0].y * Screen.height / 100;

        if (ladoAct == Lado.Der)
            _r.x = volantePos[1].x * Screen.width / 100;
        //R.x = (Screen.width) - ((Screen.width/2) - R.x);

        Vector2 centro;
        centro.x = _r.x + _r.width / 2;
        centro.y = _r.y + _r.height / 2;
        float angulo = 100 * _direccion.GetGiro();

        GUIUtility.RotateAroundPivot(angulo, centro);

        GUI.Box(_r, "");

        GUIUtility.RotateAroundPivot(angulo * -1, centro);
    }

    private void SetInv2()
    {
        GUI.skin = gsInv;

        _r.width = fondoEsc.x * Screen.width / 100;
        _r.height = fondoEsc.y * Screen.width / 100;
        _r.x = fondoPos[0].x * Screen.width / 100;
        _r.y = fondoPos[0].y * Screen.height / 100;

        int contador = 0;
        for (int i = 0; i < 3; i++)
            if (_pj.bolasas[i] != null)
                contador++;

        if (ladoAct == Lado.Der)
        {
            //R.x = (Screen.width) - R.x - R.width;
            _r.x = fondoPos[1].x * Screen.width / 100;
            gsInv.box.normal.background = textInvDer[contador];
        }
        else
        {
            gsInv.box.normal.background = textInvIzq[contador];
        }

        GUI.Box(_r, "");
    }

    private void SetInv3()
    {
        GUI.skin = gsInv;

        _r.width = fondoEsc.x * Screen.width / 100;
        _r.height = fondoEsc.y * Screen.width / 100;
        _r.x = fondoPos[0].x * Screen.width / 100;
        _r.y = fondoPos[0].y * Screen.height / 100;

        int contador = 0;
        for (int i = 0; i < 3; i++)
            if (_pj.bolasas[i] != null)
                contador++;

        if (ladoAct == Lado.Der)
        {
            //R.x = (Screen.width) - (Screen.width/2) - R.x;
            _r.x = fondoPos[1].x * Screen.width / 100;

            if (contador < 3)
            {
                gsInv.box.normal.background = textInvDer[contador];
            }
            else
            {
                tempParp += T.GetDT();

                if (tempParp >= parpadeo)
                {
                    tempParp = 0;
                    if (primIma)
                        primIma = false;
                    else
                        primIma = true;
                }

                if (primIma)
                    gsInv.box.normal.background = textInvDer[3];
                else
                    gsInv.box.normal.background = textInvDer[4];
            }
        }
        else
        {
            if (contador < 3)
            {
                gsInv.box.normal.background = textInvIzq[contador];
            }
            else
            {
                tempParp += T.GetDT();

                if (tempParp >= parpadeo)
                {
                    tempParp = 0;
                    if (primIma)
                        primIma = false;
                    else
                        primIma = true;
                }

                if (primIma)
                    gsInv.box.normal.background = textInvIzq[3];
                else
                    gsInv.box.normal.background = textInvIzq[4];
            }
        }

        GUI.Box(_r, "");
    }

    public string PrepararNumeros(int dinero)
    {
        string strDinero = dinero.ToString();
        string res = "";

        if (dinero < 1) //sin ditero
            res = "";
        else if (strDinero.Length == 6) //cientos de miles
            for (int i = 0; i < strDinero.Length; i++)
            {
                res += strDinero[i];

                if (i == 2) res += ".";
            }
        else if (strDinero.Length == 7) //millones
            for (int i = 0; i < strDinero.Length; i++)
            {
                res += strDinero[i];

                if (i == 0 || i == 3) res += ".";
            }

        return res;
    }
}