using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;

public class Level1Plot3Contronller : BaseDialogContronller
{
    public override void CreatePlot()
    {
        plotType = new PlotType("Plot3Dialog","/Level1Plot3.txt","Canvas","Text","Image",new DialogUI());
    }
}
