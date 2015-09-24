using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMapEditor.Objects.Controls
{
    public class ConsoleStreamWriter : TextWriter
    {
        private ConsolePanel output;

        public ConsoleStreamWriter(ConsolePanel output)
        {
            this.output = output;
        }

        public override void WriteLine(string value)
        {
            base.WriteLine(value);

            // Oblige le WriteLine sur le thread UI
            this.output.Invoke((Action)delegate  { this.output.WriteLine(value); });
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}
