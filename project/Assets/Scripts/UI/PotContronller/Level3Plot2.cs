using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Plot2 : BaseDialogContronller
{
    public override void CreatePlot()
    {
        plotType = new PlotType("Level3Plot1Dialog","/Level3Plot2.txt","Canvas","Text","Image",new DialogUI());
    }
}
