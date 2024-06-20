namespace RunningButton;

/// <summary>
/// Form class for the running button game
/// </summary>
public partial class RunningButtonForm : Form
{
    /// <summary>
    /// Creates form and initializes the button
    /// </summary>
    public RunningButtonForm()
    {
        InitializeComponent();
    }

    private enum Position
    {
        TopLeft,
        TopRight,
        BottomRight,
        BottomLeft
    }
    
    private void OnEnter(object? sender, EventArgs eventArgs)
    {
        if (sender is not Button senderButton)
        {
            return;
        }

        switch (_buttonPosition)
        {
            case Position.TopLeft:
                _buttonPosition += 1;
                senderButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                senderButton.Location = new Point(Size.Width - senderButton.Size.Width, 0);
                break;
            case Position.TopRight:
                _buttonPosition += 1;
                senderButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                senderButton.Location = new Point(Size.Width - senderButton.Size.Width, Size.Height - senderButton.Size.Height);
                break;
            case Position.BottomRight:
                _buttonPosition += 1;
                senderButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                senderButton.Location = new Point(0, Size.Height - senderButton.Size.Height);
                break;
            case Position.BottomLeft:
                _buttonPosition = 0;
                senderButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                senderButton.Location = new Point(0, 0);
                break;
        }
    }
    
    private void OnClick(object? sender, EventArgs eventArgs)
    {
        if (sender is not Button)
        {
            return;
        }
        
        Close();
    }
}