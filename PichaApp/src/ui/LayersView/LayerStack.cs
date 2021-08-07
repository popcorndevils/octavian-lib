using Godot;

public class LayerStack : TabContainer
{
    private VBoxContainer _LayersViewList = new VBoxContainer() {
        Name = "Layer Stack",
        RectMinSize = new Vector2(260, 200)
    };

    private ScrollContainer _Contents = new ScrollContainer() {
        SizeFlagsVertical = (int)SizeFlags.ExpandFill,
        SizeFlagsHorizontal = (int)SizeFlags.ExpandFill,
    };

    private HBoxContainer _FooterContainer = new HBoxContainer() {
        SizeFlagsHorizontal = (int)SizeFlags.ExpandFill,
    };

    private VBoxContainer _Buttons = new VBoxContainer() {
        SizeFlagsHorizontal = (int)SizeFlags.ExpandFill,
        SizeFlagsVertical = (int)SizeFlags.ExpandFill,
    };

    private Button _NewLayer = new Button() {
        SizeFlagsHorizontal = (int)SizeFlags.ShrinkEnd,
        Icon = GD.Load<Texture>("res://res/icons/queue_white.svg"),
        FocusMode = FocusModeEnum.None,
        HintTooltip = PDefaults.ToolHints.Layer.NewLayer,
    };

    public GenCanvas Canvas;

    public override void _Ready()
    {
        this.TabAlign = TabAlignEnum.Left;
        this.DragToRearrangeEnabled = false;

        this.AddToGroup("gp_canvas_gui");
        this.AddToGroup("layers_list");

        this.AddChild(this._LayersViewList);

        this._FooterContainer.AddChildren(this._NewLayer);
        this._Contents.AddChild(_Buttons);

        this._LayersViewList.AddChildren(
            this._Contents,
            this._FooterContainer
        );

        this._NewLayer.Connect("pressed", this, "OnNewLayerPressed");
    }

    public void LoadCanvas(GenCanvas c)
    {
        this.Canvas = c;

        foreach(Node n in this._Buttons.GetChildren())
        {
            this._Buttons.RemoveChild(n);
        }
        foreach(GenLayer _l in c.Layers.Values)
        {
            this._Buttons.AddChild(new LayerButtonControl() { Layer = _l });
        }
    }

    public void AddNewLayer(GenLayer l)
    {
        this._Buttons.AddChild(new LayerButtonControl() { Layer = l });
    }

    public void OnNewLayerPressed()
    {
        this.GetTree().CallGroup("pattern_designer", "NewLayer");
    }
}