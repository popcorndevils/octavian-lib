using Godot;

using PichaLib;

public class CanvasView : TabContainer
{
    public GenCanvas Active {
        get {
            if(this.GetChildren().Count > 0)
            {
                return this.GetChild<CanvasContainer>(this.CurrentTab).Canvas;
            }
            return null;
        } 
    }
    public override void _Ready()
    {
        this.AddToGroup("gp_canvas_handler");
    }

    public void AddCanvas(GenCanvas c)
    {
        var _i = this.GetChildren().Count;
        var _view = new CanvasContainer();
        this.AddChild(_view);
        c.Position = new Vector2(_view.RectSize.x / 2, _view.RectSize.y / 2);
        _view.Canvas = c;
        if(c.Data == null) { c.Data = new Canvas(); }
        this.CurrentTab = _i;
        this.SetTabTitle(this.CurrentTab, c.CanvasName);
    }

    public void AddLayer(GenLayer l)
    {
        if(this.Active != null)
        {
            this.Active.AddLayer(l);
            this.GetTree().CallGroup("gp_layer_gui", "LoadLayer", l);
        }
    }

    public void NameCurrentTab(string s)
    {
        this.SetTabTitle(this.CurrentTab, s);
    }
}
