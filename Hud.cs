public partial class Hud : Control
{
    private VBoxContainer primaryButtonContainer;
    private VBoxContainer secondaryButtonContainer;

    private Label energyLabel;

    private VBoxContainer hpContainer;

    public override void _Ready()
    {
        this.primaryButtonContainer = GetNode<VBoxContainer>("Margin/ButtonContainerA");
        this.secondaryButtonContainer = GetNode<VBoxContainer>("Margin/ButtonContainerB");

        setPrimaryVisible();

        this.energyLabel = GetNode<Label>("EnergyDisplay/LabelEnergy");
        this.hpContainer = GetNode<VBoxContainer>("HpContainer");
    }

    public void setButtons(List<(string, string, List<(string, Action)>)> nestedButtons)
    {
        clearChildren(this.primaryButtonContainer);
        clearChildren(this.secondaryButtonContainer);

        setPrimaryVisible();

        foreach (var (buttonText, label, secondaryButtons) in nestedButtons)
        {
            var button = new Button();
            button.Text = buttonText;
            button.ButtonDown += () =>
            {
                setSecondaryButtons(secondaryButtons, label);
            };

            this.primaryButtonContainer.AddChild(button);
        }
    }

    public void setSecondaryButtons(List<(string, Action)> buttons, string labelText)
    {
        clearChildren(this.secondaryButtonContainer);
        setSecondaryVisible();

        var backButton = new Button();
        backButton.Text = "< Back";
        backButton.ButtonDown += () =>
        {
            setPrimaryVisible();
            clearChildren(this.secondaryButtonContainer);
        };
        this.secondaryButtonContainer.AddChild(backButton);

        var label = new Label();
        label.Text = labelText;
        this.secondaryButtonContainer.AddChild(label);

        foreach (var (text, action) in buttons)
        {
            var button = new Button();
            button.Text = text;
            button.ButtonDown += action;

            this.secondaryButtonContainer.AddChild(button);
        }
    }

    private void clearChildren(VBoxContainer node)
    {
        foreach (var child in node.GetChildren())
        {
            child.QueueFree();
        }
    }

    private void setPrimaryVisible()
    {
        this.primaryButtonContainer.Visible = true;
        this.secondaryButtonContainer.Visible = false;
    }

    private void setSecondaryVisible()
    {
        this.primaryButtonContainer.Visible = false;
        this.secondaryButtonContainer.Visible = true;
    }

    public void clear()
    {
        clearChildren(this.primaryButtonContainer);
        clearChildren(this.secondaryButtonContainer);

        this.primaryButtonContainer.Visible = false;
        this.secondaryButtonContainer.Visible = false;
    }

    public void setEnergyLabelText(string text) => this.energyLabel.Text = text;

    public Label createHpDisplay(string characterName)
    {
        var hbox = new HBoxContainer();
        this.hpContainer.AddChild(hbox);

        var label = new Label();
        label.Text = characterName;
        hbox.AddChild(label);

        var hpLabel = new Label();
        hbox.AddChild(hpLabel);
        return hpLabel;
    }
}