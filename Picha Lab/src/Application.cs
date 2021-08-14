using Newtonsoft.Json;

using Godot;

using PichaLib;

public class Application : Node
{
    private MenuBar _Menu;
    private Control _GUI;
    private AcceptDialog _PatternDesigner;
    private FileDialog _SaveAs;
    private FileDialog _OpenCanvas;
    private HelpDialog _HelpDialog;
    private CanvasView _Canvases;

    public override void _Ready()
    {
        OS.MinWindowSize = new Vector2(820, 550);

        this._Menu = this.GetNode<MenuBar>("PichaGUI/WSVert/MenuBar");
        this._GUI = this.GetNode<Control>("PichaGUI");
        this._PatternDesigner = this.GetNode<AcceptDialog>("PichaGUI/PatternDesigner");
        this._SaveAs = this.GetNode<FileDialog>("PichaGUI/SaveDialog");
        this._OpenCanvas = this.GetNode<FileDialog>("PichaGUI/OpenDialog");
        this._HelpDialog = this.GetNode<HelpDialog>("PichaGUI/HelpDialog");
        this._Canvases = this.GetNode<WorkArea>("PichaGUI/WSVert/WorkArea").CanvasView;

        this._RegisterSignals();
    }
    
    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("undo"))
        {
            this._Canvases.UndoChange();
        }
    }

    private void _RegisterSignals()
    {
        this._Menu.ItemSelected += this.HandleMenu;
        this._SaveAs.Connect("file_selected", this, "OnFileSaveAs");
        this._OpenCanvas.Connect("file_selected", this, "OnOpenCanvas");
    }

    public void HandleMenu(MenuBarItem menu)
    {
        switch(menu.Action)
        {
            case "new_canvas":
                this.GetTree().CallGroup("gp_canvas_handler", "AddCanvas", new GenCanvas());
                break;
            case "open_canvas":
                this._OpenCanvas.PopupCentered();
                break;
            case "save_canvas":
                if(this._Canvases.Active != null)
                {
                    if(this._Canvases.FileExists) 
                    { 
                        this._Canvases.Save(); 
                    }
                    else 
                    { 
                        this._SaveAs.PopupCentered(); 
                    }
                }
                break;
            case "save_canvas_as":
                if(this._Canvases.Active != null)
                { 
                    this._SaveAs.PopupCentered();
                }
                break;
            case "open_docs":
                this._HelpDialog.PopupCentered();
                break;
            default:
                GD.PrintErr($"Unable to Parse MenuItem action \"{menu.Action}\".");
                break;
        }
    }

    public void OnFileSaveAs(string f)
    {
        this._Canvases.SaveAsFile(f);
    }

    public void OnOpenCanvas(string path)
    {
        var _can = new GenCanvas() {
            FileExists = true,
            PathName = path,
        };

        this._Canvases.AddCanvas(_can);

        var _dat = JsonConvert.DeserializeObject<Canvas>(System.IO.File.ReadAllText(path));

        _can.LoadData(_dat);
    }
}