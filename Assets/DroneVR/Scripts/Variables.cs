using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables
{
    /* La difficulté choisie
     * 1 = facile
     * 2 = moyen
     * 3 = difficile
     * */
    private static int difficulte = 1;

    /* Si la VR doit être activée ou non
     * true = activée
     * false = non activée
    */
    private static bool VR = true;

    public void Set_difficulte (int d)
    {
        difficulte = d;
    }

    public int Get_difficulte()
    {
        return difficulte;
    }

    public void Set_VR(bool b)
    {
        VR = b;
    }

    public bool Get_VR()
    {
        return VR;
    }
}
