using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using mRemoteNC.Tools;

namespace mRemoteNC.Config
{
    [SettingsSerializeAs(SettingsSerializeAs.String)]
    public class ToolStripConfig
    {
        public String Name { get; set; }
        public Boolean Visible { get; set; }
        public DockStyle DockStyle { get; set; }
        public Int32 Left { get; set; }
        public Int32 Top { get; set; }
        public Point Location { get; set; }
        public string Parent{ get; set; }
        public int Index { get; set; }
        public int Row { get; set; }
        private bool _brocken; 

        public static ToolStripConfig FromPanel(ToolStrip ts)
        {
            try
            {
                var s= new ToolStripConfig
                        {
                            Name = ts.Name,
                            Visible = ts.Visible,
                            DockStyle = ts.Dock,
                            Location = ts.Location,
                            Left = ts.Left,
                            Top = ts.Top
                        };
                if (ts.Parent != null)
                {
                    s.Parent = ts.Parent.Dock.ToString();
                    var parent = (ts.Parent as ToolStripPanel);
                    if (parent==null)
                    {
                        return s;
                    }
                    for (int index = 0; index < parent.Rows.Length; index++)
                    {
                        var row = parent.Rows[index];
                        if (row.Controls.Contains(ts))
                        {
                            s.Row = index;
                            for (int i = 0; i < row.Controls.Count(); i++)
                            {
                                var c = row.Controls[i];
                                if (c == ts)
                                    s.Index = i;
                            }

                        }
                    }
                }
                else
                {
                    s.Parent = "Top";
                }

                return s;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new ToolStripConfig() { _brocken =true,Row = 0,Left = 3,Top = 0,Location = new Point(3,0)};
            }
        }


        public static ToolStripConfig FromXMLString(string input)
        {
            return SerializeHelper.Deserialize<ToolStripConfig>(input);
            
        }

        public string ToXMLString()
        {
            return SerializeHelper.Serialize(this);
        }
    }
}
