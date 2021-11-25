using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;

public class Level1Plot4Contronller : BaseDialogContronller
{
    public override void CreatePlot()
    {
        plotType = new PlotType("Plot4Dialog","/Level1Plot4.txt","Canvas","Text","Image",new DialogUI());
    }
}
