using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6Plot4 : BaseDialogContronller
{
    public override void CreatePlot()
    {
        plotType = new PlotType("Level6Plot4Dialog","/Level6Plot4.txt","Canvas","Text","Image",new DialogUI());
    }
}
