using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6Plot2 : BaseDialogContronller
{
    public override void CreatePlot()
    {
        plotType = new PlotType("Level6Plot2Dialog","/Level6Plot2.txt","Canvas","Text","Image",new DialogUI());
    }

}
