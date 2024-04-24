using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ConsoleControl
{
    public delegate void ConsoleEventHanlder(object sender, ConsoleEventArgs args);
    [ToolboxBitmap(typeof(resfinder), "ConsoleControl.ConsoleControl.bmp")]
    public partial class ConsoleControl : UserControl
    {
        public ConsoleControl()
        {
            InitializeComponent();
            ShowDiagnostics = false;
            IsInputEnabled = true;
            SendKeyboardCommandsToProcess = false;
            InitialiseKeyMappings();
            processInterace.OnProcessOutput += new ProcessInterface.ProcessEventHanlder(processInterace_OnProcessOutput);
            processInterace.OnProcessError += new ProcessInterface.ProcessEventHanlder(processInterace_OnProcessError);
            processInterace.OnProcessInput += new ProcessInterface.ProcessEventHanlder(processInterace_OnProcessInput);
            processInterace.OnProcessExit += new ProcessInterface.ProcessEventHanlder(processInterace_OnProcessExit);
            richTextBoxConsole.KeyDown += new KeyEventHandler(richTextBoxConsole_KeyDown);
        }
        void processInterace_OnProcessError(object sender, ProcessInterface.ProcessEventArgs args)
        {
            WriteOutput(args.Content, Color.Red);
            FireConsoleOutputEvent(args.Content);
        }
        void processInterace_OnProcessOutput(object sender, ProcessInterface.ProcessEventArgs args)
        {
            WriteOutput(args.Content, Color.White);
            FireConsoleOutputEvent(args.Content);
        }
        void processInterace_OnProcessInput(object sender, ProcessInterface.ProcessEventArgs args)
        {
            throw new NotImplementedException();
        }
        void processInterace_OnProcessExit(object sender, ProcessInterface.ProcessEventArgs args)
        {
            if (ShowDiagnostics)
            {
                WriteOutput(System.Environment.NewLine + processInterace.ProcessFileName + " exited.", Color.FromArgb(255, 0, 255, 0));
            }
            Invoke((Action)(() =>
            {
                richTextBoxConsole.ReadOnly = true;
            }));
        }
        private void InitialiseKeyMappings()
        {
            keyMappings.Add(new KeyMapping(false, false, false, Keys.Tab, "{TAB}", "\t"));
            keyMappings.Add(new KeyMapping(true, false, false, Keys.C, "^(c)", "\x03\r\n"));
        }
        void richTextBoxConsole_KeyDown(object sender, KeyEventArgs e)
        {
            if (SendKeyboardCommandsToProcess && IsProcessRunning)
            {
                var mappings = from k in keyMappings
                               where 
                               (k.KeyCode == e.KeyCode &&
                               k.IsAltPressed == e.Alt &&
                               k.IsControlPressed == e.Control &&
                               k.IsShiftPressed == e.Shift)
                               select k;
                foreach (var mapping in mappings)
                {

                }
                if (mappings.Count() > 0)
                {
                    e.SuppressKeyPress = true;
                    return;
                }
            }
            if ((richTextBoxConsole.SelectionStart <= inputStart) && e.KeyCode == Keys.Back) e.SuppressKeyPress = true;
            if (richTextBoxConsole.SelectionStart < inputStart)
            {
                if (!(e.KeyCode == Keys.Left ||
                    e.KeyCode == Keys.Right ||
                    e.KeyCode == Keys.Up ||
                    e.KeyCode == Keys.Down ||
                    (e.KeyCode == Keys.C && e.Control)))
                {
                    e.SuppressKeyPress = true;
                }
            }
            if (e.KeyCode == Keys.Return)
            {
                string input = richTextBoxConsole.Text.Substring(inputStart, (richTextBoxConsole.SelectionStart) - inputStart);
                WriteInput(input, Color.White, false);
            }
        }

        public void WriteOutput(string output, Color color)
        {
            if (string.IsNullOrEmpty(lastInput) == false && 
                (output == lastInput || output.Replace("\r\n", "") == lastInput))
                return;

            Invoke((Action)(() =>
            {
                richTextBoxConsole.SelectionColor = color;
                richTextBoxConsole.SelectedText += output;
                inputStart = richTextBoxConsole.SelectionStart;
            }));
        }

        public void ClearOutput()
        {
            richTextBoxConsole.Clear();
            inputStart = 0;
        }
        public void WriteInput(string input, Color color, bool echo)
        {
            Invoke((Action)(() =>
            {
                if (echo)
                {
                    richTextBoxConsole.SelectionColor = color;
                    richTextBoxConsole.SelectedText += input;
                    inputStart = richTextBoxConsole.SelectionStart;
                }

                lastInput = input;
                processInterace.WriteInput(input);
                FireConsoleInputEvent(input);
            }));
        }
        public void StartProcess(string fileName, string arguments)
        {
            if (ShowDiagnostics)
            {
                WriteOutput("Preparing to run " + fileName, Color.FromArgb(255, 0, 255, 0));
                if (!string.IsNullOrEmpty(arguments))
                    WriteOutput(" with arguments " + arguments + "." + Environment.NewLine, Color.FromArgb(255, 0, 255, 0));
                else
                    WriteOutput("." + Environment.NewLine, Color.FromArgb(255, 0, 255, 0));
            }
            processInterace.StartProcess(fileName, arguments);
            if (IsInputEnabled)
                richTextBoxConsole.ReadOnly = false;
        }
        public void StopProcess()
        {
            processInterace.StopProcess();
        }
        private void FireConsoleOutputEvent(string content)
        {
            var theEvent = OnConsoleOutput;
            if (theEvent != null)
                theEvent(this, new ConsoleEventArgs(content));
        }
        private void FireConsoleInputEvent(string content)
        {
            var theEvent = OnConsoleInput;
            if (theEvent != null)
                theEvent(this, new ConsoleEventArgs(content));
        }
        private ProcessInterface.ProcessInterface processInterace = new ProcessInterface.ProcessInterface();
        int inputStart = -1;
        private bool isInputEnabled = true;
        private string lastInput;
        private List<KeyMapping> keyMappings = new List<KeyMapping>();
        public event ConsoleEventHanlder OnConsoleOutput;
        public event ConsoleEventHanlder OnConsoleInput;
        [Category("Console Control"), Description("Show diagnostic information, such as exceptions.")]
        public bool ShowDiagnostics
        {
            get;
            set;
        }
        [Category("Console Control"), Description("If true, the user can key in input.")]
        public bool IsInputEnabled
        {
            get { return isInputEnabled; }
            set
            {
                isInputEnabled = value;
                if (IsProcessRunning)
                    richTextBoxConsole.ReadOnly = !value;
            }
        }
        [Category("Console Control"), Description("If true, special keyboard commands like Ctrl-C and tab are sent to the process.")]
        public bool SendKeyboardCommandsToProcess
        {
            get;
            set;
        }
        [Browsable(false)]
        public bool IsProcessRunning
        {
            get { return processInterace.IsProcessRunning; }
        }
        [Browsable(false)]
        public RichTextBox InternalRichTextBox
        {
            get { return richTextBoxConsole; }
        }
        [Browsable(false)]
        public ProcessInterface.ProcessInterface ProcessInterface
        {
            get { return processInterace; }
        }
        [Browsable(false)]
        public List<KeyMapping> KeyMappings
        {
            get { return keyMappings; }
        }
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                richTextBoxConsole.Font = value;
            }
        }
    }
}