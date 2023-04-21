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
    }
    
    private void NumberOnClick(object sender, EventArgs args)
    {
        if (sender is not Button senderButton)
        {
            return;
        }
        _calculatorCore.TakeNumberToken(senderButton.Text);
    }

    private readonly CalculatorCore _calculatorCore = new ();
}