using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace POSDesktopUI.Input
{
    public class KeySequence
    {
        public Key[] Keys { get; }
        public ModifierKeys Modifiers { get; }
        public KeySequence(ModifierKeys modifiers, params Key[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));

            if (keys.Length < 1)
                throw new ArgumentException(@"At least 1 key should be provided", nameof(keys));

            Keys = new Key[keys.Length];
            keys.CopyTo(Keys, 0);
            Modifiers = modifiers;
        }
        public override string ToString()
        {
            var builder = new StringBuilder();

            if (Modifiers != ModifierKeys.None)
            {
                if ((Modifiers & ModifierKeys.Control) != ModifierKeys.None)
                    builder.Append("Ctrl+");
                if ((Modifiers & ModifierKeys.Alt) != ModifierKeys.None)
                    builder.Append("Alt+");
                if ((Modifiers & ModifierKeys.Shift) != ModifierKeys.None)
                    builder.Append("Shift+");
                if ((Modifiers & ModifierKeys.Windows) != ModifierKeys.None)
                    builder.Append("Windows+");
            }

            builder.Append(Keys[0]);

            for (var i = 1; i < Keys.Length; i++)
            {
                builder.Append("+" + Keys[i]);
            }

            return builder.ToString();
        }
    }
}
