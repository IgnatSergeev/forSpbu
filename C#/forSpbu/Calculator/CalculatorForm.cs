namespace Calculator;

public partial class CalculatorForm : Form
{
    public CalculatorForm()
    {
        InitializeComponent();
    }

    private void OperationOnClick(object sender, EventArgs args)
    {
        if (sender is not Button senderButton)
        {
            return;
        }
        _calculatorCore.TakeOperationToken(senderButton.Text);
        expression.Text = _calculatorCore.Expression;
        numberLabel.Text = _calculatorCore.Number;
    }
    
    private void NumberOnClick(object sender, EventArgs args)
    {
        if (sender is not Button senderButton)
        {
            return;
        }
        _calculatorCore.TakeNumberToken(senderButton.Text);
        expression.Text = _calculatorCore.Expression;
        numberLabel.Text = _calculatorCore.Number;
    }

    private readonly CalculatorCore _calculatorCore = new ();
}