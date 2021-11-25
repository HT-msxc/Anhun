using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Plot1 : BaseDialogContronller
{
    public override void CreatePlot()
    {
        plotType = new PlotType("Level4Plot1Dialog","/Level4Plot1.txt","Canvas","Text","Image",new DialogUI());
    }
}
