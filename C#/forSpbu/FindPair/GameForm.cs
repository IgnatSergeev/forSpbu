namespace FindPare;

public partial class GameForm : Form
{
    public GameForm(int fieldSize)
    {
        _gameCore = new GameCore(fieldSize);
        InitializeComponent(fieldSize);
    }

    private void CellOnClick(object sender, EventArgs eventArgs)
    {
        if (sender is not Button senderButton)
        {
            return;
        }
        if (senderButton.Tag is not string indexString)
        {
            return;
        } 
        int index = int.Parse(indexString);
        var coordinate = (index % fieldSize, index - (index % fieldSize) * fieldSize);
        if (_gameCore.OpenCell(index))
        {
            senderButton.Text = _gameCore.GetValue(coordinate.Item1, coordinate.Item2);
        }
        
    }

    private Button lastOpenedButton;
    private readonly GameCore _gameCore;
}