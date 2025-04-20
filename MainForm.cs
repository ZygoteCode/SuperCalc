using System;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using MetroSuite;

public partial class MainForm : MetroForm
{
    [DllImport("psapi.dll")]
    private static extern int EmptyWorkingSet(IntPtr hwProc);

    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetProcessWorkingSetSize(IntPtr process, UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);

    public MainForm()
    {
        InitializeComponent();
        CheckForIllegalCrossThreadCalls = false;
        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;

        Thread clearRamThread = new Thread(ClearRAM);
        clearRamThread.Priority = ThreadPriority.Highest;
        clearRamThread.Start();

        Thread updateTextsThread = new Thread(UpdateTexts);
        updateTextsThread.Priority = ThreadPriority.Highest;
        updateTextsThread.Start();

        guna2ComboBox1.SelectedIndex = 0;
    }

    public void ClearRAM()
    {
        while (true)
        {
            Thread.Sleep(100);
            EmptyWorkingSet(Process.GetCurrentProcess().Handle);
            GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);
        }
    }

    public void UpdateTexts()
    {
        while (true)
        {
            Thread.Sleep(1);

            metroLabel1.Text = $"First operand ({guna2TextBox1.Text.Length}):";
            metroLabel2.Text = $"Second operand ({guna2TextBox2.Text.Length}):";
            metroLabel3.Text = $"Operation result ({guna2TextBox3.Text.Length}):";
        }
    }

    private void MainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
    {
        Process.GetCurrentProcess().Kill();
    }

    private void guna2TextBox1_DoubleClick(object sender, EventArgs e)
    {
        try
        {
            Clipboard.SetText(guna2TextBox1.Text);
        }
        catch
        {

        }
    }

    private void guna2TextBox2_DoubleClick(object sender, EventArgs e)
    {
        try
        {
            Clipboard.SetText(guna2TextBox2.Text);
        }
        catch
        {

        }
    }

    private void guna2Button1_Click(object sender, EventArgs e)
    {
        try
        {
            Clipboard.SetText(guna2TextBox1.Text);
        }
        catch
        {

        }
    }

    private void guna2Button2_Click(object sender, EventArgs e)
    {
        try
        {
            Clipboard.SetText(guna2TextBox2.Text);
        }
        catch
        {

        }
    }

    private void guna2Button3_Click(object sender, EventArgs e)
    {
        try
        {
            guna2TextBox1.Text += Clipboard.GetText();
        }
        catch
        {

        }
    }

    private void guna2Button4_Click(object sender, EventArgs e)
    {
        try
        {
            guna2TextBox2.Text += Clipboard.GetText();
        }
        catch
        {

        }
    }

    private void guna2Button6_Click(object sender, EventArgs e)
    {
        try
        {
            Clipboard.SetText(guna2TextBox3.Text);
        }
        catch
        {

        }
    }

    private void guna2Button5_Click(object sender, EventArgs e)
    {
        string firstOperand = guna2TextBox1.Text;
        firstOperand = SuperCalculator.GetCorrectNumber(firstOperand);

        if (!SuperCalculator.IsNumberValid(firstOperand))
        {
            MessageBox.Show("The first operand is not valid.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        string secondOperand = guna2TextBox2.Text;
        secondOperand = SuperCalculator.GetCorrectNumber(secondOperand);

        if (!SuperCalculator.IsNumberValid(secondOperand))
        {
            MessageBox.Show("The second operand is not valid.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (guna2ComboBox1.SelectedIndex == 0)
        {
            guna2TextBox3.Text = SuperCalculator.Addition(firstOperand, secondOperand);
        }
        else if (guna2ComboBox1.SelectedIndex == 1)
        {
            guna2TextBox3.Text = SuperCalculator.Subtraction(firstOperand, secondOperand);
        }
        else if (guna2ComboBox1.SelectedIndex == 2)
        {
            guna2TextBox3.Text = SuperCalculator.Multiplication(firstOperand, secondOperand);
        }
        else if (guna2ComboBox1.SelectedIndex == 3)
        {
            guna2TextBox3.Text = SuperCalculator.Division(firstOperand, secondOperand);
        }
        else if (guna2ComboBox1.SelectedIndex == 4)
        {
            guna2TextBox3.Text = SuperCalculator.Max(firstOperand, secondOperand);
        }
        else if (guna2ComboBox1.SelectedIndex == 5)
        {
            guna2TextBox3.Text = SuperCalculator.Min(firstOperand, secondOperand);
        }
        else if (guna2ComboBox1.SelectedIndex == 6)
        {
            guna2TextBox3.Text = SuperCalculator.Modulo(firstOperand, secondOperand);
        }
        else if (guna2ComboBox1.SelectedIndex == 7)
        {
            guna2TextBox3.Text = SuperCalculator.Power(firstOperand, secondOperand);
        }
    }
}