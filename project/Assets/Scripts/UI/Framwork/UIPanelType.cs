using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelType
{
    private string _name;
    private string _path;
    public string Name
    {
        get =>_name;
        set{_name = value;}
    }
    public string Path{get => _path; set{_path = value;}}
    public UIPanelType(string path)
    {
        this._path = path;
        _name = path.Substring(path.LastIndexOf('/') + 1);
    }

}

