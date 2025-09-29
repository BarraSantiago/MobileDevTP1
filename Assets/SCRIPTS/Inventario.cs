using UnityEngine;

public class Inventario : MonoBehaviour
{
    public Vector2 fondoPos = Vector2.zero;
    public Vector2 fondoEsc = Vector2.zero;

    public Vector2 slotsEsc = Vector2.zero;
    public Vector2 slotPrimPos = Vector2.zero;
    public Vector2 separacion = Vector2.zero;

    public int fil;
    public int col;

    public Texture2D texturaVacia; //lo que aparece si no hay ninguna bolsa
    public Texture2D textFondo;
    public GUISkin gs;
    private Player _pj;

    private Rect _r;

    //------------------------------------------------------------------//

    // Use this for initialization
    private void Start()
    {
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
                GUI.skin = gs;

                //fondo
                gs.box.normal.background = textFondo;
                _r.width = fondoEsc.x * Screen.width / 100;
                _r.height = fondoEsc.y * Screen.height / 100;
                _r.x = fondoPos.x * Screen.width / 100;
                _r.y = fondoPos.y * Screen.height / 100;
                GUI.Box(_r, "");

                //bolsas
                _r.width = slotsEsc.x * Screen.width / 100;
                _r.height = slotsEsc.y * Screen.height / 100;
                int contador = 0;
                for (int j = 0; j < fil; j++)
                for (int i = 0; i < col; i++)
                {
                    _r.x = slotPrimPos.x * Screen.width / 100 + separacion.x * i * Screen.width / 100;
                    _r.y = slotPrimPos.y * Screen.height / 100 + separacion.y * j * Screen.height / 100;

                    if (contador < _pj.bolasas.Length) //&& Pj.Bolasas[contador] != null)
                    {
                        if (_pj.bolasas[contador] != null)
                            gs.box.normal.background = _pj.bolasas[contador].imagenInventario;
                        else
                            gs.box.normal.background = texturaVacia;
                    }
                    else
                    {
                        gs.box.normal.background = texturaVacia;
                    }

                    GUI.Box(_r, "");

                    contador++;
                }

                GUI.skin = null;
                break;
        }
    }

    //------------------------------------------------------------------//
}