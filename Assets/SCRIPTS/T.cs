using UnityEngine;

public static class T
{
    private static float _fdt;
    private static float _dt;

    //----------
    //fixed delta time
    public static float FactorFdt = 1;
    private static bool _fifadoFdt;

    //----------
    //delta time
    public static float FactorDT = 1;
    private static bool _fifadoDT;


    public static float GetDT()
    {
        if (!_fifadoDT)
            _dt = Time.deltaTime * FactorDT;

        return _dt;
    }

    public static float GetFdt()
    {
        if (!_fifadoFdt)
            _fdt = Time.fixedDeltaTime * FactorFdt;

        return _fdt;
    }

    public static void FijarFdt(float valor)
    {
        _fifadoFdt = true;
        _fdt = valor;
    }

    public static void RestaurarFdt()
    {
        _fifadoFdt = false;
        _fdt = Time.fixedDeltaTime;
        FactorFdt = 1;
    }

    public static void FijarDT(float valor)
    {
        _fifadoDT = true;
        _dt = valor;
    }

    public static void RestaurarDT()
    {
        _fifadoDT = false;
        _dt = Time.deltaTime;
        FactorDT = 1;
    }
}